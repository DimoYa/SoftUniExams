using System.Text;

namespace FourthTask
{
    public class Program
    {
        public static void Main()
        {
            var countOfBalls = int.Parse(Console.ReadLine());
            var points = 0.0;
            var red = 0;
            var orange = 0;
            var yellow = 0;
            var white = 0;
            var black = 0;
            var other = 0;
            var sb = new StringBuilder();

            for (int i = 0; i < countOfBalls; i++)
            {
                var colors = Console.ReadLine();

                switch (colors)
                {
                    case "red":
                        points += 5;
                        red++;
                        break;
                    case "orange":
                        points += 10;
                        orange++;
                        break;
                    case "yellow":
                        points += 15;
                        yellow++;
                        break;
                    case "white":
                        points += 20;
                        white++;
                        break;
                    case "black":
                        points = Math.Floor(points / 2);
                        black++;
                        break;
                    default:
                        other++;
                        break;
                }
            }

            sb.AppendLine($"Total points: {points}");
            sb.AppendLine($"Red balls: {red}");
            sb.AppendLine($"Orange balls: {orange}");
            sb.AppendLine($"Yellow balls: {yellow}");
            sb.AppendLine($"White balls: {white}");
            sb.AppendLine($"Other colors picked: {other}");
            sb.AppendLine($"Divides from black balls: {black}");

            Console.WriteLine(sb.ToString().TrimEnd());
        }
    }
}