using System.Collections.Generic;


namespace MVC.ViewModels
{

    public class EmployeeListViewModel:BaseViewModel
    {
        public List<EmployeeViewModel> Employees { get; set; }
    }
}
