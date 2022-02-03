using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using PF.App.Contracts;
using PF.App.Contracts.Navigation;
using PF.App.Core.DAL.Contracts;
using PF.App.Core.DAL.Contracts.Models;

namespace PF.App.Core.ViewModels
{
    public class MembersViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly IUiThreadInvoker _uiThreadInvoker;
        private readonly IMembersService _membersService;
        private readonly CancellationToken _cancellationToken;

        public MembersViewModel(INavigationService navigationService, IUiThreadInvoker uiThreadInvoker, IMembersService membersService)
        {
            _navigationService = navigationService;
            _uiThreadInvoker = uiThreadInvoker;
            _membersService = membersService;
            _cancellationToken = new CancellationToken();

            Members = new ObservableCollection<PersonComponentViewModel>();
            
            _membersService.GetTest(OnMembersDataUpdate);
        }
        
        public ObservableCollection<PersonComponentViewModel> Members { get; }

        private Task OnMembersDataUpdate(IEnumerable<Member> members) => _uiThreadInvoker.InvokeOnUIThreadAsync(() =>
        {
            Members.Clear();

            foreach (var member in members)
            {
                Members.Add(
                    new PersonComponentViewModel(
                        member.Id,
                        OnMembershipTapped,
                        member.Balance));
            }
        });

        private async Task OnMembershipTapped(PersonComponentViewModel person)
        {
            
        }
    }
}