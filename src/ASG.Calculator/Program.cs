using System.IO;
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
            string sanitizedInput = input;
            string delimiter = ",";
            if (input.StartsWith("//"))
            {
                string expression = input.StartsWith("//[") ? @"^//\[(.*?)\]\n" : @"^//.(?=\n)";

                var match = Regex.Match(input, expression);
                if (!match.Success)
                {
                    throw new Exception("Invalid input format: missing delimiter or newline.");
                }
                delimiter = input.StartsWith("//[") ? match.Groups[1].Value : match.Value.Substring(2, 1);
                sanitizedInput = input[match.Length..];
            }
            sanitizedInput = sanitizedInput.Replace("\n", delimiter);
            return sanitizedInput.Split(new[] { delimiter }, StringSplitOptions.None);
        }
    }
}
