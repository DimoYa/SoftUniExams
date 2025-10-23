namespace _06.Basketball_Tournament
{
    public class StartUp
    {
        public static void Main()
        {
            string line;
            var wonGames = 0;
            var totalGames = 0;

            while ((line = Console.ReadLine()) != "End of tournaments")
            {
                var tourNamentName = line;
                var countOfGames = int.Parse(Console.ReadLine());
                int games = 1;

                for (int i = 0; i < countOfGames; i++)
                {
                    var desiPoints = int.Parse(Console.ReadLine());
                    var otherTeamPoints = int.Parse(Console.ReadLine());

                    if (desiPoints > otherTeamPoints)
                    {
                        Console.WriteLine($"Game {games} of tournament {tourNamentName}: win with {desiPoints - otherTeamPoints} points.");
                        wonGames++;
                    }
                    else
                    {
                        Console.WriteLine($"Game {games} of tournament {tourNamentName}: lost with {otherTeamPoints - desiPoints} points.");
                    }

                    games++;
                    totalGames++;
                }
            }
            Console.WriteLine($"{((decimal)(wonGames) / totalGames)*100:F2}.00% matches won");
            Console.WriteLine($"{((decimal)(totalGames - wonGames) / totalGames)*100:F2}.00% matches lost");

        }
    }
}
