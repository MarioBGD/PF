using PF.App.Contracts.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PF.App.XamForms.Navigation
{
    public class NavigationService : INavigationService
    {
        public Task NavigateBackToAsync(NavigationState state)
        {
            throw new NotImplementedException();
        }

        public Task NavigateNextToAsync(NavigationState state)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAllPagesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
