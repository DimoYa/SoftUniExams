namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int width = Console.WindowWidth;

            void WriteCentered(string text, ConsoleColor color)
            {
                Console.ForegroundColor = color;
                int leftPadding = (width - text.Length) / 2;
                Console.WriteLine(new string(' ', Math.Max(0, leftPadding)) + text);
                Console.ResetColor();
            }

            WriteCentered("  /\\_/\\  ", ConsoleColor.Yellow);
            WriteCentered(" ( o.o )  Meow~", ConsoleColor.Cyan);
            WriteCentered("  > ^ <   ", ConsoleColor.Magenta);
            Console.WriteLine();
            WriteCentered("Happy Birthday, Pepi!", ConsoleColor.Green);
            WriteCentered("<< Celebrate with joy! >>", ConsoleColor.DarkYellow);
        }
    }
}
