namespace Gymnastics
{
    public class StartUp
    {
        public static void Main()
        {
            // Russia
            var ribbonLabourRussia = 9.1;
            var ribbonTaskRussia = 9.4;
            var hoopLabourRussia = 9.3;
            var hoopTaskRussia = 9.8;
            var ropeLabourRussia = 9.6;
            var ropeTaskRussia = 9.0;

            // Bulgaria
            var ribbonLabourBulgaria = 9.6;
            var ribbonTaskBulgaria = 9.4;
            var hoopLabourBulgaria = 9.55;
            var hoopTaskBulgaria = 9.75;
            var ropeLabourBulgaria = 9.5;
            var ropeTaskBulgaria = 9.4;

            // Italy
            var ribbonLabourItaly = 9.2;
            var ribbonTaskItaly = 9.5;
            var hoopLabourItaly = 9.45;
            var hoopTaskItaly = 9.35;
            var ropeLabourItaly = 9.7;
            var ropeTaskItaly = 9.15;
            double total = 0;

            var country = Console.ReadLine();
            var tool = Console.ReadLine();

            if (country == "Russia")
            {
                if (tool == "ribbon")
                {
                    total = ribbonLabourRussia + ribbonTaskRussia;
                }
                else if (tool == "hoop")
                {
                    total = hoopLabourRussia + hoopTaskRussia;
                }
                else if (tool == "rope")
                {
                    total = ropeLabourRussia + ropeTaskRussia;
                }
            }
            else if (country == "Bulgaria")
            {
                if (tool == "ribbon")
                {
                    total = ribbonLabourBulgaria + ribbonTaskBulgaria;
                }
                else if (tool == "hoop")
                {
                    total = hoopLabourBulgaria + hoopTaskBulgaria;
                }
                else if (tool == "rope")
                {
                    total = ropeLabourBulgaria + ropeTaskBulgaria;
                }
            }
            else if (country == "Italy")
            {
                if (tool == "ribbon")
                {
                    total = ribbonLabourItaly + ribbonTaskItaly;
                }
                else if (tool == "hoop")
                {
                    total = hoopLabourItaly + hoopTaskItaly;
                }
                else if (tool == "rope")
                {
                    total = ropeLabourItaly + ropeTaskItaly;
                }
            }

            var difference = 20 - total;

            Console.WriteLine($"The team of {country} get {total:F3} on {tool}.\r\n{(difference / 20) * 100:F2}%");
        }
    }
}
