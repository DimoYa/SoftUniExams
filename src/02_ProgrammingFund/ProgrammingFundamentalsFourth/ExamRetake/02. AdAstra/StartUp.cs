using System.Text.RegularExpressions;

namespace AdAstra
{
    public class StartUp
    {
        public static void Main()
        {
            var input = Console.ReadLine();
            var pattern = @"([|#])(?<itemName>[A-Za-z\s]+)\1(?<date>\d{2}\/\d{2}\/\d{2})\1(?<calories>(10000|[0-9]{1,4}))\1";
            var items = new List<Item>();

            var matches = Regex.Matches(input, pattern);

            foreach (Match m in matches)
            {
                var name = m.Groups["itemName"].Value;
                var date = m.Groups["date"].Value;
                var calories = int.Parse(m.Groups["calories"].Value);

                items.Add(new Item(name, date, calories));
            }

            var totalCalories = items.Sum(x=> x.Calories);
           
            var daysToLive = (int)(totalCalories / 2000);

            Console.WriteLine($"You have food to last you for: {daysToLive} days!");
            items
                .ToList()
                .ForEach(x => Console.WriteLine($"Item: {x.Name}, Best before: {x.Date}, Nutrition: {x.Calories}"));
        }
        public class Item
        {
            public Item(string name, string date, int calories)
            {
                this.Name = name;
                this.Date = date;
                this.Calories = calories;
            }

            public string Name { get; set; }

            public string Date { get; set; }

            public int Calories { get; set; }
        }
    }
}
