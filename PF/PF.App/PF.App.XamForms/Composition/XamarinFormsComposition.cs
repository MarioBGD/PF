using DryIoc;
using PF.App.Contracts.Navigation;
using PF.App.Core.Composition;
using PF.App.XamForms.Navigation;
using PF.App.Contracts;
using PF.App.Contracts.Components;
using PF.App.Contracts.Storage;
using PF.App.Core.ViewModels;
using PF.App.Core.ViewModels.Startup;
using PF.App.XamForms.Components;
using PF.App.XamForms.Storage;
using PF.App.XamForms.Views;
using PF.App.XamForms.Views.Startup;

namespace PF.App.XamForms.Composition
{
    public static class XamarinFormsComposition
    {
        public static void Configure(IRegistrator registrator)
        {
            CoreComposition.Configure(registrator);

            registrator.Register<IFormsNavigationProvider, FormsNavigationProvider>(Reuse.Singleton);
            registrator.Register<INavigationService, NavigationService>(Reuse.Singleton);
            registrator.Register<ISecureStorageService, SecureStorageService>();
            registrator.Register<IUiThreadInvoker, UiThreadInvoker>();
            registrator.Register<IDialogsManager, DialogsManagerComponent>();
            registrator.Register<ICacheStorage, CacheStorage>();
            
            //Pages + VM
            registrator.RegisterPage<LoginView, LoginViewModel>();
            registrator.RegisterPage<MainPage, MainViewModel>();
            registrator.RegisterPage<GroupPage, GroupViewModel>();
            //registrator.RegisterPage<FriendsView, FriendsViewModel>();
            //registrator.RegisterPage<GroupView, GroupViewModel>();
            
            //VM
            registrator.Register<AppBarViewModel, AppBarViewModel>();
            registrator.Register<FriendsViewModel, FriendsViewModel>();
            registrator.Register<GroupsListViewModel, GroupsListViewModel>();
            registrator.Register<ExpensesViewModel, ExpensesViewModel>();
            registrator.Register<MembersViewModel, MembersViewModel>();
        }
    }
}