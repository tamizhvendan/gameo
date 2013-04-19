using System.Linq;
using Gameo.DataAccess;
using Gameo.DataAccess.Core;
using Gameo.Domain;
using Gameo.Services;
using Gameo.Web.Models;

[assembly: WebActivator.PreApplicationStartMethod(typeof(Gameo.Web.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivator.ApplicationShutdownMethodAttribute(typeof(Gameo.Web.App_Start.NinjectWebCommon), "Stop")]

namespace Gameo.Web.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
            kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
            
            RegisterServices(kernel);
            return kernel;
        }

       

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IBranchRepository>().To<BranchRepository>();
            kernel.Bind<IGamingConsoleRepository>().To<GamingConsoleRepository>();
            kernel.Bind<IUserRepository>().To<UserRepository>();
            kernel.Bind<IGameRepository>().To<GameRepository>();
            kernel.Bind<IAuthenticationService>().To<AuthenticationService>();
            kernel.Bind<IGameService>().To<GameService>();
            kernel.Bind<IGamingTrend>().To<GamingTrend>();
            kernel.Bind<IDailySaleDetailsRepository>().To<DailySaleDetailsRepository>();
            kernel.Bind<IMembershipRepository>().To<MembershipRepository>();
            kernel.Bind<IMonthlyExpensesRepository>().To<MonthlyExpensesRepository>();
            kernel.Bind<ICollectionService>().To<CollectionService>();
            kernel.Bind<IRevenueService>().To<RevenueService>();
            kernel.Bind<ITrendChartEngine>().To<TrendChartEngine>();
        }
    }
}
