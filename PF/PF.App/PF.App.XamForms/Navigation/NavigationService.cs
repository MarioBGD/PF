using PF.App.Contracts.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PF.App.Core.ViewModels.Startup;
using PF.App.XamForms.Views.Startup;
using Splat;
using Xamarin.Forms;

namespace PF.App.XamForms.Navigation
{
    public class NavigationService : INavigationService
    {
        private readonly IFormsNavigationProvider _formsNavigationProvider;
        
        public NavigationService(IFormsNavigationProvider formsNavigationProvider)
        {
            _formsNavigationProvider = formsNavigationProvider;
        }

        public async Task NavigateNextToAsync<TViewModel>()
        {
            var page = RegistratorViewExtension.GetPage<TViewModel>();
            await Device.InvokeOnMainThreadAsync(async () => await _formsNavigationProvider.Navigation.PushAsync(page));
        }

        public Task NavigateBackToAsync<TViewModel>() => throw new NotImplementedException();

        public Task RemoveAllPagesAsync() => Task.CompletedTask;
    }
}
