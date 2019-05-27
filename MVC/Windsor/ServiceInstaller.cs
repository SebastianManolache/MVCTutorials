using BussinessLayer.Interfaces;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using MVC.DataAccessLayer;
using MVC.DataAccessLayer.Managers;

namespace MVC.Windsor
{
    public class ServiceInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<SalesDbContext>().LifestyleTransient());
            container.Register(Component.For<IEmployeeBusinessLayer>().ImplementedBy<EmployeeBusinessLayer>().LifestyleTransient());
        }
    }
}
