namespace ThirdTask
{
    public class Program
    {
        public static void Main()
        {
            var countJoinery = int.Parse(Console.ReadLine());

            if (countJoinery >= 10)
            {
                var typeOfJoinery = Console.ReadLine();
                var deliveryType = Console.ReadLine();

                var size90X130 = 110;
                var size100X150 = 140;
                var size130X180 = 190;
                var size200X300 = 250;

                var price = 0.0;

                switch (typeOfJoinery)
                {
                    case "90X130":
                        price = countJoinery * size90X130 * (countJoinery > 30 && countJoinery <= 60
                                                        ? (1 - 0.05)
                                                        : countJoinery > 60 ? (1 - 0.08)
                                                        : 1);
                        break;
                    case "100X150":
                        price = countJoinery * size100X150 * (countJoinery > 40 && countJoinery <= 80
                                                       ? (1 - 0.06)
                                                       : countJoinery > 80 ? (1 - 0.1)
                                                       : 1);
                        break;
                    case "130X180":
                        price = countJoinery * size130X180 * (countJoinery > 20 && countJoinery <= 50
                                                       ? (1 - 0.07)
                                                       : countJoinery > 50 ? (1 - 0.12)
                                                       : 1);
                        break;
                    case "200X300":
                        price = countJoinery * size200X300 * (countJoinery > 25 && countJoinery <= 50
                                                       ? (1 - 0.09)
                                                       : countJoinery > 50 ? (1 - 0.14)
                                                       : 1);
                        break;
                }

                if (deliveryType == "With delivery")
                {
                    price += 60;
                }

                if (countJoinery > 99)
                {
                    price *= (1 - 0.04);
                }

                Console.WriteLine($"{price:F2} BGN");
            }
            else
            {
                Console.WriteLine("Invalid order");
            }
        }
    }
}