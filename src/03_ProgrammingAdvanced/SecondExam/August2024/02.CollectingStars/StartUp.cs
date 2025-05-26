using System;
using System.Collections.Generic;

namespace _02.CollectingStars
{
    public class StartUp
    {
        public static void Main()
        {
            int n = int.Parse(Console.ReadLine());

            var matrix = new char[n, n];
            var playerPosition = (-1, -1);
            var collectedStars = 2;

            for (int i = 0; i < n; i++)
            {
                string[] row = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);
                for (int j = 0; j < n; j++)
                {
                    matrix[i, j] = row[j][0];
                    if (matrix[i, j] == 'P')
                    {
                        playerPosition = (i, j);
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

            matrix[playerPosition.Item1, playerPosition.Item2] = '.';

            while (collectedStars > 0 && collectedStars < 10)
            {
                var command = Console.ReadLine();

                var move = directions[command];
                int newRow = playerPosition.Item1 + move.dx;
                int newCol = playerPosition.Item2 + move.dy;

                if (newRow < 0 || newRow >= n || newCol < 0 || newCol >= n)
                {
                    playerPosition = (0, 0);

                    if (matrix[0, 0] == '*')
                    {
                        collectedStars++;
                        matrix[0, 0] = '.';
                    }
                    else if (matrix[0, 0] == '#')
                    {
                        collectedStars--;
                    }
                    continue;
                }

                char cell = matrix[newRow, newCol];

                if (cell == '#')
                {
                    collectedStars--; 
                    continue;
                }

                if (cell == '*')
                {
                    collectedStars++;
                    matrix[newRow, newCol] = '.';
                }

                playerPosition = (newRow, newCol);
            }

            matrix[playerPosition.Item1, playerPosition.Item2] = 'P';

            if (collectedStars == 10)
            {
                Console.WriteLine("You won! You have collected 10 stars.");
            }
            else
            {
                Console.WriteLine("Game over! You are out of any stars.");
            }

            Console.WriteLine($"Your final position is [{playerPosition.Item1}, {playerPosition.Item2}]");

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Console.Write(matrix[i, j] + " ");
                }
                Console.WriteLine();
            }
        }
    }
}
