using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinjaTest
{
    [TestFixture]
    public class FizzBuzzTest
    {
        [Test]
        [TestCase(15)]
        public void GetOutput_NumberIsDivisibleBy3And5_ReturnFizzBuzz(int number)
        {
            //Act
            var result = FizzBuzz.GetOutput(number);

            //Assert
            Assert.That(result,Is.EqualTo("fizzbuzz").IgnoreCase);
        }

        [Test]
        [TestCase(9)]
        public void GetOutput_NumberIsDivisibleBy3Only_ReturnFizz(int number)
        {
            //Act
            var result = FizzBuzz.GetOutput(number);

            //Assert
            Assert.That(result, Is.EqualTo("fizz").IgnoreCase);
        }

        [Test]
        [TestCase(10)]
        public void GetOutput_NumberIsDivisibleBy5Only_ReturnBuzz(int number)
        {
            //Act
            var result = FizzBuzz.GetOutput(number);

            //Assert
            Assert.That(result, Is.EqualTo("buzz").IgnoreCase);
        }


        [Test]
        [TestCase(2)]
        public void GetOutput_NumberIsNotDivisibleBy3Or5_ReturnBuzz(int number)
        {
            //Act
            var result = FizzBuzz.GetOutput(number);

            //Assert
            Assert.That(result, Is.EqualTo(number.ToString()).IgnoreCase);
        }
    }
}
