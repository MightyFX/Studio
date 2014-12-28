using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;

namespace MightyFX.Studio
{
#if FALSE
    /// <summary>
    /// Resolve dependencies for MVC / Web API using MEF.
    /// </summary>
    public class MefDependencyResolver : System.Web.Http.Dependencies.IDependencyResolver, System.Web.Mvc.IDependencyResolver
    {
        private readonly CompositionContainer _container;

        public MefDependencyResolver(CompositionContainer container)
        {
            _container = container;
        }

        public IDependencyScope BeginScope()
        {
            return this;
        }

        /// <summary>
        /// Called to request a service implementation.
        /// 
        /// Here we call upon MEF to instantiate implementations of dependencies.
        /// </summary>
        /// <param name="serviceType">Type of service requested.</param>
        /// <returns>Service implementation or null.</returns>
        public object GetService(Type serviceType)
        {
            if (serviceType == null)
                throw new ArgumentNullException("serviceType");

            var name = AttributedModelServices.GetContractName(serviceType);
            var export = _container.GetExportedValueOrDefault<object>(name);
            return export;
        }

        /// <summary>
        /// Called to request service implementations.
        /// 
        /// Here we call upon MEF to instantiate implementations of dependencies.
        /// </summary>
        /// <param name="serviceType">Type of service requested.</param>
        /// <returns>Service implementations.</returns>
        public IEnumerable<object> GetServices(Type serviceType)
        {
            if (serviceType == null)
                throw new ArgumentNullException("serviceType");

            var exports = _container.GetExportedValues<object>(AttributedModelServices.GetContractName(serviceType));
            return exports;
        }

        public void Dispose()
        {
        }
    }

    public static class MefConfig
    {
        public static void RegisterMef()
        {
            var asmCatalog = new AssemblyCatalog(Assembly.GetExecutingAssembly());
            var container = new CompositionContainer(asmCatalog);
            var resolver = new MefDependencyResolver(container);
            // Install MEF dependency resolver for MVC
            DependencyResolver.SetResolver(resolver);
            // Install MEF dependency resolver for Web API
            GlobalConfiguration.Configuration.DependencyResolver = resolver;
        }
    }
#endif

    public class Plugins
    {
        public static CompositionContainer Host
        {
            get;
            private set;
        }

        public static void Initialize(List<string> pluginFolders)
        {
            var catalog = new AggregateCatalog();

            catalog.Catalogs.Add(new DirectoryCatalog(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin")));

            foreach (var plugin in pluginFolders)
            {
                var directoryCatalog = new DirectoryCatalog(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Modules", plugin));
                catalog.Catalogs.Add(directoryCatalog);

            }
            Host = new CompositionContainer(catalog);
            Host.ComposeParts();
        }

        public static T GetInstance<T>(string contractName = null)
        {
            var type = default(T);
            if (Host == null)
            {
                return type;
            }

            if (!string.IsNullOrWhiteSpace(contractName))
            {
                type = Host.GetExportedValue<T>(contractName);
            }
            else
            {
                type = Host.GetExportedValue<T>();
            }

            return type;
        }
    }
}