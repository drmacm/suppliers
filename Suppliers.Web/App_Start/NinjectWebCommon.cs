[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Suppliers.Web.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(Suppliers.Web.App_Start.NinjectWebCommon), "Stop")]

namespace Suppliers.Web.App_Start
{
    using System;
    using System.Web;
    using CompositionRoot;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using Ninject;
    using Ninject.Web.Common;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates a kernel that will manage all dependency injections in the system.
        /// <see cref="SuppliersBindings"/> contains mappings between IDal interfaces and EntityFramework implementations.
        /// Bindings are extracted to a separate project so that Suppliers.Web doesn't even know about Suppliers.DataAccess - there is no project reference.
        /// That way we ensure that no one will misuse the system and directly create things like SupplierDal or SuppliersContext etc.
        /// </summary>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel(new SuppliersBindings());
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }
    }
}
