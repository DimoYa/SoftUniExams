using System;
using System.Net.Http.Headers;

namespace _03.PlantDiscovery
{
    public class StartUp
    {
        public static void Main()
        {
            var n = int.Parse(Console.ReadLine());
            var plants = new List<Plant>();

            for (int i = 0; i < n; i++)
            {
                var plant = Console.ReadLine().Split(new[] { "<->" }, StringSplitOptions.None)
                                              .Select(x => x.Trim())
                                              .ToArray(); ;

                var plantName = plant[0];
                var rarity = int.Parse(plant[1]);
                Plant currentPlant = GetPlantByName(plants, plantName);

                if (currentPlant is null)
                {
                    plants.Add(new Plant(plantName, rarity));
                }
                else
                {
                    currentPlant.Rarity = rarity;
                }

            }

            string command;

            while ((command = Console.ReadLine()) != "Exhibition")
            {
                var token = command.Split(new[] {':', '-'}, StringSplitOptions.RemoveEmptyEntries)
                                   .Select(x=> x.Trim())
                                   .ToList();

                var commandName = token[0];
                var commandParameters = token.Skip(1).ToList();

                switch (commandName)
                {
                    case "Rate":
                        RateCommand(plants, commandParameters);
                        break;
                    case "Update":
                        UpdateCommand(plants, commandParameters);
                        break;
                    case "Reset":
                        ResetCommand(plants, commandParameters);
                        break;
                }
            }
            Console.WriteLine("Plants for the exhibition:");
            plants.ToList().ForEach(p =>
            {
                var average = p.Rating.Any() ? p.Rating.Average() : 0;
                Console.WriteLine($"- {p.Name}; Rarity: {p.Rarity}; Rating: {average:F2}");
            });
        }

        private static Plant GetPlantByName(List<Plant> plants, string plantName)
        {
            return plants.FirstOrDefault(p => p.Name == plantName);
        }

        private static void ResetCommand(List<Plant> plants, List<string> commandParameters)
        {
            var plantName = commandParameters[0];

            Plant currentPlant = GetPlantByName(plants, plantName);
            if (currentPlant is null)
            {
                Console.WriteLine("error");
                return;
            }
            currentPlant.Rating.Clear();
        }

        private static void UpdateCommand(List<Plant> plants, List<string> commandParameters)
        {
            var plantName = commandParameters[0];
            var newRarity = int.Parse(commandParameters[1]);

            Plant currentPlant = GetPlantByName(plants, plantName);
            if (currentPlant is null)
            {
                Console.WriteLine("error");
                return;
            }
            currentPlant.Rarity = newRarity;
        }

        private static void RateCommand(List<Plant> plants, List<string> commandParameters)
        {
            var plantName = commandParameters[0];
            var rating = int.Parse(commandParameters[1]);

            Plant currentPlant = GetPlantByName(plants, plantName);
            if (currentPlant is null)
            {
                Console.WriteLine("error");
                return;
            }
            currentPlant.Rating.Add(rating);
        }
    }
    public class Plant
    {
        public Plant(string name, int rarity)
        {
            Name = name;
            Rarity = rarity;
            Rating = new List<double>();
        }

        public string Name { get; set; }
        public int Rarity { get; set; }
        public List<double> Rating { get; set; }
    }
}
