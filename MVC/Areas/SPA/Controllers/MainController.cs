using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MVC.DataAccessLayer;
using MVC.Filters;
using BusinessEntities;
//using MVC.ViewModels;
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

        [ChildActionOnly]
        public ActionResult EmployeeList()
        {
            EmployeeListViewModel employeeListViewModel = new EmployeeListViewModel();
            EmployeeBusinessLayer empBal = new EmployeeBusinessLayer();
            var employees = empBal.GetEmployeesAsync().Result;

            var empViewModels = new List<EmployeeViewModel>();

            foreach (Employee emp in employees)
            {
                EmployeeViewModel empViewModel = new EmployeeViewModel();
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
            return PartialView("EmployeeList", employeeListViewModel);
        }


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
    }
}
