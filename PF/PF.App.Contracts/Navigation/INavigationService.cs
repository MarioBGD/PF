using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PF.App.Contracts.Navigation
{
    public interface INavigationService
    {
        Task NavigateNextToAsync<TViewModel>();
        Task NavigateBackToAsync<TViewModel>();
        Task RemoveAllPagesAsync();
    }
}
