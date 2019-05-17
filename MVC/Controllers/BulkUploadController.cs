using MVC.DataAccessLayer;
using MVC.Filters;
using MVC.Models;
using MVC.ViewModels;
using System.Collections.Generic;
using System.Web.Mvc;
using System;
using System.IO;

namespace MVC.Controllers
{
    public class BulkUploadController : Controller
    {
        [HeaderFooterFilter]
        [AdminFilter]
        public ActionResult Index()
        {
            return View(new FileUploadViewModel());
        }

        [AdminFilter]
        public ActionResult Upload(FileUploadViewModel model)
        {
            List<Employee> employees = GetEmployees(model);
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
