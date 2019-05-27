using BusinessEntities;
using BussinessLayer.Interfaces;
using MVC.Filters;
using MVC.ViewModels;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MVC.Areas.SPA.Controllers
{
    public class SpaBulkUploadController : AsyncController
    {
        private readonly IEmployeeBusinessLayer employeeBusinessLayer;

        public SpaBulkUploadController (IEmployeeBusinessLayer employeeBusinessLayer)
        {
            this.employeeBusinessLayer = employeeBusinessLayer;
        }

        [AdminFilter]
        public ActionResult Index()
        {
            return PartialView("Index");
        }

        [AdminFilter]
        public async Task<ActionResult> Upload(FileUploadViewModel model)
        {
            var thread1 = Thread.CurrentThread.ManagedThreadId;
            var employees = await Task.Factory.StartNew
                (() => GetEmployees(model));
            int thread2 = Thread.CurrentThread.ManagedThreadId;

            employeeBusinessLayer.UploadEmployees(employees);
            var viewModel = new EmployeeListViewModel();
            viewModel.Employees = new List<EmployeeViewModel>();

            employees.ForEach(employee =>
            {
                var employeeViewModel = new EmployeeViewModel();
                employeeViewModel.EmployeeName = employee.FirstName + " " + employee.LastName;
                employeeViewModel.Salary = employee.Salary.ToString("C");

                if (employee.Salary > 15000)
                {
                    employeeViewModel.SalaryColor = "yellow";
                }
                else
                {
                    employeeViewModel.SalaryColor = "green";
                }

                viewModel.Employees.Add(employeeViewModel);
            });

            return Json(viewModel);
        }

        private List<Employee> GetEmployees(FileUploadViewModel model)
        {
            var csvreader = new StreamReader(model.fileUpload.InputStream);
            var employees = new List<Employee>();
           
            csvreader.ReadLine();

            while (!csvreader.EndOfStream)
            {
                var line = csvreader.ReadLine();
                var values = line.Split(',');//Values are comma separated

                Employee e = new Employee
                {
                    FirstName = values[0],
                    LastName = values[1],
                    Salary = int.Parse(values[2])
                };
                employees.Add(e);
            }
            return employees;
        }
    }
}
