using PF.DTO.Groups;
using PF.Mobile.App.DAL;
using PF.Mobile.App.Models;
using PF.Mobile.App.ViewModels.Items;
using PF.Mobile.App.ViewModels.Popups;
using PF.Mobile.App.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PF.Mobile.App.ViewModels
{
    public class GroupsViewModel : BaseViewModel
    {
        private List<GroupDTO> GroupsData { get; set; }

        public Command CreateGroupCommand { get; }

        public ObservableCollection<GroupItemViewModel> GroupsList { get; set; }

        public GroupsViewModel()
        {
            CreateGroupCommand = new Command(async () => await OnCreateGroupClicked(), () => !IsBusy);

            GroupsData = new List<GroupDTO>();
            GroupsList = new ObservableCollection<GroupItemViewModel>();

            DataManager.GetData(DataManager.DataType.Memberships, OnMembershipsDataUpdate);

            Task.Run(async () =>
            {
                string state = await Xamarin.Essentials.SecureStorage.GetAsync("state");

                if (string.IsNullOrEmpty(state))
                {
                    return;
                }

                long groupId = long.Parse(state);

                if (groupId == 0)
                {
                    return;
                }

                IsBusy = true;
                await OnGroupEnter(new GroupItemViewModel(groupId, null));
                IsBusy = false;
            });
        }

        public void OnMembershipsDataUpdate(object data)
        {
            var memberships = (IEnumerable<MembershipDTO>)data;

            Device.InvokeOnMainThreadAsync(() =>
            {
                GroupsList.Clear();

                foreach (var membership in memberships)
                {
                    GroupItemViewModel groupModel;
                    groupModel = new GroupItemViewModel(membership.GroupId, OnGroupEnter);
                    GroupsList.Add(groupModel);
                }
            });

            List<long> missingGropsDataIds = memberships.Select(x => x.GroupId).Where(x => !GroupsData.Select(g => g.Id).Contains(x)).ToList();
            DataManager.GetData(DataManager.DataType.Groups, UpdateGroupsData, missingGropsDataIds);
        }

        private void UpdateGroupsData(object data)
        {
            IEnumerable<GroupDTO> groups = (IEnumerable<GroupDTO>)data;

            Device.InvokeOnMainThreadAsync(() =>
            {
                foreach (var groupDTO in groups)
                {
                    var groupModel = GroupsList.Where(x => x.GroupId == groupDTO.Id).FirstOrDefault();
                    groupModel?.UpdateData(groupDTO);
                    GroupsData.Add(groupDTO);
                }
            });
        }


        private async Task OnCreateGroupClicked()
        {
            IsBusy = true;
            var view = new Views.Popups.EditGroupPopupView(null);
            ((EditGroupPopupViewModel)view.BindingContext).OnPoped = GroupEditPoped;
            await Rg.Plugins.Popup.Services.PopupNavigation.Instance.PushAsync(view);

            //var view = new Views.Popups.ExpensePopupView();
            //await Rg.Plugins.Popup.Services.PopupNavigation.Instance.PushAsync(view);

            IsBusy = false;
        }

        private void GroupEditPoped(object result)
        {
            if ((bool)result)
                DataManager.GetData(DataManager.DataType.Memberships, OnMembershipsDataUpdate);
        }

        private async Task OnGroupEnter(GroupItemViewModel group)
        {
            IsBusy = true;

            App.CurrentGroupId = group.GroupId;
            App.CurrentGroup = GroupsData.Where(p => p.Id == group.GroupId).FirstOrDefault();
            await Application.Current.MainPage.Navigation.PushAsync(new GroupView(group.GroupId));

            IsBusy = false;
            //remove group
            //IsBusy = true;
            //var res = await PayFair.Mobile.BLL.ApiClient.ApiClient.DeleteGroup(group.Id);
            //if (res.StatusCode == System.Net.HttpStatusCode.OK)
            //{
            //    Acr.UserDialogs.UserDialogs.Instance.Toast("Deleted");
            //    DataManager.GetData(DataManager.DataType.Groups, OnGroupsDataUpdate);
            //}
            //IsBusy = false;
        }
    }
}
