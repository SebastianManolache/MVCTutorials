using MVC.DataAccessLayer;
using MVC.Filters;
using MVC.Models;
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
        [HeaderFooterFilter]
        [AdminFilter]
        public ActionResult Index()
        {
            return View(new FileUploadViewModel());
        }

        [AdminFilter]
        [HandleError]
        public async Task<ActionResult> Upload(FileUploadViewModel model)
        {
            int thread1 = Thread.CurrentThread.ManagedThreadId;
            List<Employee> employees = await Task.Factory.StartNew<List<Employee>>
                (() => GetEmployees(model));
            int thread2 = Thread.CurrentThread.ManagedThreadId;
            var employeeBusinerLayer = new EmployeeBusinessLayer();
            employeeBusinerLayer.UploadEmployees(employees);
            return RedirectToAction("Index", "Employee");
        }
        private List<Employee> GetEmployees(FileUploadViewModel model)
        {
           var employees = new List<Employee>();
            var csvreader = new StreamReader(model.fileUpload.InputStream);
            csvreader.ReadLine(); // Assuming first line is header
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
