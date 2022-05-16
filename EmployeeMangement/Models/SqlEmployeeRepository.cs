using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeMangement.Models
{
    public class SqlEmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _DbContext;

        public SqlEmployeeRepository(AppDbContext context)
        {
            _DbContext = context;
        }
        public Employee Add(Employee employee)
        {
            _DbContext.Add(employee);
            _DbContext.SaveChanges();
            return employee;
        }

        public Employee Delete(int id)
        {
            Employee employee = _DbContext.Employees.Find(id);
            if(employee != null)
            {
                _DbContext.Employees.Remove(employee);
                _DbContext.SaveChanges();
            }

            return employee;
        }

        public Employee GetEmployee(int id)
        {
            return _DbContext.Employees.Find(id);
        }

        public IEnumerable<Employee> GetEmployees()
        {
            return _DbContext.Employees;
        }

        public Employee Update(Employee employeeChanges)
        {
            var emp = _DbContext.Employees.Attach(employeeChanges);

            emp.State = EntityState.Modified;
            _DbContext.SaveChanges();

            return employeeChanges;
        }
    }
}
