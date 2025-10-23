namespace TennisEquipment
{
    public class StartUp
    {
        public static void Main()
        {
            var priceTennisRacquets = double.Parse(Console.ReadLine());
            var countTennisRacquet = int.Parse(Console.ReadLine());
            var countShoes = int.Parse(Console.ReadLine());
            var priceShoes = priceTennisRacquets/6;

            var totalPriceRacquets = priceTennisRacquets * countTennisRacquet;
            var totalPriceShoes = priceShoes * countShoes;
            var totalPriceRest = (totalPriceRacquets + totalPriceShoes) * 0.2;

            var totalPrice = totalPriceRacquets + totalPriceShoes + totalPriceRest;

            var djokoPrice = totalPrice / 8;
            var sponsorsPrice = totalPrice * 7 / 8;

            Console.WriteLine($"Price to be paid by Djokovic {Math.Floor(djokoPrice)}");
            Console.WriteLine( $"Price to be paid by sponsors {Math.Ceiling(sponsorsPrice)}");
        }
    }
}
