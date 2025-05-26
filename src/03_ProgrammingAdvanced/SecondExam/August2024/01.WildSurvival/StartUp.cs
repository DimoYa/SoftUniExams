using System;
using System.Collections.Generic;
using System.Linq;

namespace _01.WildSurvival
{
    public class StartUp
    {
        public static void Main()
        {
            var bees = new Queue<int>(Console.ReadLine().Split().Select(int.Parse));
            var beeEaters = new Stack<int>(Console.ReadLine().Split().Select(int.Parse));

            while (bees.Count > 0 && beeEaters.Count > 0)
            {
                int currentBeeGroup = bees.Dequeue();
                int currentEaterGroup = beeEaters.Pop();

                int eaterPower = currentEaterGroup * 7;

                if (eaterPower > currentBeeGroup)
                {
                    int remainingPower = eaterPower - currentBeeGroup;
                    int returningEaters = (int)Math.Ceiling((double)remainingPower / 7);

                    if (beeEaters.Count > 0)
                    {
                        int nextGroup = beeEaters.Pop();
                        beeEaters.Push(nextGroup + returningEaters);
                    }
                    else
                    {
                        beeEaters.Push(returningEaters);
                    }
                }
                else if (currentBeeGroup > eaterPower)
                {
                    int survivingBees = currentBeeGroup - eaterPower;
                    bees.Enqueue(survivingBees);
                }
            }

            Console.WriteLine("The final battle is over!");
            if (bees.Count == 0 && beeEaters.Count == 0)
            {
                Console.WriteLine("But no one made it out alive!");
            }
            if (bees.Count > 0)
            {
                Console.WriteLine($"Bee groups left: {string.Join(", ", bees)}");
            }
            if (beeEaters.Count > 0)
            {
                Console.WriteLine($"Bee-eater groups left: {string.Join(", ", beeEaters)}");
            }
        }
    }
}