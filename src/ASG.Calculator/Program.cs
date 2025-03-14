using ASG.Calculator.Contracts;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace ASG.Calculator
{
    public class Program
    {

        static void Main(string[] args)
        {
            var settings = CalculatorSettings.FromArgs(args);

            // Set up DI
            var services = new ServiceCollection();
            services.AddSingleton(settings); 
            services.AddTransient<IStringCalculator, StringCalculator>();
            var provider = services.BuildServiceProvider();
            // Resolve StringCalculator via DI
            var calculator = provider.GetRequiredService<IStringCalculator>();

            Console.WriteLine("Fatima Macias");
            Console.WriteLine("String Calculator - Enter input strings to calculate sums. Press Ctrl + C to exit.");
            Console.WriteLine($"Settings -> Alternate Delimiter: {settings.AlternateDelimiter ?? "None"}, Allow Negatives: {settings.AllowNegatives}, Upper Bound: {settings.UpperBound}");
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
