using System.IO;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace ASG.Calculator
{
    public class Program
    {

        static void Main(string[] args)
        {
            string alternateDelimiter = null;
            bool allowNegatives = false;
            int upperBound = 1000;
            foreach (var arg in args)
            {
                if (arg.StartsWith("--delimiter="))
                    alternateDelimiter = arg.Substring("--delimiter=".Length);
                else if (arg == "--allow-negatives")
                    allowNegatives = true;
                else if (arg.StartsWith("--upper-bound=") && int.TryParse(arg.Substring("--upper-bound=".Length), out int ub))
                    upperBound = ub;
            }
            var calculator = new StringCalculator(alternateDelimiter, allowNegatives, upperBound);

            Console.WriteLine("Fatima Macias");
            Console.WriteLine("String Calculator - Enter input strings to calculate sums. Press Ctrl + C to exit.");
            Console.WriteLine($"Settings -> Alternate Delimiter: {alternateDelimiter ?? "None"}, Allow Negatives: {allowNegatives}, Upper Bound: {upperBound}");
            Console.WriteLine("----------------------------------------------------------");

            while (true)
            {
                Console.Write("\nEnter input: ");
                string input = Console.ReadLine();

                try
                {
                    string result = calculator.AddNumbers(input);
                    Console.WriteLine($"Result: {result}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
            
        }
       
    }
    
}
