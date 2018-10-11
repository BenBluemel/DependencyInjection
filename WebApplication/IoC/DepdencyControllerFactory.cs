using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebApplication.IoC
{
    public class IoCControllerFactory : DefaultControllerFactory
    {
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return DependencyInjector.Instance.Container.Resolve(controllerType) as Controller;
        }

        public override void ReleaseController(IController controller)
        {
            if (controller is IDisposable dispose)
            {
                dispose.Dispose();
            }
        }
    }
}