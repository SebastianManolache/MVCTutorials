﻿using System.Collections.Generic;


namespace MVC.ViewModels
{

    public class EmployeeListViewModel
    {
        public List<EmployeeViewModel> Employees { get; set; }
        public FooterViewModel FooterData { get; set; }
        public string UserName { get; set; }
    }
}
