using System.Text.RegularExpressions;

namespace _02_FancyBarcodes
{
    public class StartUp
    {
        public static void Main()
        {
            var pattern = @"^(@#{1,})(?<barCode>[A-Z][A-Za-z0-9]{4,}[A-Z])\1$"; 
            var productGroup = new List<string>();

            var n = int.Parse(Console.ReadLine());

            for (int i = 0; i < n; i++)
            {
                var input = Console.ReadLine();
                var match = Regex.IsMatch(input, pattern);  // First call

                if (match)
                {
                    var product = Regex.Match(input, pattern).Groups["barCode"].Value;  // Second call
                    var digits = product.Where(char.IsDigit).ToArray();
                    if (digits.Length == 0)
                    {
                        productGroup.Add("Product group: 00");
                    }
                    else
                    {
                        productGroup.Add($"Product group: {string.Join("", digits)}");
                    }
                }
                else
                {
                    productGroup.Add("Invalid barcode");
                }
            }
            productGroup.ForEach(x => Console.WriteLine(x));
        }
    }
}
