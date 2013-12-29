using System;
using System.Web.Routing;
using System.Web.Mvc;
using MvcBlog.WebUI.Mappers;
using Ninject;
using MvcBlog.Domain;
using MvcBlog.WebUI.Abstract;
using MvcBlog.WebUI.Concrete;

namespace MvcBlog.WebUI.Infrastructure
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private readonly IKernel _ninjectKernel;

        // constructor
        public NinjectControllerFactory()
        {
            _ninjectKernel = new StandardKernel();
            AddBindings();
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return controllerType == null ? null : (IController)_ninjectKernel.Get(controllerType);
        }
        
        private void AddBindings()
        {
            _ninjectKernel.Bind<IRepository>().To<SqlRepository>();
            _ninjectKernel.Bind<IConfigService>().To<ConfigService>().InSingletonScope();
            _ninjectKernel.Bind<IMapper>().To<CommonMapper>().InSingletonScope();
        }
    }
}