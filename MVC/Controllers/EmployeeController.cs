using MVC.Models;
using MVC.ViewModels;
using System.Collections.Generic;
using System.Web.Mvc;

namespace MVC.Controllers
{

    public class EmployeeController : Controller
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
        public ActionResult Index()
        {
            var employeeListViewModel = new EmployeeListViewModel();
            var employeeBusinessLayer = new EmployeeBusinessLayer();
            var employees = employeeBusinessLayer.GetEmployees();
            var empViewModels = new List<EmployeeViewModel>();

            employees.ForEach(employee =>
            {
                var empViewModel = new EmployeeViewModel();
                empViewModel.EmployeeName = employee.FirstName + " " + employee.LastName;
                empViewModel.Salary = employee.Salary.ToString("C");
                if (employee.Salary > 15000)
                {
                    empViewModel.SalaryColor = "yellow";
                }
                else
                {
                    empViewModel.SalaryColor = "green";
                }
                empViewModels.Add(empViewModel);
            });
            employeeListViewModel.Employees = empViewModels;
            return View("Index", employeeListViewModel);
        }
        public ActionResult AddNew()
        {
            return View("CreateEmployee");
        }
    }
}
