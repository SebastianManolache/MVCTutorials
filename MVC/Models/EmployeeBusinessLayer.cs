using System.Collections.Generic;

namespace MVC.Models
{
    public class EmployeeBusinessLayer
    {
        public List<Employee> GetEmployees()
        {
            List<Employee>  employees = new List<Employee> ();
            var emp = new Employee
            {
                FirstName = "johnswon",
                LastName = "fernandes",
                Salary = 14000
            };
            employees.Add(emp);
            emp = new Employee
            {
                FirstName = "michael",
                LastName = "jackson",
                Salary = 16000
            };
            employees.Add(emp);
             emp = new Employee
            {
                FirstName = "robert",
                LastName = "pattinson",
                Salary = 20000
            };
            employees.Add(emp);
            return employees;
        }
    }
}
