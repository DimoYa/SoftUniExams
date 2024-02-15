using System.Net.Http.Headers;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.IO;

namespace SecondTask
{
    public class Program
    {
        public static void Main()
        {
            var numberOfLines = int.Parse(Console.ReadLine());
            string pattern = @"^\@#+(?<valid>[A-Z*][A-Za-z0-9]{4,}[A-Z])\@#+$";
            string digitPattern = @"\d+";
            var sb = new StringBuilder();


            for (int i = 0; i < numberOfLines; i++)
            {
                var input = Console.ReadLine();

                if (Regex.IsMatch(input, pattern))
                {

                    MatchCollection matches = Regex.Matches(input, digitPattern);
                    var result = new StringBuilder();

                    if (matches.Count > 0)
                    {
                        foreach (Match m in matches)
                        {
                            result.Append(m.Value);
                        }
                    }
                    else
                    {
                        result.AppendLine("00");
                    }

                    sb.AppendLine($"Product group: {result.ToString().TrimEnd()}");
                }
                else
                {
                    sb.AppendLine("Invalid barcode");
                }
            }

            Console.WriteLine(sb.ToString().TrimEnd());
        }
    }
}