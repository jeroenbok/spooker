using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using Spooker.Web.Domain;
using Spooker.Web.Wiring;
using log4net;

namespace Spooker.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : HttpApplication
    {
        private readonly Lazy<IContainer> _container = new Lazy<IContainer>(() => new SpookerWiring().Create());

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteCollection routeCollection = RouteTable.Routes;
            routeCollection.MapRoute("Index", "", new {controller = "Register", action = "Index"});

            RouteConfig.RegisterRoutes(routeCollection);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            DependencyResolver.SetResolver(new AutofacDependencyResolver(_container.Value));
        }

        public override void Dispose()
        {
            _container.Value.Dispose();
            LogManager.GetLogger(typeof(MvcApplication)).Info("Disposed");
            LogManager.ShutdownRepository();
        }
    }
}