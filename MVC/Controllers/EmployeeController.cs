using MVC.DataAccessLayer;
using MVC.Models;
using MVC.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
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

        public async Task<ActionResult> Index()
        {
            var employeeListViewModel = new EmployeeListViewModel();
            var employeeBusinessLayer = new EmployeeBusinessLayer();
            var employees = await employeeBusinessLayer.GetEmployeesAsync();
            var empViewModels = new List<EmployeeViewModel>();

            employees.ForEach(employee =>
            {
                var empViewModel = new EmployeeViewModel
                {
                    EmployeeName = employee.FirstName + " " + employee.LastName,
                    Salary = employee.Salary.ToString("C")
                };
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
            return View("CreateEmployee", new CreateEmployeeViewModel());
        }

        public async Task<ActionResult> SaveEmployee(Employee employee, string BtnSubmit)
        {
            switch (BtnSubmit)
            {
                case "Save Employee":
                    if (ModelState.IsValid)
                    {
                        var employeeBusinessLayer = new EmployeeBusinessLayer();
                        await employeeBusinessLayer.SaveEmployeeAsync(employee);
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        var viewModel = new CreateEmployeeViewModel();
                        viewModel.FirstName = employee.FirstName;
                        viewModel.LastName = employee.LastName;
                        if (employee.Salary.GetType() == typeof(int))
                        {
                            viewModel.Salary = employee.Salary.ToString();
                        }
                        else
                        {
                            viewModel.Salary = ModelState["Salary"].Value.AttemptedValue;
                        }

                        return View("CreateEmployee", viewModel);
                    }

                case "Cancel":
                    return RedirectToAction("Index");
            }

            return new EmptyResult();
        }
    }
}
