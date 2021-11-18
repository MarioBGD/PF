﻿using PF.App.Contracts.Navigation;
using PF.App.Contracts.Startup;
using System;
using System.Collections.Generic;
using System.Text;

namespace PF.App.Core.Startup
{
    public class StartupCoordinator : IStartupCoordinator
    {
        private INavigationService _navigationService;

        public StartupCoordinator(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }


        public void Start()
        {
            _navigationService.NavigateNextToAsync(NavigationState.Authentication);
        }
    }
}
