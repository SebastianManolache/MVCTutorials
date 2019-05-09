using System.Collections.Generic;


namespace MVC.ViewModels
{

    public class EmployeeListViewModel
    {
       
        public string UserName { get; set; }
        public List<EmployeeViewModel> Employees{get;set;}  
    }
}
