using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Gameo.DataAccess;
using Gameo.DataAccess.Core;
using Gameo.Domain;
using Gameo.Web.Areas.Admin.Models;
using Gameo.Web.Models;
using Ninject;

namespace Gameo.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Game", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
            MongoDbMapping.Initialize();
            ModelBinders.Binders.Add(typeof(CustomUserIdentity), new CustomUserIdentityModelBinder());
            var userRepository = DependencyResolver.Current.GetService<IUserRepository>();
            AddAdminUserIfNoUserFound(userRepository);
        }

        private static void AddAdminUserIfNoUserFound(IUserRepository userRepository)
        {
            if (!userRepository.HasAdminUser)
            {
                userRepository.Add(new User { Name = "spiel", Password = "gamer", IsAdmin = true });
            }
        }
    }
}