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
            Assert.Equal(5001, result);
        }

        [Fact]
        public void AddNumbers_NegativeNumber_ReturnsCorrectSum()
        {
            string input = "4,-3";
            int result = Program.AddNumbers(input);
            Assert.Equal(1, result);
        }

        [Fact]
        public void AddNumbers_InvalidNumber_ReturnsSumWithZero()
        {
            string input = "5,tytyt";
            int result = Program.AddNumbers(input);
            Assert.Equal(5, result);
        }

        [Fact]
        public void AddNumbers_TooManyNumbers_ThrowsException()
        {
            string input = "1,2,3";
            var exception = Assert.Throws<Exception>(() => Program.AddNumbers(input));
            Assert.Equal("You can only provide a maximum of two numbers.", exception.Message);
        }

        [Fact]
        public void AddNumbers_MissingNumber_ReturnsZero()
        {
            string input = ",";
            int result = Program.AddNumbers(input);
            Assert.Equal(0, result);
        }
    }
}