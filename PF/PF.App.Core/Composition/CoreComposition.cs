using DryIoc;
using PF.App.Contracts.Startup;
using PF.App.Core.Startup;
using System;
using System.Collections.Generic;
using System.Text;

namespace PF.App.Core.Composition
{
    public class CoreComposition
    {
        public static void Configure(IRegistrator registrator)
        {
            registrator.Register<IStartupCoordinator, StartupCoordinator>();
        }
    }
}
