using System.Text;

namespace _01_PasswordReset
{
    public class StartUp
    {
        public static string password;
        public static StringBuilder sb;

        public static void Main()
        {
            password = Console.ReadLine();
            string command;
            sb = new StringBuilder();

            while ((command = Console.ReadLine()) != "Done")
            {
                var tokens = command.Split(" ", StringSplitOptions.RemoveEmptyEntries);

                if (tokens.Length == 0) continue;

                var commandName = tokens[0];
                var commandArgs = tokens.Skip(1).ToArray();

                switch (commandName)
                {
                    case "TakeOdd":
                        TakeOddCommand();
                        break;
                    case "Cut":
                        CutCommand(commandArgs);
                        break;
                    case "Substitute":
                        SubstituteCommand(commandArgs);
                        break;
                }
            }
            sb.AppendLine($"Your password is: {password}");
            Console.WriteLine(sb.ToString().TrimEnd());
        }

        private static void SubstituteCommand(string[] commandArgs)
        {
            if (commandArgs.Length < 2) return;

            var substring = commandArgs[0];
            var substitute = commandArgs[1];

            if (!password.Contains(substring))
            {
                sb.AppendLine("Nothing to replace!");
                return;
            }

            password = password.Replace(substring, substitute);
            sb.AppendLine(password);
        }

        private static void CutCommand(string[] commandArgs)
        {
            if (commandArgs.Length < 2) return;

            var index = int.Parse(commandArgs[0]);
            var length = int.Parse(commandArgs[1]);

            password = password.Remove(index, length);
            sb.AppendLine(password);
        }

        private static void TakeOddCommand()
        {
            var newPassword = new StringBuilder();
            for (int i = 1; i < password.Length; i += 2)
            {
                newPassword.Append(password[i]);
            }
            password = newPassword.ToString();
            sb.AppendLine(password);
        }
    }
}
