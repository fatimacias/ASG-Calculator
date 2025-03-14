using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASG.Calculator
{
    public class CalculatorSettings
    {
        public string AlternateDelimiter { get; set; } = "\n"; // Default as per your constructor
        public bool AllowNegatives { get; set; } = false;
        public int UpperBound { get; set; } = 1000;
        public string Operation { get; set; } = "add";

        public static CalculatorSettings FromArgs(string[] args)
        {
            var settings = new CalculatorSettings();

            foreach (var arg in args)
            {
                if (arg.StartsWith("--delimiter="))
                    settings.AlternateDelimiter = arg["--delimiter=".Length..];
                else if (arg == "--allow-negatives")
                    settings.AllowNegatives = true;
                else if (arg.StartsWith("--upper-bound=") && int.TryParse(arg["--upper-bound=".Length..], out int ub))
                    settings.UpperBound = ub;
                else if (arg.StartsWith("--operation="))
                    settings.Operation = arg["--operation=".Length..].ToLower();
            }

            return settings;
        }
    }
}
