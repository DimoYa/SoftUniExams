namespace SecondTask
{
    public class Program
    {
        public static void Main()
        {
            var priceLuggage = double.Parse(Console.ReadLine());
            var kgForLuggage = double.Parse(Console.ReadLine());
            var daysToTravel = int.Parse(Console.ReadLine());
            var countLuggage = int.Parse(Console.ReadLine());
            var luggageTaxes = 0.0;

            if (kgForLuggage < 10)
            {
                luggageTaxes = priceLuggage * 0.2;
            }
            else if (kgForLuggage >= 10 && kgForLuggage <= 20)
            {
                luggageTaxes = priceLuggage * 0.5;
            }
            else
            {
                luggageTaxes = priceLuggage;
            }

            double additionalCost;

            if (daysToTravel < 7)
            {
                additionalCost = 1.4;
            }
            else if (daysToTravel >= 7 && daysToTravel <= 30)
            {
                additionalCost = 1.15;
            }
            else
            {
                additionalCost = 1.10;
            }

            luggageTaxes *= additionalCost;

            var totalSum = luggageTaxes * countLuggage;

            Console.WriteLine($"The total price of bags is: {totalSum:F2} lv.");
        }
    }
}