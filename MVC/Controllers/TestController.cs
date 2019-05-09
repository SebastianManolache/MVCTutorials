using MVC.Models;
using MVC.ViewModels;
using System.Web.Mvc;

namespace MVC.Controllers
{

    public class TestController : Controller
    {

        public Customer GetCustomer()
        {
            var customer = new Customer
            {
                Address = "Address1",
                CustomerName = "Customer 1"
            };
            return customer;
        }
        public ActionResult GetView()
        {
            var employee = new Employee
            {
                FirstName = "Sukesh",
                LastName = "Maria",
                Salary = 20000
            };
            var viewModelEmployee = new EmployeeViewModel();
            viewModelEmployee.EmployeeName = employee.FirstName + "  " + employee.LastName;
            viewModelEmployee.Salary = employee.Salary.ToString("C");
            if(employee.Salary>15000)
            {
                viewModelEmployee.SalaryColor = "yellow";
            }
            else
            {
                viewModelEmployee.SalaryColor = "green";
            }
            viewModelEmployee.UserName = "Admin";
            return View("MyView", viewModelEmployee);
        }
    }
}
