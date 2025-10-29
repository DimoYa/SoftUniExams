namespace RapidCourier
{
    public class StartUp
    {
        public static void Main()
        {
            var packagesStack = new Stack<int>(Console.ReadLine().Split().Select(int.Parse));
            var couriersQueue = new Queue<int>(Console.ReadLine().Split().Select(int.Parse));
            var deliveredPackages = 0;

            while (packagesStack.Any() && couriersQueue.Any())
            {
                var currentPackage = packagesStack.Pop();
                var currentCourier = couriersQueue.Dequeue();

                if (currentCourier >= currentPackage)
                {
                    if (currentCourier > currentPackage)
                    {
                        var newCapacity = currentCourier - 2 * currentPackage;

                        if (newCapacity > 0)
                        {
                            couriersQueue.Enqueue(newCapacity);
                        }
                    }
                    deliveredPackages += currentPackage;
                }
                else
                {
                    var newPackageW = currentPackage - currentCourier;
                    packagesStack.Push(newPackageW);
                    deliveredPackages += currentCourier;
                }
            }
            Console.WriteLine($"Total weight: {deliveredPackages} kg");
            if (!packagesStack.Any() && !couriersQueue.Any())
            {
                Console.WriteLine("Congratulations, all packages were delivered successfully by the couriers today.");
            }
            else if (packagesStack.Any() && !couriersQueue.Any())
            {
                Console.WriteLine($"Unfortunately, there are no more available couriers to deliver the following packages: {string.Join(", ", packagesStack)}");
            }
            else
            {
                Console.WriteLine($"Couriers are still on duty: {string.Join(", ", couriersQueue)} but there are no more packages to deliver.");
            }
        }
    }
}
