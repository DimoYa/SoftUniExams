namespace FirstTask
{
    public class Program
    {
        static void Main()
        {
            var avioName = Console.ReadLine();
            var adultsCountOfTickets = int.Parse(Console.ReadLine());
            var childrenCountOfTickets = int.Parse(Console.ReadLine());
            var netPriceAdult = double.Parse(Console.ReadLine());
            var serviceTaxPrice = double.Parse(Console.ReadLine());

            var netPriceChield = netPriceAdult * (1 - 0.7);
            var TotalPricePerAdult = netPriceAdult + serviceTaxPrice;
            var TotalPricePerChild = netPriceChield + serviceTaxPrice;

            var totalPrice = TotalPricePerAdult * adultsCountOfTickets + TotalPricePerChild * childrenCountOfTickets;

            var profit = totalPrice * 0.2;

            Console.WriteLine($"The profit of your agency from {avioName} tickets is {profit:F2} lv.");
        }
    }
}