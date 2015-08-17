using DD4T.ContentModel.Contracts.Caching;
using DD4T.ContentModel.Contracts.Configuration;
using DD4T.ContentModel.Contracts.Logging;
using DD4T.ContentModel.Contracts.Providers;
using DD4T.ContentModel.Contracts.Resolvers;
using DD4T.ContentModel.Factories;
using DD4T.Factories;
using DD4T.Utils;
using DD4T.Utils.Caching;
using DD4T.Utils.Logging;
using DD4T.Utils.Resolver;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DD4T.DI.Unity
{
    public  static class Bootstrap
    {
        public static void UserDD4T(this IUnityContainer container)
        {
            //not all dll's are loaded in the app domain. we will load the assembly in the appdomain to be able map the mapping
            var location = string.Format(@"{0}\bin\", AppDomain.CurrentDomain.BaseDirectory);
            var file = Directory.GetFiles(location, "DD4T.Providers.*").FirstOrDefault();
            var load = Assembly.LoadFile(file);

            var provider = AppDomain.CurrentDomain.GetAssemblies().Where(ass => ass.FullName.StartsWith("DD4T.Providers")).FirstOrDefault();
            var providerTypes = provider.GetTypes();
            var pageProvider = providerTypes.Where(a => typeof(IPageProvider).IsAssignableFrom(a)).FirstOrDefault();
            var cpProvider = providerTypes.Where(a => typeof(IComponentPresentationProvider).IsAssignableFrom(a)).FirstOrDefault();
            var linkProvider = providerTypes.Where(a => typeof(ILinkProvider).IsAssignableFrom(a)).FirstOrDefault();
            var binaryProvider = providerTypes.Where(a => typeof(IBinaryProvider).IsAssignableFrom(a)).FirstOrDefault();
            var componentProvider = providerTypes.Where(a => typeof(IComponentProvider).IsAssignableFrom(a)).FirstOrDefault();
            var commonServices = providerTypes.Where(a => typeof(IProvidersCommonServices).IsAssignableFrom(a)).FirstOrDefault();

            
            if(!container.IsRegistered<IDD4TConfiguration>())
                container.RegisterType<IDD4TConfiguration, DD4TConfiguration>(new ContainerControlledLifetimeManager());
            if(!container.IsRegistered<IPublicationResolver>())
                container.RegisterType<IPublicationResolver, DefaultPublicationResolver>(new ContainerControlledLifetimeManager());
            if(!container.IsRegistered<ILogger>())
                container.RegisterType<ILogger, DefaultLogger>(new ContainerControlledLifetimeManager());

            if(!container.IsRegistered<ICacheAgent>())
                container.RegisterType<ICacheAgent, DefaultCacheAgent>();


            //providers
            if (binaryProvider != null && !container.IsRegistered<IBinaryProvider>())
                container.RegisterType(typeof(IBinaryProvider), binaryProvider);

            if (componentProvider != null && !container.IsRegistered<IComponentProvider>())
                container.RegisterType(typeof(IComponentProvider), componentProvider);

            if (pageProvider != null && !container.IsRegistered<IPageProvider>())
                container.RegisterType(typeof(IPageProvider), pageProvider);

            if (cpProvider != null && !container.IsRegistered<IComponentPresentationProvider>())
                container.RegisterType(typeof(IComponentPresentationProvider), cpProvider);

            if (linkProvider != null && !container.IsRegistered<ILinkProvider>())
                container.RegisterType(typeof(ILinkProvider), linkProvider);

            if (commonServices != null && !container.IsRegistered<IProvidersCommonServices>())
                container.RegisterType(typeof(IProvidersCommonServices), commonServices);



            //factories
            if (!container.IsRegistered<IPageFactory>())
                container.RegisterType<IPageFactory, PageFactory>();

            if (!container.IsRegistered<IComponentPresentationFactory>())
                container.RegisterType<IComponentPresentationFactory, ComponentPresentationFactory>();

            if (!container.IsRegistered<ILinkFactory>())
                container.RegisterType<ILinkFactory, LinkFactory>();

            if (!container.IsRegistered<IBinaryFactory>())
                container.RegisterType<IBinaryFactory, BinaryFactory>();

            if (!container.IsRegistered<IComponentFactory>())
                container.RegisterType<IComponentFactory, ComponentFactory>();

            if (!container.IsRegistered<IFactoryCommonServices>())
                container.RegisterType<IFactoryCommonServices, FactoryCommonServices>();



        }
    }
}
