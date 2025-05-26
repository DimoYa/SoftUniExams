using System.Text.RegularExpressions;

namespace _02.DestinationMapper
{
    public class StartUp
    {
        public static void Main()
        {
            var input = Console.ReadLine();
            var pattern = @"([=\/])(?<placeName>[A-Z][A-Za-z]{2,})\1";
            var travelPoints = 0;
            var places = new List<String>();

            var matches = Regex.Matches(input, pattern);

            foreach (Match m in matches)
            {
                var place = m.Groups["placeName"].Value;
                travelPoints += place.Length;
                places.Add(place);
            }

            Console.WriteLine($"Destinations: {string.Join(", ", places)}");
            Console.WriteLine($"Travel Points: {travelPoints}");
        }
    }
}
