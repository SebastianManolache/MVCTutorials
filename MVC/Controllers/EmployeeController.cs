using MVC.DataAccessLayer;
using MVC.Filters;
using MVC.Models;
using MVC.ViewModels;
using System;
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

        [Authorize]
        public async Task<ActionResult> Index()
        {
            var employeeBusinessLayer = new EmployeeBusinessLayer();
            var employeeListViewModel = new EmployeeListViewModel();
            var employees = await employeeBusinessLayer.GetEmployeesAsync();
            var empViewModels = new List<EmployeeViewModel>();

            employeeListViewModel.UserName = User.Identity.Name;

            employees.ForEach(employee =>
            {
                var empViewModel = new EmployeeViewModel
                {
                    EmployeeName = employee.FirstName + " " + employee.LastName,
                    Salary = employee.Salary.ToString("C")
                };
                empViewModel.SalaryColor = employee.Salary > 15000 ? "yellow" : "green";
                empViewModels.Add(empViewModel);
            });
            employeeListViewModel.Employees = empViewModels;

            employeeListViewModel.FooterData = new FooterViewModel();
            employeeListViewModel.FooterData.CompanyName = "Assist";
            employeeListViewModel.FooterData.Year = DateTime.Now.Year.ToString();

            return View("Index", employeeListViewModel);
        }

        [AdminFilter]
        public ActionResult AddNew()
        {
            return View("CreateEmployee", new CreateEmployeeViewModel());
        }

        [AdminFilter]
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

        [ChildActionOnly]
        public ActionResult GetAddNewLink()
        {
            if(Convert.ToBoolean(Session["IsAdmin"]))
            {
                return PartialView("AddNewLink");
            }
            else
            {
                return new EmptyResult();
            }
        }
    }
}
