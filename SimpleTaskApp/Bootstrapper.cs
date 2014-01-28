using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc3;
using SimpleTaskData;
using SimpleTaskData.Repositories;
using System.Web.Http;

namespace SimpleTaskApp
{
    public static class Bootstrapper
    {
        public static void Initialise()
        {
            var container = BuildUnityContainer();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));

            // register dependency resolver for WebAPI RC
            GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);
            // if you still use the beta version - change above line to:
            //GlobalConfiguration.Configuration.ServiceResolver.SetResolver(new Unity.WebApi.UnityDependencyResolver(container));
        }

        private static IUnityContainer BuildUnityContainer()
        {
            IUnityContainer myContainer = new UnityContainer();

            RepositoryFactories repositoryFactory = new RepositoryFactories();          // This is the backend where our repositories are stored.

            myContainer.RegisterInstance<RepositoryFactories>(repositoryFactory);       // Will provide a singleton for RepositoryFactory
            myContainer.Resolve<RepositoryFactories>();

            myContainer.RegisterType<IRepositoryProvider,RepositoryProvider>();         // This is what provide repositories to Unit Of Work

            myContainer.RegisterType<ISimpleTaskUow, SimpleTaskUow>();

            return myContainer;
        }
    }
}