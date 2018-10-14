using DependencyInjection;
using System.Web.Mvc;
using WebApplication.Controllers;
using WebApplication.IoC;
using WebApplication.Managers;
using WebApplication.Repositories;

namespace WebApplication
{
    public class IoCRegistration
    {
        public static void RegisterIoC()
        {
            DependencyInjector.Instance.Container.Register<IUserRepository, UserRepository>(LifecycleType.Singleton);
            DependencyInjector.Instance.Container.Register<IUserManager, UserManager>();
            DependencyInjector.Instance.Container.Register<HomeController, HomeController>();
            ControllerBuilder.Current.SetControllerFactory(typeof(IoCControllerFactory));
        }
    }
}