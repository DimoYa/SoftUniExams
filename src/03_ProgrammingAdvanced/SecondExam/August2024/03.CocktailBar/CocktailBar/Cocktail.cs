using System.Text;
using System.Xml.Linq;

namespace CocktailBar
{
    public class Cocktail
    {
        private List<string> _ingredients;

        public Cocktail(string name, decimal price, double volume, string ingredients)
        {
            _ingredients = ingredients.Split(", ").ToList();
            Name = name;
            Price = price;
            Volume = volume;
        }

        public List<string> Ingredients { get => _ingredients; }

        public string Name { get; set; }
        public decimal Price  { get; set; }
        public double Volume  { get; set; }

        public override string ToString()
        {
            var result = new StringBuilder();

            result.AppendLine($"{Name}, Price: {Price:F2} BGN, Volume: {Volume} ml");
            result.AppendLine($"Ingredients: {string.Join(", ", Ingredients)}");

            return result.ToString().TrimEnd();
        }
    }
}
