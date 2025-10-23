
using System;
using System.Reflection;
using System.Text;

namespace PasswordReset
{
    public class Program
    {
        static string password;
        public static void Main()
        {
            password = Console.ReadLine();
            string command;

            while ((command = Console.ReadLine()) != "Done") 
            {
                var token = command.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                var commandName = token[0];
                var commandParams = token.Skip(1).ToList();
                string result = string.Empty;

                switch (commandName)
                {
                    case "TakeOdd":
                        result = TakeOddCommand();
                        break;
                    case "Cut":
                        result = CutCommand(commandParams);
                        break;
                    case "Substitute":
                        result = SubstituteCommand(commandParams);
                        break;
                }

                Console.WriteLine(result);
            }
            Console.WriteLine($"Your password is: {password}");
        }

        private static string SubstituteCommand(List<string> commandParams)
        {
            var substring = commandParams[0];
            var substitute = commandParams[1];

            if (password.Contains(substring))
            {
                var result = password.Replace(substring, substitute);
                password = result;
                return result;
            }
            return "Nothing to replace!";
        }

        private static string CutCommand(List<string> commandParams)
        {
            var index = int.Parse(commandParams[0]);
            var length = int.Parse(commandParams[1]);

            var result = password.Remove(index, length);
            password = result;

            return result;
        }

        private static string TakeOddCommand()
        {
            StringBuilder result = new StringBuilder();

            for (int i = 1; i < password.Length; i += 2)
            {
                result.Append(password[i]);
            }
            password = result.ToString();
            return result.ToString();
        }
    }
}
