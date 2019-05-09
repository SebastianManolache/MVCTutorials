using MVC.Models;
using MVC.ViewModels;
using System.Collections.Generic;
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
            var employeeListViewModel = new EmployeeListViewModel();
            var emBal = new EmployeeBusinessLayer();
            var employees = emBal.GetEmployees();
            List<EmployeeViewModel>  empViewModels = new List<EmployeeViewModel> ();

            foreach (Employee emp in employees)
            {
                var empViewModel = new EmployeeViewModel();
                empViewModel.EmployeeName = emp.FirstName + " " + emp.LastName;
                empViewModel.Salary = emp.Salary.ToString("C");
                if (emp.Salary > 15000)
                {
                    empViewModel.SalaryColor = "yellow";
                }
                else
                {
                    empViewModel.SalaryColor = "green";
                }
                empViewModels.Add(empViewModel);
            }
            employeeListViewModel.Employees = empViewModels;
            employeeListViewModel.UserName = "Admin";
            return View("MyView", employeeListViewModel);
           
        }
    }
}
