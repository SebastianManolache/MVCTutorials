using MVC.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace MVC.DataAccessLayer
{
    public class EmployeeBusinessLayer
    {
        public async Task<List<Employee>> GetEmployeesAsync()
        {
            using (var db = new SalesDbContext())
            {
                return await db
                    .Employees
                    .ToListAsync();
            }
        }
        public async Task<Employee> SaveEmployeeAsync(Employee employee)
        {
            using (var db = new SalesDbContext())
            {
                db.Employees.Add(employee);
                await db.SaveChangesAsync();
            }
            return employee;
        }

        public bool IsValidUser(UserDetails user)
        {
            if (user.UserName == "Admin" && user.Password == "Admin")
            {
                return true;
            }
            else { return false; }
        }
    }
}
