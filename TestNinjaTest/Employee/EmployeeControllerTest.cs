using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using TestNinja.Employee;
using TestNinja.Mocking;

namespace TestNinjaTest.Employee
{
    [TestFixture]
    public class EmployeeControllerTest
    {
        [Test]
        public void DeleteEmployee_WhenCalled_DeleteEmployeeFromDb()
        {
            var mockEmployeeRepo = new Mock<IEmployeeRepository>();
            var controller = new EmployeeController(mockEmployeeRepo.Object);

            controller.DeleteEmployee(1);
            mockEmployeeRepo.Verify(r=> r.DeleteEmployee(1));
        }

        [Test]
        public void DeleteEmployee_WhenCalled_ReturnsRedirectResult()
        {
            var mockEmployeeRepo = new Mock<IEmployeeRepository>();
            var controller = new EmployeeController(mockEmployeeRepo.Object);

           var result =  controller.DeleteEmployee(1);
            Assert.That(result,Is.TypeOf<RedirectResult>());
        }
    }
}
