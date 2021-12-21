using PF.App.Contracts.Navigation;
using PF.App.Contracts.Startup;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PF.App.Contracts.Storage;
using PF.App.Core.DAL.Contracts;
using PF.App.Core.ViewModels;
using PF.App.Core.ViewModels.Startup;

namespace PF.App.Core.Startup
{
    public class StartupCoordinator : IStartupCoordinator
    {
        private readonly INavigationService _navigationService;
        private readonly ISecureStorageService _secureStorageService;
        private readonly IAuthenticationService _authenticationService;

        public StartupCoordinator(INavigationService navigationService, ISecureStorageService secureStorageService, IAuthenticationService authenticationService)
        {
            _navigationService = navigationService;
            _secureStorageService = secureStorageService;
            _authenticationService = authenticationService;
        }


        public void Start()
        {
            Task.Run(async () =>
            {
                var username = await _secureStorageService.ReadStringAsync("usn", null);
                var hashedPassword = await _secureStorageService.ReadStringAsync("psw", null);

                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(hashedPassword))
                {
                    await _navigationService.NavigateNextToAsync<LoginViewModel>();
                }
                else
                {
                    await _authenticationService.LoginAsync(username, hashedPassword);
                    await _navigationService.NavigateNextToAsync<MainViewModel>();
                }

            });
        }
    }
}
