using MVC.Logger;
using System.Web.Mvc;

namespace MVC.Filters
{
    public class EmployeeExceptionFilter: HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            var logger = new FileLogger();
            logger.LogException(filterContext.Exception);
            base.OnException(filterContext);
        }
    }
}
