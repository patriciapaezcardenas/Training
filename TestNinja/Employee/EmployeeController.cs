using System.Data.Entity;
using TestNinja.Employee;

namespace TestNinja.Mocking
{
    public class EmployeeController
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
           _employeeRepository = employeeRepository;
        }

        public ActionResult DeleteEmployee(int id)
        {
            _employeeRepository.DeleteEmployee(id);
            return RedirectToAction("Employees");
        }

        private ActionResult RedirectToAction(string employees)
        {
            return new RedirectResult();
        }
    }

    public class ActionResult { }
 
    public class RedirectResult : ActionResult { }
    
    public class EmployeeContext
    {
        public DbSet<Employee> Employees { get; set; }

        public void SaveChanges()
        {
        }
    }

    public class Employee
    {
    }
}