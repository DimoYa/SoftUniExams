namespace FifthTask
{
    public class Program
    {
        public static void Main()
        {
            string command;
            string bestPlayer = string.Empty;
            int maximumGoals = 0;

            while ((command = Console.ReadLine()) != "END")
            {
                var nameOfPlayer = command;
                var numberOfGoals = int.Parse(Console.ReadLine());

                if (numberOfGoals > maximumGoals)
                {
                    bestPlayer = nameOfPlayer;
                    maximumGoals = numberOfGoals;
                }

                if (numberOfGoals >= 10)
                {
                    bestPlayer = nameOfPlayer;
                    break;
                }
            }

            Console.WriteLine($"{bestPlayer} is the best player!");
            var hatTrick = maximumGoals >= 3 ? " and made a hat-trick !!!" : ".";
            Console.WriteLine($"He has scored {maximumGoals} goals{hatTrick}");
        }
    }
}