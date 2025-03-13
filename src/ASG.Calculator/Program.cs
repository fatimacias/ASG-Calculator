namespace ASG.Calculator
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Fatima Macias");
            Console.WriteLine("Enter numbers separated by a coma");
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

            string[] parts = input.Split(',');

            int sum = 0;
            foreach (var part in parts)
            {
                if (int.TryParse(part, out int number))
                    sum += number;
            }

            return sum;
        }
    }
}
