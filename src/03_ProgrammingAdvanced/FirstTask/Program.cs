namespace FirstTask
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var suggestedLink =
                new Queue<int>(Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse)); // fifo

            var featuredArticles =
               new Stack<int>(Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse)); // lifo


            var targetEngValue = int.Parse(Console.ReadLine());

            var finalFeed = new List<int>();

            while (suggestedLink.Any() && featuredArticles.Any())
            {
                var currentSuggestion = suggestedLink.Dequeue();
                var currentFeature = featuredArticles.Pop();

                var greater = Math.Max(currentFeature, currentSuggestion);
                var smaller = Math.Min(currentFeature, currentSuggestion);

                var remainder = greater % smaller;

                if (greater == currentFeature)
                {
                    finalFeed.Add(remainder);
                    if (remainder > 0)
                    {
                        featuredArticles.Push(remainder * 2);
                    }
                }
                else if (greater == currentSuggestion)
                {
                    finalFeed.Add(-remainder);
                    if (remainder > 0)
                    {
                        suggestedLink.Enqueue(remainder * 2);
                    }
                }
                else
                {
                    finalFeed.Add(0);
                }

            }
            var totalEngValue = finalFeed.Sum();
            Console.WriteLine($"Final Feed: {string.Join(", ", finalFeed)}");

            if (totalEngValue >= targetEngValue)
            {
                Console.WriteLine($"Goal achieved! Engagement Value: {totalEngValue}");
            }
            else
            {
                Console.WriteLine($"Goal not achieved! Short by: {targetEngValue - totalEngValue}");
            }
        }
    }
}
