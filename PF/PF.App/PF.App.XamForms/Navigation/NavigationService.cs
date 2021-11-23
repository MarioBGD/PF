using PF.App.Contracts.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PF.App.Core.ViewModels.Startup;
using PF.App.XamForms.Views.Startup;

namespace PF.App.XamForms.Navigation
{
    public class NavigationService : INavigationService
    {
        private readonly IFormsNavigationProvider _formsNavigationProvider;
        
        public NavigationService(IFormsNavigationProvider formsNavigationProvider)
        {
            _formsNavigationProvider = formsNavigationProvider;
        }

        public Task NavigateNextToAsync<TViewModel>() => _formsNavigationProvider.Navigation.PushAsync(RegistratorViewExtension.GetPage<TViewModel>());
        
        public Task NavigateBackToAsync<TViewModel>() => throw new NotImplementedException();

        public Task RemoveAllPagesAsync() => Task.CompletedTask;
    }
}
