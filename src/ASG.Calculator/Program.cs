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
            Console.WriteLine("String Calculator - Enter input strings to calculate sums. Press Ctrl + C to exit.");
            Console.WriteLine("----------------------------------------------------------");

            while (true)
            {
                Console.Write("\nEnter input: ");
                string input = Console.ReadLine();

                try
                {
                    string result = AddNumbers(input);
                    Console.WriteLine($"Result: {result}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
            
        }
        public static string AddNumbers(string input)
        {
            if (string.IsNullOrEmpty(input))
                return "0";
            
            string[] parts = SanitizedInput(input);
            List<int> numbers = GetValidNumbers(parts);
            return $"{string.Join('+', numbers)}={numbers.Sum()}";
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
        private static List<int> GetValidNumbers(string[] numbers)
        {
            var validNumbers = new List<int>();
            List<int> negativeNumbers = [];
            foreach (var numStr in numbers)
            {
                if (int.TryParse(numStr, out int number))
                {
                    if (number > 1000)
                        number = 0;
                    else if(number < 0)
                        negativeNumbers.Add(number);
                    validNumbers.Add(number);
                }
                else
                {
                    validNumbers.Add(0); 
                }
            }
            if (negativeNumbers.Count > 0)
            {
                throw new Exception($"Negative numbers are not allowed: {string.Join(", ", negativeNumbers)}");
            }
            return validNumbers;
        }
    }
    
}
