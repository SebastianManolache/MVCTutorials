using MVC.Models;
using MVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace MVC.ViewModels
{
       
    public class EmployeeListViewModel
    {
       
        public string UserName { get; set; }
        public List<EmployeeViewModel> Employees{get;set;}  
    }
}