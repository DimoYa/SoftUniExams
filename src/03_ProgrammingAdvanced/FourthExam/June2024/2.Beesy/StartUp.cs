namespace Beesy
{
    public class StartUp
    {
        public static void Main()
        {
            var n = int.Parse(Console.ReadLine());
            var matrix = new char[n, n];
            var beePosition = (-1, -1);
            var unitsOfEnergy = 15;
            var collectedNectar = 0;
            var isHiveReached = false;
            var isEnergyRestored = false;

            for (int i = 0; i < n; i++)
            {
                var row = Console.ReadLine().ToArray();
                for (int j = 0; j < n; j++)
                {
                    matrix[i, j] = row[j];
                    if (matrix[i, j] == 'B')
                    {
                        beePosition = (i, j);
                    }
                }
            }

            var directions = new Dictionary<string, (int dx, int dy)>
            {
                { "up", (-1, 0) },
                { "down", (1, 0) },
                { "left", (0, -1) },
                { "right", (0, 1) }
            };

            matrix[beePosition.Item1, beePosition.Item2] = '-';


            while (true)
            {
                if (unitsOfEnergy == 0)
                {
                    if (isEnergyRestored || collectedNectar < 30)
                    {
                        break;
                    }

                    unitsOfEnergy = collectedNectar - 30;
                    collectedNectar = 30;
                    isEnergyRestored = true;

                    if (unitsOfEnergy == 0)
                    {
                        break;
                    }
                }

                var command = Console.ReadLine();

                (int dx, int dy) move = directions[command];
                int newRow = beePosition.Item1 + move.dx;
                int newCol = beePosition.Item2 + move.dy;

                // Wrap around if bee moves out of bounds
                if (newRow < 0) newRow = n - 1;
                else if (newRow >= n) newRow = 0;

                if (newCol < 0) newCol = n - 1;
                else if (newCol >= n) newCol = 0;

                var newPosition = matrix[newRow, newCol];
                beePosition = (newRow, newCol);
                unitsOfEnergy--;
               
                if (newPosition == 'H')
                {
                    isHiveReached = true;
                    matrix[newRow, newCol] = '-';
                    break;
                }

                if (char.IsDigit(newPosition))
                {
                    collectedNectar += newPosition - '0';
                    matrix[newRow, newCol] = '-';
                }
            }

            if (isHiveReached)
            {
                if (collectedNectar >= 30)
                {
                    Console.WriteLine($"Great job, Beesy! The hive is full. Energy left: {unitsOfEnergy}");
                }
                else
                {
                    Console.WriteLine("Beesy did not manage to collect enough nectar.");
                }
            }
            else 
            {
                Console.WriteLine("This is the end! Beesy ran out of energy.");
            }

            matrix[beePosition.Item1, beePosition.Item2] = 'B';

            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Console.Write(matrix[i, j]);
                }
                Console.WriteLine();
            }
        }
    }
}
