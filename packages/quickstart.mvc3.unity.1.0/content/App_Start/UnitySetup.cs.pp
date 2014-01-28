//Calls UnitySetup's Start method on application startup
[assembly: WebActivator.PreApplicationStartMethod(typeof($rootnamespace$.App_Start.UnitySetup), "Start")]

//Registers Module that properly disposes the child UnityContainer after each request
//http://unitymvc3.codeplex.com/
//http://devtrends.co.uk/blog/introducing-the-unity.mvc3-nuget-package-to-reconcile-mvc3-unity-and-idisposable
[assembly: WebActivator.PreApplicationStartMethod(typeof(Unity.Mvc3.PreApplicationStartCode), "PreStart")] 


namespace $rootnamespace$.App_Start
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Microsoft.Practices.Unity;
    using Unity.Mvc3;
    using UnityConfiguration;

    /// <summary>
    /// UnitySetup
    /// </summary>
    public class UnitySetup
    {
        /// <summary>
        /// Runs on application startup and creates Unity Container
        /// </summary>
        public static void Start()
        {
            var mainContainer = new UnityContainer();

            //Search for UnityRegistry classes and add them to the register process
            //For extensibility using other NuGet packages
            mainContainer.Configure(x =>
            {
                //Search for all types that inherit from UnityRegistry
                x.AddRegistry<UnitySetup_UnityRegistryScanner>();

            });

            //Register Controllers
            mainContainer.RegisterControllers();

            //Set up filters to work with dependency injection
            SetupFilterAttributes(mainContainer);

            //Set MVC 3 DependencyResolver
            DependencyResolver.SetResolver(new UnityDependencyResolver(mainContainer));

        }

        /// <summary>
        /// Set up filters to work with dependency injection
        /// </summary>
        /// <param name="httpContainer"></param>
        private static void SetupFilterAttributes(IUnityContainer httpContainer)
        {
            var oldProvider = FilterProviders.Providers.Single(f => f is FilterAttributeFilterProvider);
            FilterProviders.Providers.Remove(oldProvider);
            var provider = new UnityFilterAttributeFilterProvider(httpContainer);
            FilterProviders.Providers.Add(provider);
            //Alternative:
            //container.RegisterInstance<IFilterProvider>(provider);
        }
    }

    /// <summary>
    /// Unity FilterAttribute provider. 
    /// For DependencyInjection support in MVC Filters
    /// </summary>
    public class UnityFilterAttributeFilterProvider : FilterAttributeFilterProvider
    {
        private IUnityContainer _container;
        public UnityFilterAttributeFilterProvider(IUnityContainer container)
        {
            _container = container;
        }

        protected override IEnumerable<FilterAttribute> GetControllerAttributes(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
        {
            var attributes = base.GetControllerAttributes(controllerContext, actionDescriptor);

            foreach (var attribute in attributes)
            {
                _container.BuildUp(attribute.GetType(), attribute);
            }
            return attributes;
        }

        protected override IEnumerable<FilterAttribute> GetActionAttributes(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
        {
            var attributes = base.GetActionAttributes(controllerContext, actionDescriptor);

            foreach (var attribute in attributes)
            {
                _container.BuildUp(attribute.GetType(), attribute);
            }
            return attributes;
        }
    }
}