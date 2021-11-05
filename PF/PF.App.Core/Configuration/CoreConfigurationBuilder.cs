using DryIoc;
using System;
using System.Collections.Generic;
using System.Text;

namespace PF.App.Core.Configuration
{
    
    public class CoreConfigurationBuilder
    {
        public static void Build(Container container, Action<Container> containerSetup)
        {
            containerSetup(container);
            SetupSplat();
            SetupLogger();
        }

        private static void SetupSplat()
        {

        }

        private static void SetupLogger()
        {

        }
    }
}
