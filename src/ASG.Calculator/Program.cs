using System.IO;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace ASG.Calculator
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Fatima Macias");
            Console.WriteLine("Enter numbers with an optional custom delimiter in the format: //{delimiter}\n{numbers}");
            string input = Console.ReadLine();
            try
            {
                int result = AddNumbers(input);
                Console.WriteLine($"Result: {result}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
        public static int AddNumbers(string input)
        {
            if (string.IsNullOrEmpty(input))
                return 0;
            
            string[] parts = SanitizedInput(input);

            int sum = 0;
            List<int> negativeNumbers = [];
            foreach (var part in parts)
            {
                if (int.TryParse(part, out int number) && number<=1000)
                {
                    if(number>0)
                    {
                        sum += number;
                    }
                    else
                    {
                        negativeNumbers.Add(number);
                    }
                }
            }
            if(negativeNumbers.Count>0)
            {
                throw new Exception($"Negative numbers are not allowed: {string.Join(", ", negativeNumbers)}");
            }
            return sum;
        }
        static string[] SanitizedInput(string input)
        {
            //Default demiliters
            List<string> delimiters = [",", "\n"];
            string sanitizedInput = input;

            if (input.StartsWith("//"))
            {
                var customDelimiterMatch = Regex.Match(input, @"^//(.*?)\n(.*)", RegexOptions.Singleline);
                if (customDelimiterMatch.Success)
                {
                    string delimiterPart = customDelimiterMatch.Groups[1].Value;
                    sanitizedInput = customDelimiterMatch.Groups[2].Value;

                    // Multiple delimiters of any length
                    var multiDelimiterMatches = Regex.Matches(delimiterPart, @"\[(.*?)]");
                    if (multiDelimiterMatches.Count > 0)
                    {
                        foreach (Match m in multiDelimiterMatches.Cast<Match>())
                        {
                            delimiters.Add(m.Groups[1].Value);
                        }
                    }
                    else
                    {
                        // Single custom delimiter
                        delimiters.Add(delimiterPart);
                    }
                }
            }
            return SplitNumbers(sanitizedInput, delimiters);

        }
        private static string[] SplitNumbers(string numbers, List<string> delimiters)
        {
            string pattern = string.Join("|", delimiters.Select(Regex.Escape));
            return Regex.Split(numbers, pattern);
        }
    }
    
}
