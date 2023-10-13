using TestNinja.Fundamentals;

namespace TestNinjaTest
{
    using NUnit.Framework;

    [TestFixture]
    public class MathTest
    {
        private Math _math;

        [SetUp]
        public void SetUp()
        {
            _math = new Math(); // Arrange part.
        }

        [Test]
        public void Add_WhenCalled_SumOfArguments()
        {
            //var mathObject = new Math();

            var result = _math.Add(1, 2);

            Assert.That(result, Is.EqualTo(3));
        }

        [Test]
        [TestCase(1,2,2)]
        [TestCase(2, 1, 2)]
        [TestCase(3, 3,3)]
        public void Max_WhenCalled_ReturnsTheGreatestArgument(int valueOne, int valueTwo, int expectedResult)
        {
            //Act
            var result = _math.Max(valueOne, valueTwo);

            //Assert
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        public void GetOddNumbers_LimitIsGreaterThanZero_ReturnOddNumbersUpToLimit()
        {
            //Act
            var result = _math.GetOddNumbers(5);

            //Assert 
            Assert.That(result, Is.EquivalentTo(new [] {1,3,5}));
        }

        [Test]
        public void GetOddNumbers_LimitIsZero_ReturnEmptyResult()
        {
            //Act
            var result = _math.GetOddNumbers(0);

            //Assert 
            Assert.That(result, Is.Empty);
        }
    }
}
