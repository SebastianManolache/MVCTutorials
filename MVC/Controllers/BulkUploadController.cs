using BusinessEntities;
using BussinessLayer.Interfaces;
using MVC.Filters;
using MVC.ViewModels;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MVC.Controllers
{
    public class BulkUploadController : AsyncController
    {
        private readonly IEmployeeBusinessLayer employeeBusinessLayer;

        public BulkUploadController (IEmployeeBusinessLayer employeeBusinessLayer)
        {
            this.employeeBusinessLayer = employeeBusinessLayer;
        }

        [AdminFilter]
        [HeaderFooterFilter]
        public ActionResult Index()
        {
            return View(new FileUploadViewModel());
        }

        [AdminFilter]
        [HandleError]
        public async Task<ActionResult> Upload(FileUploadViewModel model)
        {
            var thread1 = Thread.CurrentThread.ManagedThreadId;
            var employees = await Task.Factory.StartNew
                (() => GetEmployees(model));
            var thread2 = Thread.CurrentThread.ManagedThreadId;

            employeeBusinessLayer.UploadEmployees(employees);

            return RedirectToAction("Index", "Employee");
        }
        private List<Employee> GetEmployees(FileUploadViewModel model)
        {
            var csvreader = new StreamReader(model.fileUpload.InputStream);
            var employees = new List<Employee>();
           
            csvreader.ReadLine(); 
            while (!csvreader.EndOfStream)
            {
                var line = csvreader.ReadLine();
                var values = line.Split(',');
                var e = new Employee
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
