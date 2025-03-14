﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ASG.Calculator
{
    public class StringCalculator(string alternateDelimiter = "\n", bool allowNegatives = false, int upperBound = 1000)
    {
        private readonly List<string> DefaultDelimiters = new List<string> { ",", "\n" };
        private readonly string _alternateDelimiter = alternateDelimiter;
        private readonly bool _allowNegatives = allowNegatives;
        private readonly int _upperBound = upperBound;

        public string AddNumbers(string input)
        {
            if (string.IsNullOrEmpty(input))
                return "0";

            string[] parts = SanitizedInput(input);
            List<int> numbers = GetValidNumbers(parts);
            return $"{string.Join('+', numbers)}={numbers.Sum()}";
        }
        private string[] SanitizedInput(string input)
        {
            //Default demiliters
            List<string> delimiters = [",", "\n"];
            if (!string.IsNullOrEmpty(_alternateDelimiter))
                delimiters.Add(_alternateDelimiter);
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
        private string[] SplitNumbers(string numbers, List<string> delimiters)
        {
            string pattern = string.Join("|", delimiters.Select(Regex.Escape));
            return Regex.Split(numbers, pattern);
        }
        private List<int> GetValidNumbers(string[] numbers)
        {
            var validNumbers = new List<int>();
            List<int> negativeNumbers = [];
            foreach (var numStr in numbers)
            {
                if (int.TryParse(numStr, out int number))
                {
                    if (number > _upperBound)
                        number = 0;
                    else if (number < 0)
                        negativeNumbers.Add(number);
                    validNumbers.Add(number);
                }
                else
                {
                    validNumbers.Add(0);
                }
            }
            if (!_allowNegatives && negativeNumbers.Count > 0)
            {
                throw new Exception($"Negative numbers are not allowed: {string.Join(", ", negativeNumbers)}");
            }
            return validNumbers;
        }

    }
}
