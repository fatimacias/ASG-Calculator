namespace ASG.Calculator.Tests
{
    public class CalculatorTests
    {
        [Fact]
        public void AddNumbers_EmptyInput_ReturnsZero()
        {
            string input = "";
            int result = Program.AddNumbers(input);
            Assert.Equal(0, result);
        }
        [Fact]
        public void AddNumbers_SingleNumber_ReturnsNumber()
        {
            string input = "20";
            int result = Program.AddNumbers(input);
            Assert.Equal(20, result);
        }

        [Fact]
        public void AddNumbers_TwoValidNumbers_ReturnsSum()
        {
            string input = "1,5000";
            int result = Program.AddNumbers(input);
            Assert.Equal(1, result);
        }

        [Fact]
        public void AddNumbers_NegativeNumber_ReturnsCorrectSum()
        {
            string input = "4,-3";
            var exception = Assert.Throws<Exception>(() => Program.AddNumbers(input));
            Assert.Equal("Negative numbers are not allowed: -3", exception.Message);

        }

        [Fact]
        public void AddNumbers_InvalidNumber_ReturnsSumWithZero()
        {
            string input = "5,tytyt";
            int result = Program.AddNumbers(input);
            Assert.Equal(5, result);
        }

        [Fact]
        public void AddNumbers_TooManyNumbers_ReturnsSum()
        {
            string input = "1,2,3,4,5,6,7,8,9,10,11,12";
            int result = Program.AddNumbers(input);
            Assert.Equal(78, result);
        }

        [Fact]
        public void AddNumbers_MissingNumber_ReturnsZero()
        {
            string input = ",";
            int result = Program.AddNumbers(input);
            Assert.Equal(0, result);
        }
        [Fact]
        public void AddNumbers_ValidNumbersWithNewLineAndComma_ReturnsCorrectSum()
        {
            string input = "1\n2,3";
            int result = Program.AddNumbers(input);
            Assert.Equal(6, result);  // 1 + 2 + 3 = 6
        }
        [Fact]
        public void AddNumbers_NegativeNumbers_ThrowsException()
        {
            string input = "1,-2,3";
            var exception = Assert.Throws<Exception>(() => Program.AddNumbers(input));
            Assert.Equal("Negative numbers are not allowed: -2", exception.Message);
        }
        [Fact]
        public void AddNumbers_NumbersGreaterThan1000_ReturnsCorrectSum()
        {
            string input = "2,1001,6";
            int result = Program.AddNumbers(input);
            Assert.Equal(8, result);  
        }
        [Theory]
        [InlineData("//,\n2,ff,100", 102)]
        [InlineData("//#\n2#5", 7)]
        [InlineData("//#\n2#5#8\n10", 25)]
        [InlineData("//[***]\n11***22***33", 66)]
        [InlineData("//[#]\n2#5#8\n10", 25)]
        [InlineData("//[*][!!][r9r]\n11r9r22*hh*33!!44", 110)]
        public void AddNumbers_InvalidNumberWithCustomDelimiter_ReturnsCorrectSum(string input, int expected)
        {
            int result = Program.AddNumbers(input);
            Assert.Equal(expected, result);
        }


    }
}