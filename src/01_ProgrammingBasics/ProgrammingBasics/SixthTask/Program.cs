using System.Text;

namespace SixthTask
{
    public class Program
    {
        public static void Main()
        {
            var startRange = Console.ReadLine();
            var endRange = Console.ReadLine();
            var result = new StringBuilder();

            for (int first = int.Parse(startRange[0].ToString()); first <= int.Parse(endRange[0].ToString()); first++)
            {
                for (int second = int.Parse(startRange[1].ToString()); second <= int.Parse(endRange[1].ToString()); second++)
                {
                    for (int third = int.Parse(startRange[2].ToString()); third <= int.Parse(endRange[2].ToString()); third++)
                    {
                        for (int fourth = int.Parse(startRange[3].ToString()); fourth <= int.Parse(endRange[3].ToString()); fourth++)
                        {
                            if (first % 2 != 0 && second % 2 != 0 && third % 2 != 0 && fourth % 2 != 0)
                            {
                                result.Append($"{first}{second}{third}{fourth}");
                                result.Append(' ');
                            }
                        }
                    }
                }
            }

            Console.WriteLine(result.ToString().TrimEnd());
        }
    }
}