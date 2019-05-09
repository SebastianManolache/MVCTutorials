using MVC.Models;
using System.Web.Mvc;

namespace MVC.Controllers
{

    public class TestController : Controller
    {

        public Customer GetCustomer()
        {
            var customer = new Customer
            {
                Address = "Address1",
                CustomerName = "Customer 1"
            };
            return customer;
        }
        public ActionResult GetView()
        {
            var employee = new Employee
            {
                FirstName = "Sukesh",
                LastName = "Maria",
                Salary = 20000
            };
            return View("MyView",employee);
        }
    }
}
