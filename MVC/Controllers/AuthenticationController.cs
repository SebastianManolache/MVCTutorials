using MVC.DataAccessLayer;
using MVC.Models;
using System.Web.Mvc;
using System.Web.Security;

namespace MVC.Controllers
{
    public class AuthenticationController : Controller
    {
        // GET: Authentication
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult DoLogin(UserDetails user)
        {
            if (ModelState.IsValid)
            {
                var employeeBusinessLayer = new EmployeeBusinessLayer();
                if (employeeBusinessLayer.IsValidUser(user))
                {
                    FormsAuthentication.SetAuthCookie(user.UserName, false);
                    return RedirectToAction("Index", "Employee");
                }
                else
                {
                    ModelState.AddModelError("CredentialError", "Invalid Username or Password");
                    return View("Login");
                }
            }
            else
            {
                return View("Login");
            }
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}