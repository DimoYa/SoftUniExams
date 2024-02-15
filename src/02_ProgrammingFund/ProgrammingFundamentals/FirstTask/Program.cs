namespace FirstTask
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    public class Program
    {
        public static string phrase;
        public static void Main()
        {
            phrase = Console.ReadLine();
            string input;

            while ((input = Console.ReadLine()) != "Done")
            {
                var token = input.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries).ToList();

                var command = token[0];
                var parameters = token.Skip(1).ToList();

                switch (command)
                {
                    case "TakeOdd":
                        TakeOddCommand();
                        break;
                    case "Cut":
                        CutCommand(parameters);
                        break;
                    case "Substitute":
                        SubstituteCommand(parameters);
                        break;
                    default: throw new ArgumentException("invalid commmand");
                }
            }

            Console.WriteLine($"Your password is: {phrase}");
        }

        private static void SubstituteCommand(List<string> parameters)
        {
            var substring = parameters[0];
            var substitute = parameters[1];

            if (phrase.Contains(substring))
            {
                phrase = phrase.Replace(substring, substitute);
                Console.WriteLine(phrase);
            }
            else
            {
                Console.WriteLine("Nothing to replace!");
            }
        }

        private static void CutCommand(List<string> parameters)
        {
            var index = int.Parse(parameters[0]);
            var length = int.Parse(parameters[1]);

            phrase = phrase.Remove(index, length);
            Console.WriteLine(phrase);
        }

        private static void TakeOddCommand()
        {
            phrase = new string(phrase.Where((c, i) => i % 2 != 0).ToArray());
            Console.WriteLine(phrase);
        }
    }
}