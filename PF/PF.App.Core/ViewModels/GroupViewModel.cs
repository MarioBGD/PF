using System.Threading.Tasks;
using System.Windows.Input;
using PF.App.Contracts.Navigation;
using PF.App.Core.LibUI;

namespace PF.App.Core.ViewModels
{
    public class GroupViewModel : BaseViewModel
    {
        public GroupViewModel(INavigationService navigationService)
        {
            BackCommand = new SimpleCommand(async () => await navigationService.NavigateBackToAsync<MainViewModel>());
        }
        
        public ICommand BackCommand { get; }
    }
}