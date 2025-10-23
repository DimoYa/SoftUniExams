
namespace Tennis_Ranklist
{
    public class StartUp
    {
        public static void Main()
        {
            var countOfGames = int.Parse(Console.ReadLine());
            var initialPoints = int.Parse(Console.ReadLine());
            var finalePoints = 0;
            var wonGames = 0;

            for (int i = 0; i < countOfGames; i++)
            {
                var stage = Console.ReadLine();

                switch (stage)
                {
                    case "W":
                        finalePoints += 2000; wonGames++;
                        break;
                    case "F":
                        finalePoints += 1200;
                        break;
                    case "SF":
                        finalePoints += 720;
                        break;
                    default:
                        break;
                }
            }
            Console.WriteLine($"Final points: {initialPoints + finalePoints}\nAverage points: {Math.Floor((decimal)(finalePoints / countOfGames))} \n{(double)wonGames / countOfGames * 100:F2}%");
        }
    }
}
