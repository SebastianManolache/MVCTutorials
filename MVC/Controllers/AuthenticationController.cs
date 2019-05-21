using MVC.DataAccessLayer;
using BusinessEntities;
using System.Web.Mvc;
using System.Web.Security;

namespace MVC.Controllers
{
    public class AuthenticationController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        public ActionResult DoLogin(UserDetails user)
        {
            if (ModelState.IsValid)
            {
                var employeeBusinessLayer = new EmployeeBusinessLayer();
                UserStatus status = employeeBusinessLayer.GetUserValidity(user);
                bool IsAdmin = false;
                if (status == UserStatus.AuthenticatedAdmin)
                {
                    IsAdmin = true;
                }
                else if (status == UserStatus.AuthentucatedUser)
                {
                    IsAdmin = false;
                }
                else
                {
                    ModelState.AddModelError("CredentialErorr", "Invalid Username or Password");
                    return View("Login");
                }
                FormsAuthentication.SetAuthCookie(user.UserName, false);
                Session["IsAdmin"] = IsAdmin;
                return RedirectToAction("Index", "Employee");
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
