using System;
using PF.App.Contracts.Navigation;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PF.App.XamForms.Navigation
{
    public class NavigationService : INavigationService
    {
        private readonly IFormsNavigationProvider _formsNavigationProvider;
        private Page CurrentPage;
        
        public NavigationService(IFormsNavigationProvider formsNavigationProvider)
        {
            _formsNavigationProvider = formsNavigationProvider;
        }

        public Task NavigateNextToAsync<TViewModel>() =>
            Device.InvokeOnMainThreadAsync(() =>
            {
                var page = RegistratorViewExtension.GetPage<TViewModel>();
                CurrentPage = page;
                return _formsNavigationProvider.Navigation.PushAsync(page);
            });
        

        public Task NavigateBackToAsync<TViewModel>() =>
            Device.InvokeOnMainThreadAsync(() =>
            {
                var page = RegistratorViewExtension.GetPage<TViewModel>();
                CurrentPage = page;
                return _formsNavigationProvider.Navigation.PushAsync(page);
            });


        public Task RemoveAllPagesAsync()
        {
            return Task.CompletedTask;
            // foreach (var page in _formsNavigationProvider.Navigation.NavigationStack)
            // {
            //     if (CurrentPage != page)
            //     {
            //         _formsNavigationProvider.Navigation.RemovePage(page);
            //     }
            // }
        }
    }
}
