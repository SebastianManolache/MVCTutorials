using MVC.DataAccessLayer.Managers;
using MVC.Filters;
using BusinessEntities;
using MVC.ViewModels;
using System.Collections.Generic;
using System.Web.Mvc;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace MVC.Controllers
{
    public class BulkUploadController : AsyncController
    {
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
            var employeeBusinerLayer = new EmployeeBusinessLayer();
            employeeBusinerLayer.UploadEmployees(employees);

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
