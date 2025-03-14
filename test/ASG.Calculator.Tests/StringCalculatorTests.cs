using ASG.Calculator.Contracts;

namespace ASG.Calculator.Tests
{
    public class StringCalculatorTests
    {
        [Fact]
        public void AddNumbers_EmptyInput_ReturnsZero()
        {
            var calc = new StringCalculator();
            string input = "";
            var result = calc.AddNumbers(input);
            Assert.Equal("0", result);
        }
        [Fact]
        public void AddNumbers_SingleNumber_ReturnsNumber()
        {
            var calc = new StringCalculator();
            string input = "20";
            var result = calc.AddNumbers(input);
            Assert.Equal("20=20", result);
        }

        [Fact]
        public void AddNumbers_TwoValidNumbers_ReturnsSum()
        {
            var calc = new StringCalculator();
            string input = "1,5000";
            var result = calc.AddNumbers(input);
            Assert.Equal("1+0=1", result);
        }

        [Fact]
        public void AddNumbers_NegativeNumber_ReturnsCorrectSum()
        {
            var calc = new StringCalculator();
            string input = "4,-3";
            var exception = Assert.Throws<Exception>(() => calc.AddNumbers(input));
            Assert.Equal("Negative numbers are not allowed: -3", exception.Message);

        }

        [Fact]
        public void AddNumbers_InvalidNumber_ReturnsSumWithZero()
        {
            var calc = new StringCalculator();
            string input = "5,tytyt";
            var result = calc.AddNumbers(input);
            Assert.Equal("5+0=5", result);
        }

        [Fact]
        public void AddNumbers_TooManyNumbers_ReturnsSum()
        {
            var calc = new StringCalculator();
            string input = "1,2,3,4,5,6,7,8,9,10,11,12";
            var result = calc.AddNumbers(input);
            Assert.Equal("1+2+3+4+5+6+7+8+9+10+11+12=78", result);
        }

        [Fact]
        public void AddNumbers_MissingNumber_ReturnsZero()
        {
            var calc = new StringCalculator();
            string input = ",";
            var result = calc.AddNumbers(input);
            Assert.Equal("0+0=0", result);
        }
        [Fact]
        public void AddNumbers_ValidNumbersWithNewLineAndComma_ReturnsCorrectSum()
        {
            var calc = new StringCalculator();
            string input = "1\n2,3";
            var result = calc.AddNumbers(input);
            Assert.Equal("1+2+3=6", result);  // 1 + 2 + 3 = 6
        }
        [Fact]
        public void AddNumbers_NegativeNumbers_ThrowsException()
        {
            var calc = new StringCalculator();
            string input = "1,-2,3";
            var exception = Assert.Throws<Exception>(() => calc.AddNumbers(input));
            Assert.Equal("Negative numbers are not allowed: -2", exception.Message);
        }
        [Fact]
        public void AddNumbers_NumbersGreaterThan1000_ReturnsCorrectSum()
        {
            var calc = new StringCalculator();
            string input = "2,1001,6";
            var result = calc.AddNumbers(input);
            Assert.Equal("2+0+6=8", result);  
        }
        [Theory]
        [InlineData("//,\n2,ff,100", "2+0+100=102")]
        [InlineData("//#\n2#5", "2+5=7")]
        [InlineData("//#\n2#5#8\n10", "2+5+8+10=25")]
        [InlineData("//[***]\n11***22***33", "11+22+33=66")]
        [InlineData("//[#]\n2#5#8\n10", "2+5+8+10=25")]
        [InlineData("//[*][!!][r9r]\n11r9r22*hh*33!!44", "11+22+0+33+44=110")]
        public void AddNumbers_InvalidNumberWithCustomDelimiter_ReturnsCorrectSum(string input, string expected)
        {
            var calc = new StringCalculator();
            var result = calc.AddNumbers(input);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Add_NumberGreaterThanUpperBound_Ignored()
        {
            var calc = new StringCalculator(upperBound:2000);
            var result = calc.AddNumbers("2,1001,6");
            Assert.Equal("2+1001+6=1009", result);
        }
        [Fact]
        public void Add_CustomSingleCharDelimiter_ReturnsSum()
        {
            var calc = new StringCalculator(";");
            var result = calc.AddNumbers("1;2;3");
            Assert.Equal("1+2+3=6", result);
        }
        [Fact]
        public void Add_AllowNegatives_DoesNotThrow()
        {
            var calc = new StringCalculator(allowNegatives: true);
            var result = calc.AddNumbers("1,-2,3");
            Assert.Equal("1+-2+3=2", result);
        }

        [Fact]
        public void AddNumbers_SimpleSum_UsingInterface()
        {
            var settings = new CalculatorSettings();
            IStringCalculator calculator = new StringCalculator(settings);

            var result = calculator.AddNumbers("1,2,3");

            Assert.Equal("1+2+3=6", result);
        }
        [Fact]
        public void DivisionByZero_ThrowsException()
        {
            var settings = new CalculatorSettings { Operation = "div" };
            var calculator = new StringCalculator(settings);

            var ex = Assert.Throws<DivideByZeroException>(() => calculator.AddNumbers("10,0,2"));
            Assert.Equal("Cannot divide by zero.", ex.Message);
        }
        [Fact]
        public void Subtraction_ReturnsCorrectResult()
        {
            var settings = new CalculatorSettings { Operation = "sub" };
            var calculator = new StringCalculator(settings);

            var result = calculator.AddNumbers("10,3,2");

            Assert.Equal("10-3-2=5", result);
        }

        [Fact]
        public void Multiplication_ReturnsCorrectResult()
        {
            var settings = new CalculatorSettings { Operation = "mul" };
            var calculator = new StringCalculator(settings);

            var result = calculator.AddNumbers("2,3,4");

            Assert.Equal("2*3*4=24", result);
        }

        [Fact]
        public void Division_ReturnsCorrectResult()
        {
            var settings = new CalculatorSettings { Operation = "div" };
            var calculator = new StringCalculator(settings);

            var result = calculator.AddNumbers("100,2,5");

            Assert.Equal("100/2/5=10", result);
        }
    }
}