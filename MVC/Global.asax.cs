﻿using System.Web;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using BusinessLayer;
using Castle.Windsor;
using MVC.Windsor;

namespace MVC
{
    public class MvcApplication : HttpApplication
    {
        private static WindsorContainer container;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            // MappingsConfig.RegisterMappings();

            BundleConfig.RegisterBundles(BundleTable.Bundles);
            BusinessSettings.SetBusiness();
            container = new WindsorContainer();
            container.Install(new ControllerInstaller(), new ServiceInstaller());
            ControllerBuilder.Current.SetControllerFactory(new WindsorControllerFactory(container));
            GlobalConfiguration.Configuration.Services.Replace(typeof(IHttpControllerActivator), new WindsorControllerActivator(container));

        }
    }
}
