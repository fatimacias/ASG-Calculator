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
    }
}