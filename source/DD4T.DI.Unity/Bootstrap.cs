using DD4T.ContentModel.Contracts.Caching;
using DD4T.ContentModel.Contracts.Configuration;
using DD4T.ContentModel.Contracts.Logging;
using DD4T.ContentModel.Contracts.Providers;
using DD4T.ContentModel.Contracts.Resolvers;
using DD4T.ContentModel.Factories;
using DD4T.Core.Contracts.ViewModels;
using DD4T.DI.Unity.Exceptions;
using DD4T.Factories;
using DD4T.Utils;
using DD4T.Utils.Caching;
using DD4T.Utils.Logging;
using DD4T.Utils.Resolver;
using DD4T.ViewModels;
using DD4T.ViewModels.Reflection;
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
        public static void UseDD4T(this IUnityContainer container)
        {
            //not all dll's are loaded in the app domain. we will load the assembly in the appdomain to be able map the mapping
            var binDirectory = string.Format(@"{0}\bin\", AppDomain.CurrentDomain.BaseDirectory);
            if (!Directory.Exists(binDirectory))
                return;


            var file = Directory.GetFiles(binDirectory, "DD4T.Providers.*").FirstOrDefault();
            if (file == null)
                throw new ProviderNotFoundException();

            var load = Assembly.LoadFile(file);

            container.RegisterProviders();
            container.RegisterFactories();
            container.RegisterRestProvider();
            container.RegisterMvc();
            container.RegisterResolvers();
            container.RegisterViewModels();
         
  
                      
            if(!container.IsRegistered<IDD4TConfiguration>())
                container.RegisterType<IDD4TConfiguration, DD4TConfiguration>(new ContainerControlledLifetimeManager());
             
            if(!container.IsRegistered<ILogger>())
                container.RegisterType<ILogger, DefaultLogger>(new ContainerControlledLifetimeManager());

            if(!container.IsRegistered<ICacheAgent>())
                container.RegisterType<ICacheAgent, DefaultCacheAgent>();

            //caching JMS

            if (!container.IsRegistered<IMessageProvider>())
                container.RegisterType<IMessageProvider, JMSMessageProvider>(new ContainerControlledLifetimeManager());


        }
    }
}
