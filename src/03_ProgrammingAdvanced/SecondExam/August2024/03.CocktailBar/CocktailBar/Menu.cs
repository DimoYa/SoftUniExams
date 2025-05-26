using System.Text;
using System.Xml.Linq;

namespace CocktailBar
{
    public class Menu
    {
        public int BarCapacity { get; set; }

        public Menu(int barCapacity)
        {
            BarCapacity = barCapacity;
            Cocktails = new List<Cocktail>();
        }

        public List<Cocktail> Cocktails { get; set; }

        public void AddCocktail(Cocktail cocktail)
        {
            if (BarCapacity > Cocktails.Count && Cocktails.All(c => c.Name != cocktail.Name))
            {
                Cocktails.Add(cocktail);
            }
        }

        public bool RemoveCocktail(string name)
        {
            var coctailToRemove = Cocktails.FirstOrDefault(c => c.Name == name);
            if (coctailToRemove == null)
            {
                return false;
            }
            Cocktails.Remove(coctailToRemove);
            return true;
        }

        public Cocktail GetMostDiverse()
        {
            return Cocktails
                .OrderByDescending(x => x.Ingredients.Count)
                .FirstOrDefault();
        }

        public string Details(string cocktailName)
        {
            var coctail = Cocktails.FirstOrDefault(c => c.Name == cocktailName);
            return coctail.ToString();
        }

        public string GetAll()
        {
            var result = new StringBuilder();

            result.AppendLine("All Cocktails:");
            Cocktails
                .OrderBy(c => c.Name)
                .Select(c => c.Name)
                .ToList()
                .ForEach(c => result.AppendLine(c));

            return result.ToString().TrimEnd();
        }
    }
}
