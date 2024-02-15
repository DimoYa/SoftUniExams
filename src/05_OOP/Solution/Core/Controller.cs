using NauticalCatchChallenge.Core.Contracts;
using NauticalCatchChallenge.Models;
using NauticalCatchChallenge.Models.Contracts;
using NauticalCatchChallenge.Repositories;
using NauticalCatchChallenge.Repositories.Contracts;
using NauticalCatchChallenge.Utilities.Messages;
using System.IO;
using System.Text;
using System.Xml.Linq;

namespace NauticalCatchChallenge.Core
{
    public class Controller : IController
    {
        private IRepository<IDiver> divers;
        private IRepository<IFish> fishes;

        public Controller()
        {
            this.divers = new DiverRepository();
            this.fishes = new FishRepository();
        }

        public string ChaseFish(string diverName, string fishName, bool isLucky)
        {
            IDiver diver = this.divers.GetModel(diverName);
            IFish fish = this.fishes.GetModel(fishName);

            if (diver is null)
            {
                return string.Format(OutputMessages.DiverNotFound, nameof(DiverRepository), diverName);
            }

            if (fish is null)
            {
                return string.Format(OutputMessages.FishNotAllowed, fishName);
            }

            if (diver.HasHealthIssues)
            {
                return string.Format(OutputMessages.DiverHealthCheck, diverName);
            }

            if (diver.OxygenLevel < fish.TimeToCatch)
            {
                diver.Miss(fish.TimeToCatch);
                if (diver.OxygenLevel <= 0)
                {
                    diver.UpdateHealthStatus();
                }
                return string.Format(OutputMessages.DiverMisses, diverName, fishName);
            }
            else if (diver.OxygenLevel == fish.TimeToCatch)
            {
                if (isLucky)
                {
                    diver.Hit(fish);
                    if (diver.OxygenLevel <= 0)
                    {
                        diver.UpdateHealthStatus();
                    }
                    return string.Format(OutputMessages.DiverHitsFish, diverName, fish.Points, fishName);
                }
                else
                {
                    diver.Miss(fish.TimeToCatch);

                    if (diver.OxygenLevel <= 0)
                    {
                        diver.UpdateHealthStatus(); 
                    }
                    return string.Format(OutputMessages.DiverMisses, diverName, fishName);
                }
            }
            else
            {
                diver.Hit(fish);
                if (diver.OxygenLevel <= 0)
                {
                    diver.UpdateHealthStatus();
                }
                return string.Format(OutputMessages.DiverHitsFish, diverName, fish.Points, fishName);

            }
        }

        public string CompetitionStatistics()
        {
            var healthyDivers = this.divers.Models.Where(diver => !diver.HasHealthIssues)
                                        .OrderByDescending(diver => diver.CompetitionPoints)
                                        .ThenByDescending(diver => diver.Catch.Count)
                                        .ThenBy(diver => diver.Name);

            StringBuilder statistics = new StringBuilder();
            statistics.AppendLine("**Nautical-Catch-Challenge**");
            healthyDivers.ToList().ForEach(diver => statistics.AppendLine(diver.ToString()));

            return statistics.ToString().TrimEnd();
        }

        public string DiveIntoCompetition(string diverType, string diverName)
        {

            var newDiver = this.divers.GetModel(diverName);

            if (newDiver is not null)
            {
                return string.Format(OutputMessages.DiverNameDuplication, diverName, nameof(DiverRepository));
            }

            switch (diverType.ToLower())
            {
                case "freediver":
                    newDiver = new FreeDiver(diverName);
                    break;

                case "scubadiver":
                    newDiver = new ScubaDiver(diverName);
                    break;

                default:
                    return string.Format(OutputMessages.DiverTypeNotPresented, diverType);
            }

            this.divers.AddModel(newDiver);

            return string.Format(OutputMessages.DiverRegistered, diverName, nameof(DiverRepository));
        }

        public string DiverCatchReport(string diverName)
        {
            IDiver diver = this.divers.GetModel(diverName);

            StringBuilder catchReport = new StringBuilder();

            catchReport.AppendLine(diver.ToString());
            catchReport.AppendLine("Catch Report:");

            foreach (string fishName in diver.Catch)
            {
                IFish fish = this.fishes.GetModel(fishName);

                if (fish != null)
                {
                    catchReport.AppendLine(fish.ToString());
                }
            }

            return catchReport.ToString().TrimEnd();
        }

        public string HealthRecovery()
        {
            int recoveredCount = 0;

            foreach (IDiver diver in this.divers.Models)
            {
                if (diver.HasHealthIssues)
                {
                    diver.UpdateHealthStatus(); 
                    diver.RenewOxy(); 
                    recoveredCount++;
                }
            }

            return string.Format(OutputMessages.DiversRecovered, recoveredCount);
        }

        public string SwimIntoCompetition(string fishType, string fishName, double points)
        {
            var newFish = this.fishes.GetModel(fishName);

            if (newFish is not null)
            {
                return string.Format(OutputMessages.FishNameDuplication, fishName, nameof(FishRepository));
            }

            switch (fishType.ToLower())
            {
                case "reeffish":
                    newFish = new ReefFish(fishName, points);
                    break;

                case "deepseafish":
                    newFish = new DeepSeaFish(fishName, points);
                    break;

                case "predatoryfish":
                    newFish = new PredatoryFish(fishName, points);
                    break;

                default:
                    return string.Format(OutputMessages.FishTypeNotPresented, fishType);
            }

            this.fishes.AddModel(newFish);

            return string.Format(OutputMessages.FishCreated, fishName);
        }
    }
}