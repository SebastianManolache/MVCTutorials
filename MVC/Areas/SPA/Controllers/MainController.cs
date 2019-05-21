using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC.ViewModels.SPA;
using OldViewModel = MVC.ViewModels;

namespace MVC.Areas.SPA.Controllers
{
    public class MainController : Controller
    {
        public ActionResult Index()
        {
            var view = new MainViewModel();
            view.UserName = User.Identity.Name;
            view.FooterData = new OldViewModel.FooterViewModel();
            view.FooterData.CompanyName = "Internship ASSIST";
            view.FooterData.Year = DateTime.Now.Year.ToString();
            return View("Index", view);
        }
    }
}
