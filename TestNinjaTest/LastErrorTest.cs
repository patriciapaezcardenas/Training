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
    public class ErrorLoggerTest
    {
        private ErrorLogger _logger;

        [SetUp]
        public void SetUp()
        {
            _logger = new ErrorLogger();
        }

        [Test]
        public void Log_WhenCalled_SetTheLastErrorProperty()
        {
            //Act
            string error = "exception";
            _logger.Log(error);

            //Assert
            Assert.That(_logger.LastError, Is.EqualTo(error));
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void Log_InvalidError_ThrowArgumentNullException(string error)
        {
            //Act
            Assert.That(() => _logger.Log(error), Throws.ArgumentNullException);
            Assert.That(() => _logger.Log(error), Throws.Exception.TypeOf<ArgumentNullException>());
        }

        [Test]
        public void Log_ValidError_RaisesErrorLoggedEvent()
        {
            //Act
            Guid errorId = Guid.Empty;

            _logger.ErrorLogged += (sender, guid) =>
            {
                errorId = guid;
            };

            _logger.Log("Error here");

            //Assert
            Assert.That(errorId, Is.Not.Empty);
        }
    }
}
