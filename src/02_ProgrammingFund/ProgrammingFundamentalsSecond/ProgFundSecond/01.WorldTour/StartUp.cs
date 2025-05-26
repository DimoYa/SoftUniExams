using System;
using System.Data;

namespace _01.WorldTour
{
    public class StartUp
    {
        public static string stops;
        public static void Main()
        {
            stops = Console.ReadLine();

            string command;

            while ((command = Console.ReadLine()) != "Travel")
            {
                var token = command.Split(new[] { ':' }, StringSplitOptions.RemoveEmptyEntries);

                var commandName = token[0];
                var commandParams = token.Skip(1).ToList();

                switch (commandName)
                {
                    case "Add Stop":
                        AddStopCommand(commandParams);
                        break;
                    case "Remove Stop":
                        RemoveStopCommand(commandParams);
                        break;
                    case "Switch":
                        SwitchCommand(commandParams);
                        break;
                }
                Console.WriteLine(stops);
            }
            Console.WriteLine($"Ready for world tour! Planned stops: {stops}");
        }

        private static void SwitchCommand(List<string> commandParams)
        {
            var oldString = commandParams[0];
            var newString = commandParams[1];

            if (stops.Contains(oldString))
            {
                stops = stops.Replace(oldString, newString);
            }
        }

        private static void RemoveStopCommand(List<string> commandParams)
        {
            var startIndex = int.Parse(commandParams[0]);
            var endtIndex = int.Parse(commandParams[1]);

            if (IsValidIdex(startIndex, stops) && IsValidIdex(endtIndex, stops))
            {
                stops = stops.Remove(startIndex, endtIndex - startIndex + 1);
            }
        }

        private static void AddStopCommand(List<string> commandParams)
        {
            var index = int.Parse(commandParams[0]);
            var append = commandParams[1];

            if (IsValidIdex(index, stops))
            {
                stops = stops.Insert(index, append);
            }
        }

        private static bool IsValidIdex(int index, string phrase)
        {
            return index >= 0 && index < phrase.Length;
        }
    }
}
