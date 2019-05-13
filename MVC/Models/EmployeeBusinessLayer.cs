using System.Collections.Generic;
using System.Linq;
using MVC.DataAccessLayer;


namespace MVC.Models
{
    public class EmployeeBusinessLayer
    {
        public List<Employee> GetEmployees()
        {
            using (var salesDal = new SalesContext())
            {
                return salesDal.Employees.ToList();
            }
        }
        public Employee SaveEmployee(Employee e)
        {
            var salesDal = new SalesContext();
            salesDal.Employees.Add(e);
            salesDal.SaveChanges();
            return e;
        }
    }
}
