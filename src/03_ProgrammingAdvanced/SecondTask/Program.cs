using System;
using System.Collections.Generic;

namespace SecondTask
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());

            var matrix = new char[n, n];
            var spaceshipPosition = (-1, -1);
            var resources = 100;
            var missionAccomplished = false;

            for (int i = 0; i < n; i++)
            {
                string[] row = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);
                for (int j = 0; j < n; j++)
                {
                    matrix[i, j] = row[j][0];
                    if (matrix[i, j] == 'S')
                    {
                        spaceshipPosition = (i, j);
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

            matrix[spaceshipPosition.Item1, spaceshipPosition.Item2] = '.';
            while (true)
            {
                var command = Console.ReadLine();

                (int dx, int dy) move = directions[command];
                int newRow = spaceshipPosition.Item1 + move.dx;
                int newCol = spaceshipPosition.Item2 + move.dy;

                // Check if spaceship moves out of bounds
                if (newRow < 0 || newRow >= n || newCol < 0 || newCol >= n)
                {
                    Console.WriteLine("Mission failed! The spaceship was lost in space.");
                    break;
                }

                var newPosition = matrix[newRow, newCol];
                resources -= 5;

                if (newPosition == 'M')
                {
                    resources -= 5;
                    matrix[newRow, newCol] = '.';
                }
                else if (newPosition == 'R')
                {
                    resources = Math.Min(100, resources + 10);
                }
                else if (newPosition == 'P')
                {
                    Console.WriteLine($"Mission accomplished! The spaceship reached Planet Eryndor with {resources} resources left.");
                    missionAccomplished = true;
                    break;
                }

                spaceshipPosition = (newRow, newCol);

                if (resources < 5)
                {
                    Console.WriteLine("Mission failed! The spaceship was stranded in space.");
                    break;
                }
            }

            if (!missionAccomplished)
            {
                matrix[spaceshipPosition.Item1, spaceshipPosition.Item2] = 'S';
            }

            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Console.Write(matrix[i, j] + " ");
                }
                Console.WriteLine();
            }
        }
    }
}
