
namespace TheLmitationGame
{
    public class StartUp
    {
        static string message;
        public static void Main()
        {
            message = Console.ReadLine();

            string line;

            while ((line = Console.ReadLine()) != "Decode")
            {
                var token = line.Split('|');
                var commandName = token[0];
                var commandParameters = token.Skip(1).ToList();

                switch (commandName)
                {
                    case "ChangeAll":
                        ChangeAllCommand(commandParameters);
                        break;
                    case "Insert":
                        InsertCommand(commandParameters);
                        break;
                    case "Move":
                        MoveCommand(commandParameters);
                        break;
                }
            }
            Console.WriteLine($"The decrypted message is: {message}");
        }

        private static void ChangeAllCommand(List<string> commandParameters)
        {
            var substring = commandParameters[0];
            var replacement = commandParameters[1];

            message = message.Replace(substring, replacement);
        }

        private static void InsertCommand(List<string> commandParameters)
        {
            int index = int.Parse(commandParameters[0]);
            var value = commandParameters[1];

            message = message.Insert(index, value);
        }

        private static void MoveCommand(List<string> commandParameters)
        {
            int n = int.Parse(commandParameters[0]);

            message =  message.Substring(n) + message.Substring(0, n);
        }
    }
}
