using MVC.ViewModels;
using System;
using System.Web;
using System.Web.Mvc;

namespace MVC.Filters
{
    public class HeaderFooterFilter : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            ViewResult view = filterContext.Result as ViewResult;
            if (view != null)
            {
                BaseViewModel baseViewModel = view.Model as BaseViewModel;
                if (baseViewModel != null)
                {
                    baseViewModel.FooterData = new FooterViewModel();
                    baseViewModel.FooterData.CompanyName = "ASSIST";
                    baseViewModel.FooterData.Year = DateTime.Now.Year.ToString();
                    baseViewModel.UserName = HttpContext.Current.User.Identity.Name;
                }
            }
        }
    }
}
