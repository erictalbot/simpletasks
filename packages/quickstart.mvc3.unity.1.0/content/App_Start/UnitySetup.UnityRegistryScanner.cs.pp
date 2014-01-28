using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UnityConfiguration;

namespace $rootnamespace$.App_Start
{
    public class UnitySetup_UnityRegistryScanner : UnityRegistry
    {
        public UnitySetup_UnityRegistryScanner()
        {
            Scan(scan =>
            {
                //Search for UnityRegistry classes in current assembly
                //For fast Unity configuration
                scan.AssemblyContaining<UnitySetup_DefaultConvention>();
                scan.ForRegistries();
            });
        }
    }
}