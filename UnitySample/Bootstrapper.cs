using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc3;
using UnitySample.Service;
using System.Web.Http;

namespace UnitySample
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

        /*
         * J'ai trouvé un problème avec Ninject qui l'empêche de fonctionner avec ASP.NET MVC4 sur visual studio 2012. Je ne suis pas le seul à avoir ce problème c'est pourquoi je préfère Unity.
         * 
         * */

        private static IUnityContainer BuildUnityContainer()
        {
            // La méthode suivate retourne toujours le même objet.
            // http://unity.codeplex.com/discussions/401669

            IUnityContainer myContainer = new UnityContainer();
            myContainer.RegisterInstance<IMessageService>(MessageService.Instance);
            myContainer.Resolve<IMessageService>();
            return myContainer;

            // La méthode suivante retourne un nouvel object à chaque fois.
            // 
            //var container = new UnityContainer();
            // register all your components with the container here
            // it is NOT necessary to register your controllers
            //container.RegisterType<IMessageService, MessageService>();
            // e.g. container.RegisterType<ITestService, TestService>();            
            //return container;
        }
    }
}