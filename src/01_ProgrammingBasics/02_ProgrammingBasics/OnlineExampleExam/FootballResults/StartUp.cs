namespace FootballResults
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            var firstGameResult = Console.ReadLine();
            var secondGameResult = Console.ReadLine();
            var thirdGameResult = Console.ReadLine();

            var wonGames = 0;
            var lostGames = 0;
            var remiGames = 0;

            string[] results = { firstGameResult, secondGameResult, thirdGameResult };

            foreach (var result in results)
            {
                var parts = result.Split(':');
                int team1 = int.Parse(parts[0]);
                int team2 = int.Parse(parts[1]);

                if (team1 > team2)
                    wonGames++;
                else if (team1 < team2)
                    lostGames++;
                else
                    remiGames++;
            }

            Console.WriteLine($"Team won {wonGames} games.");
            Console.WriteLine($"Team lost {lostGames} games.");
            Console.WriteLine($"Drawn games: {remiGames}");
        }
    }
}
