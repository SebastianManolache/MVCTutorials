using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MVC.DataAccessLayer;
using MVC.Filters;
using BusinessEntities;
using MVC.ViewModels.SPA;
using OldViewModel = MVC.ViewModels;

namespace MVC.Areas.SPA.Controllers
{
    public class MainController : Controller
    {
        public ActionResult Index()
        {
            var view = new MainViewModel();
            view.UserName = User.Identity.Name;
            view.FooterData = new OldViewModel.FooterViewModel();
            view.FooterData.CompanyName = "Internship ASSIST";
            view.FooterData.Year = DateTime.Now.Year.ToString();
            return View("Index", view);
        }
        //[Authorize]
        ///[HeaderFooterFilter]
        /*
        public async Task<ActionResult> EmployeeList()
        {
            var employeeBusinessLayer = new EmployeeBusinessLayer();
            var employeeListViewModel = new EmployeeListViewModel();
            var employees = await employeeBusinessLayer.GetEmployeesAsync();

            var empViewModels =  new List<EmployeeViewModel>();

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

            return View("EmployeeList", employeeListViewModel);
                       
        }
        */
        [Authorize]
        [HeaderFooterFilter]
        public ActionResult EmployeeList()
        {
            var employeeListViewModel = new EmployeeListViewModel();
            try
            {
                var empBal = new EmployeeBusinessLayer();
                var employees = empBal.GetEmployees();
                //var employees = new List<Employee>();

                var empViewModels = new List<EmployeeViewModel>();

                foreach (Employee emp in employees)
                {
                    var empViewModel = new EmployeeViewModel
                    {
                        EmployeeName = emp.FirstName + " " + emp.LastName,
                        Salary = emp.Salary.ToString("C")
                    };
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

            }
            catch (Exception e)
            {

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
        public ActionResult SaveEmployee(Employee employee)
        {
            var employeeBusinessLayer = new EmployeeBusinessLayer();
            employeeBusinessLayer.SaveEmployeeAsync(employee);

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
            return Json(empViewModel);
        }
    }
}

