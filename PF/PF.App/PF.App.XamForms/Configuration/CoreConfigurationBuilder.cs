using DryIoc;
using Splat;
using Splat.DryIoc;
using System;
using System.Collections.Generic;
using System.Text;
using PF.App.Contracts;

namespace PF.App.Core.Configuration
{
    
    public class CoreConfigurationBuilder
    {
        public static void Build(Container container, Action<Container> containerSetup)
        {
            containerSetup(container);
            SetupSplat(container);
        }

        private static void SetupSplat(Container container)
        {
            Locator.SetLocator(new DryIocDependencyResolver(container));
            Locator.CurrentMutable.InitializeSplat();
        }
    }
}
