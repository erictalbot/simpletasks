using UnityConfiguration;

namespace $rootnamespace$.App_Start
{
    public class UnitySetup_DefaultConvention : UnityRegistry
    {
        /// <summary>
        /// Register based on convention for Unity
        /// </summary>
        public UnitySetup_DefaultConvention()
        {
            // Scan one or several assemblies and auto register types based on a 
            // convention. You can also include or exclude certain types and/or 
            // namespaces using a filter.
            Scan(scan =>
            {
                scan.AssemblyContaining<UnitySetup_DefaultConvention>();
                scan.With<NamingConvention>();
            });

        }
    }
}