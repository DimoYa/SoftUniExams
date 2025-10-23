using System.ComponentModel.DataAnnotations;

namespace GameNumberWars
{
    public class Program
    {
        public static void Main()
        {
            var firstPlayerName = Console.ReadLine();
            var secondPlayerName = Console.ReadLine();
            var isWar = false;
            var isGameEnded = false;
            var firstWinWar = false;
            var secondWinWar = false;
            int firstPlayerPointsSum = 0;
            int secondPlayerPointsSum = 0;

            while (true)
            {
                var firstPlayerInput = Console.ReadLine();
                var secondPlayerInput = Console.ReadLine();

                if (firstPlayerInput == "End of game" || secondPlayerInput == "End of game")
                {
                    isGameEnded = true;
                    break;
                }

                int firstPlayerPoints = int.Parse(firstPlayerInput);
                int secondPlayerPoints = int.Parse(secondPlayerInput);


                if (firstPlayerPoints > secondPlayerPoints)
                {
                    if (isWar)
                    {
                        firstWinWar = true;
                        break;
                    }

                    firstPlayerPointsSum += firstPlayerPoints - secondPlayerPoints;
                }
                else if (firstPlayerPoints < secondPlayerPoints)
                {
                    if (isWar)
                    {
                        secondWinWar = true;
                        break;
                    }
                    secondPlayerPointsSum += secondPlayerPoints - firstPlayerPoints;
                }
                else
                {
                    isWar = true;
                    continue;
                }
            }
            if (isWar) 
            {
                Console.WriteLine("Number wars!");
                if (firstWinWar)
                {
                    Console.WriteLine($"{firstPlayerName} is winner with {firstPlayerPointsSum} points");
                }
                else
                {
                    Console.WriteLine($"{secondPlayerName} is winner with {secondPlayerPointsSum} points");
                }
            }
            if (isGameEnded)
            {
                Console.WriteLine($"{firstPlayerName} has {firstPlayerPointsSum} points");
                Console.WriteLine($"{secondPlayerName} has {secondPlayerPointsSum} points");
            }
        }
    }
}
