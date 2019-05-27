using BusinessEntities;
using BussinessLayer.Interfaces;
using MVC.Filters;
using MVC.ViewModels.SPA;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Web.Mvc;
using OldViewModel = MVC.ViewModels;

namespace MVC.Areas.SPA.Controllers
{
    public class MainController : Controller
    {
        private readonly IEmployeeBusinessLayer employeeBusinessLayer;

        public MainController(IEmployeeBusinessLayer employeeBusinessLayer)
        {
            this.employeeBusinessLayer = employeeBusinessLayer;
        }

        public ActionResult Index()
        {
            var view = new MainViewModel
            {
                UserName = User.Identity.Name,
                FooterData = new OldViewModel.FooterViewModel()
            };
            view.FooterData.CompanyName = "Internship ASSIST";
            view.FooterData.Year = DateTime.Now.Year.ToString();

            return View("Index", view);
        }
        
        [Authorize]
        [HeaderFooterFilter]
        public ActionResult EmployeeList()
        {
            var employeeListViewModel = new EmployeeListViewModel();
            try
            {
                var employees = employeeBusinessLayer.GetEmployees();
                var empViewModels = new List<EmployeeViewModel>();

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
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }

            return PartialView("EmployeeList", employeeListViewModel);
        }

        [ChildActionOnly]
        public ActionResult GetAddNewLink()
        {
            if (Convert.ToBoolean(Session["IsAdmin"]))
            {
                return PartialView("AddNewLink");
            }
            else
            {
                return new EmptyResult();
            }
        }

        [AdminFilter]
        public ActionResult AddNew()
        {
            var viewModel = new CreateEmployeeViewModel();
            return PartialView("CreateEmployee", viewModel);
        }

        [AdminFilter]
        public async Task<ActionResult> SaveEmployeeAsync(Employee employee)
        {
            await employeeBusinessLayer.SaveEmployeeAsync(employee);

            var empViewModel = new EmployeeViewModel
            {
                EmployeeName = $"{employee.FirstName} {employee.LastName}",
                Salary = employee.Salary.ToString("C"),
                SalaryColor = employee.Salary > 15000 ? "yellow" : "green"
            };

            return Json(empViewModel);
        }
    }
}
