using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using PF.App.Contracts;
using PF.App.Contracts.Navigation;
using PF.App.Core.DAL.Contracts;
using PF.App.Core.DAL.Contracts.Models;
using PF.App.Core.LibUI;
using PF.App.Core.ViewModels.Components;

namespace PF.App.Core.ViewModels
{
    public class GroupsListViewModel : BaseViewModel
    {
        private readonly IGroupsService _groupsService;
        private readonly INavigationService _navigationService;
        private readonly IUiThreadInvoker _uiThreadInvoker;
        private readonly CancellationToken _cancellationToken;

        public GroupsListViewModel(IGroupsService groupsService, INavigationService navigationService, IUiThreadInvoker uiThreadInvoker)
        {
            _groupsService = groupsService;
            _navigationService = navigationService;
            _uiThreadInvoker = uiThreadInvoker;
            _cancellationToken = new CancellationToken();

            Groups = new ObservableCollection<GroupItemViewModel>();

            //groupsService.GetGroups(OnGroupsDataUpdate, _cancellationToken);
            groupsService.GetTest(OnGroupsDataUpdate);
        }

        public ICommand CreateGroupCommand { get; }
        public ObservableCollection<GroupItemViewModel> Groups { get; }

        private Task OnGroupsDataUpdate(IEnumerable<Group> groups) => _uiThreadInvoker.InvokeOnUIThreadAsync(() => 
        {
            Groups.Clear();

            foreach (var group in groups)
            {
                Groups.Add(
                    new GroupItemViewModel(
                        group,
                        new SimpleCommand(() => OnGroupClicked(group))));
            }
        });

        private async Task OnGroupClicked(Group group)
        {
            IsBusy = true;
            await _navigationService.NavigateNextToAsync<GroupViewModel>();
            IsBusy = false;
        }
}
}