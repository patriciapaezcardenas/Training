using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestNinja.Mocking;

namespace TestNinja.Employee
{
    public interface IEmployeeRepository
    {
        void DeleteEmployee(int id);
    }

    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmployeeContext _db;

        public EmployeeRepository()
        {
            _db = new EmployeeContext();
        }

        public void DeleteEmployee(int id)
        {
            var employee = _db.Employees.Find(id);
            if (employee is null) return;

            _db.Employees.Remove(employee);
            _db.SaveChanges();
        }
    }
}
