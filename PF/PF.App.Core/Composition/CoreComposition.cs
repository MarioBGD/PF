using DryIoc;
using PF.App.Contracts.Startup;
using PF.App.Core.Startup;
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
            registrator.Register<IHasher, SHA512>();
            
            //DAL
            registrator.Register<IAuthenticationService, AuthenticationService>();
            registrator.Register<IApiCallsService, ApiService>();
            registrator.Register<IDataGetterService, ApiDataGetterService>(); //TODO: global or only local
            registrator.Register<IDataManager, DataManager>();
            registrator.Register<ISessionManager, SessionManager>(Reuse.Singleton);
            registrator.Register<IUsersService, UsersService>();
            registrator.Register<IFriendsService, FriendsService>();
            registrator.Register<IGroupsService, GroupsService>();
            registrator.Register<IMembersService, MembersService>();
            registrator.Register<IExpensesServcie, ExpensesService>();
        }
    }
}
