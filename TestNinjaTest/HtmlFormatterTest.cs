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
    public class HtmlFormatterTest
    {
        private HtmlFormatter _htmlFormatter;

       [SetUp]
        public void SetUp()
        {
            _htmlFormatter = new HtmlFormatter();
        }

        [Test]
        public void FormatAsBold_WhenCalled_ShouldEncloseTheStringWithStrongElement()
        {
            //Arrange

            //Act
            var result = _htmlFormatter.FormatAsBold("my content");

            //Assert
            Assert.That(result,Is.EqualTo("<strong>my content</strong>").IgnoreCase);
            Assert.That(result,Does.StartWith("<strong>"));
            Assert.That(result, Does.EndWith("</strong>"));
            Assert.That(result, Does.Contain("my content"));
        }
    }
}
