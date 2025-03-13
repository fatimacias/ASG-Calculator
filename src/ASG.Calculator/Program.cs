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

            string sanitizedInput = input.Replace("\n", ",");
            string[] parts = sanitizedInput.Split(',');

            int sum = 0;
            List<int> negativeNumbers = new List<int>();
            foreach (var part in parts)
            {
                if (int.TryParse(part, out int number))
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
    }
}
