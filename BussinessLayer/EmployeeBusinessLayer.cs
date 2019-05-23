using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using BusinessEntities;

namespace MVC.DataAccessLayer
{
    public class EmployeeBusinessLayer
    {
        public List<Employee> GetEmployees()
        {
            using (var db = new SalesDbContext())
            {
                var employees = db
                    .Employees
                    .ToListAsync().Result;
                return employees;
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

        public UserStatus GetUserValidity(UserDetails user)
        {
            if (user.UserName == "Admin" && user.Password == "Admin")
                return UserStatus.AuthenticatedAdmin;
            else if (user.UserName == "Seby" && user.Password == "Seby")
                return UserStatus.AuthentucatedUser;
            else
                return UserStatus.NonAuthenticatedUser;
        }
        public void UploadEmployees(List<Employee> employees)
        {
            var db = new SalesDbContext();
            db.Employees.AddRange(employees);
            db.SaveChanges();
        }
    }
}