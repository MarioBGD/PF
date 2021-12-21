using DryIoc;
using System;
using System.Collections.Generic;
using System.Text;
using PF.App.Contracts;
using Splat;
using Splat.DryIoc;

namespace PF.App.Core.Configuration
{
    
    public class CoreConfigurationBuilder
    {
        public static void Build(Container container, Action<Container> containerSetup)
        {
            containerSetup(container);
            SetupSplat(container);
            SetupLogger();
        }

        private static void SetupSplat(Container container)
        {
            Locator.SetLocator(new DryIocDependencyResolver(container));
            Locator.CurrentMutable.InitializeSplat();
        }

        private static void SetupLogger()
        {

        }
    }
}
