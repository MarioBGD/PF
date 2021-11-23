using DryIoc;
using PF.App.Contracts.Startup;
using PF.App.Core.Startup;
using System;
using System.Collections.Generic;
using System.Text;
using PF.App.Core.DAL;
using PF.App.Core.DAL.Api;
using PF.App.Core.DAL.Contracts;
using PF.Common.Crypto;

namespace PF.App.Core.Composition
{
    public class CoreComposition
    {
        public static void Configure(IRegistrator registrator)
        {
            registrator.Register<IStartupCoordinator, StartupCoordinator>();
            registrator.Register<IAuthenticationService, AuthenticationService>();
            registrator.Register<IHasher, SHA512>();
            registrator.Register<IApiCallsService, ApiService>();
            registrator.Register<ISessionManager, SessionManager>(Reuse.Singleton);
        }
    }
}
