namespace POC.XUnit
{
    public class UnitTest1
    {
       // [Fact]
        [Theory]
        [InlineData(-1,5)]
        [InlineData(3,2)]
        [InlineData(3,41)]
        [InlineData(2,1)]
        public void Test1(int value,int value2)
        {
            var math = new MathHelper();

            // Act
            var result = math.Add(value, value2);

            // Assert
            Assert.Equal(5, result);
        }
    }

    public class MathHelper
    {
        public int Add(int a, int b) => a + b;
    }
}