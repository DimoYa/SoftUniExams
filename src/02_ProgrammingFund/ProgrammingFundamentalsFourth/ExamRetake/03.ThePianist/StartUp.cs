
using System.Text;

namespace ThePianist
{
    public class StartUp
    {
        static List<Piece> pieces;
        public static void Main()
        {
            var numberOfPieces = int.Parse(Console.ReadLine());
            pieces = new List<Piece>();
            var result = new StringBuilder();

            for (int i = 0; i < numberOfPieces; i++)
            {
                var token = Console.ReadLine().Split('|');

                var name = token[0];
                var composer = token[1];
                var key = token[2];

                pieces.Add(new Piece(name, composer, key));
            }

            string command;

            while ((command = Console.ReadLine())!= "Stop")
            {
                var token = command.Split('|');

                var commandName = token[0];
                var commandParameters = token.Skip(1).ToList();

                switch (commandName)
                {
                    case "Add":
                        result.AppendLine(AddCommand(commandParameters));
                        break;
                    case "Remove":
                        result.AppendLine(RemoveCommand(commandParameters));
                        break;
                    case "ChangeKey":
                        result.AppendLine(ChangeKeyCommand(commandParameters));
                        break;
                }
            }
            pieces.ToList().ForEach(p => result.AppendLine($"{p.Name} -> Composer: {p.Composer}, Key: {p.Key}"));
            Console.WriteLine(result.ToString().TrimEnd());
        }

        private static string ChangeKeyCommand(List<string> commandParameters)
        {
            var name = commandParameters[0];
            var newKey = commandParameters[1];

            var currentPiece = pieces.FirstOrDefault(p => p.Name == name);

            if (currentPiece is null)
            {
                return $"Invalid operation! {name} does not exist in the collection.";
            }

            currentPiece.Key = newKey;
            return $"Changed the key of {name} to {newKey}!";
        }

        private static string RemoveCommand(List<string> commandParameters)
        {
            var name = commandParameters[0];

            var currentPiece = pieces.FirstOrDefault(p => p.Name == name);

            if (currentPiece is null)
            {
                return $"Invalid operation! {name} does not exist in the collection.";
            }

            pieces.Remove(currentPiece);
            return $"Successfully removed {name}!";
        }

        private static string AddCommand(List<string> commandParameters)
        {
            var name = commandParameters[0];
            var composer = commandParameters[1];
            var key = commandParameters[2];

            var currentPiece = pieces.FirstOrDefault(p => p.Name == name);

            if (currentPiece is not null) 
            {
                return $"{name} is already in the collection!";
            }

            pieces.Add(new Piece(name, composer, key));
            return $"{name} by {composer} in {key} added to the collection!";
        }
    }
    public class Piece
    {
        public Piece(string name, string composer, string key)
        {
            Name = name;
            Composer = composer;
            Key = key;
        }

        public string Name { get; set; }
        public string Composer { get; set; }
        public string Key { get; set; }
    }
}
