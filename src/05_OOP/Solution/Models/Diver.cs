using NauticalCatchChallenge.Models.Contracts;
using NauticalCatchChallenge.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NauticalCatchChallenge.Models
{
    public abstract class Diver : IDiver
    {
        private string name;
        private int oxygenLevel;
        private List<string> @catch;
        private double competitionPoints;
        private bool hasHealthIssues;

        protected Diver(string name, int oxygenLevel)
        {
            Name = name;
            OxygenLevel = oxygenLevel;
            this.hasHealthIssues = false;
            this.competitionPoints = 0;
            this.@catch = new List<string>();
        }

        public string Name
        {
            get { return name; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.DiversNameNull);
                }
                name = value;
            }
        }

        public int OxygenLevel
        {
            get { return oxygenLevel; }
            protected set
            {
                oxygenLevel = Math.Max(0, value);
            }
        }

        public IReadOnlyCollection<string> Catch
        {
            get { return @catch.AsReadOnly(); }
        }

        public double CompetitionPoints
        {
            get { return Math.Round(competitionPoints, 1); }
            private set { competitionPoints = value; }
        }

        public bool HasHealthIssues
        {
            get => hasHealthIssues;
            private set
            {
                hasHealthIssues = value;
            }
        }

        public void Hit(IFish fish)
        {
            this.OxygenLevel -= fish.TimeToCatch;
            this.@catch.Add(fish.Name);
            this.CompetitionPoints += fish.Points;
        }

        public abstract void Miss(int timeToCatch);

        public abstract void RenewOxy();


        public void UpdateHealthStatus()
        {
            this.HasHealthIssues = !this.HasHealthIssues;
        }

        public override string ToString()
        {
            return $"Diver [ Name: {this.Name}, Oxygen left: {this.OxygenLevel}, Fish caught: {this.Catch.Count}, Points earned: {this.CompetitionPoints} ]";
        }
    }
}
