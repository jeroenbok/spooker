using System.Reflection;
using System.Web.Mvc;
using Autofac;
using Spooker.Web.Infrastructure.Cookies;
using log4net;

namespace Spooker.Web.Wiring
{
    public class SpookerWiring
    {
        private readonly ILog _log = LogManager.GetLogger(typeof(SpookerWiring));

        public IContainer Create()
        {
            _log.Info("Hi there");

            var configuration = new ContainerBuilder();
            configuration.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).Where(t => t.BaseType == typeof(Controller)).AsSelf();
            configuration.RegisterType<AppCookies>().AsImplementedInterfaces();
            configuration.RegisterType<CookieContainer>().AsImplementedInterfaces();
            return configuration.Build();
        }
    }
}