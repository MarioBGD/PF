using PF.DTO.Groups;
using PF.DTO.Users;
using PF.Mobile.App.DAL;
using PF.Mobile.App.DAL.Api;
using PF.Mobile.App.ViewModels.Items;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PF.Mobile.App.ViewModels.Popups
{
    public class InviteFriendsPopupViewModel : BaseViewModel
    {
        private bool changed;
        private long GroupId;
        private IEnumerable<MembershipDTO> Memberships;

        public Command OkCommand { get; }
        


         public ObservableCollection<PersonItemViewModel> Friends { get; set; }

        public InviteFriendsPopupViewModel(long groupId, IEnumerable<MembershipDTO> memberships)
        {
            GroupId = groupId;
            Memberships = memberships;

            OkCommand = new Command(async () => await Ok(), () => !IsBusy);
            Friends = new ObservableCollection<PersonItemViewModel>();

            DataManager.GetData(DataManager.DataType.Friendships, OnFriendshipsUpdate);
        }

        private void OnFriendshipsUpdate(object data)
        {
            IEnumerable<FriendshipDTO> friendships = (IEnumerable<FriendshipDTO>)data;
            friendships = friendships.Where(x => x.Status == FriendshipDTO.FriendshipStatus.Accepted);

            Device.InvokeOnMainThreadAsync(() =>
           {
               foreach (var friendship in friendships)
               {

                   long friendId = friendship.UserOneId == SessionManager.UserId ? friendship.UserTwoId : friendship.UserOneId;

                   if (!Memberships.Select(x => x.UserId).Contains(friendId))
                   {
                       Friends.Add(new PersonItemViewModel(friendId, OnFriendClicked, "Invite"));
                   }
               }

               List<long> ids = friendships.Select(x => x.UserOneId).ToList();
               ids.AddRange(friendships.Select(x => x.UserTwoId));
               ids = ids.Distinct().Where(x => !Memberships.Select(p => p.UserId).Contains(x)).ToList();

               DataManager.GetData(DataManager.DataType.Users, OnUsersDataUpdate, ids);
           });
        }

        private void OnUsersDataUpdate(object data)
        {
            IEnumerable<UserDTO> users = (IEnumerable<UserDTO>)data;

            Device.InvokeOnMainThreadAsync(() =>
            {
                foreach (var user in users)
                {
                    PersonItemViewModel person = Friends.Where(x => x.UserId == user.Id).FirstOrDefault();

                    if (person != null)
                    {
                        person.UpdateUserData(user);
                    }
                }
            });
        }

        private async Task OnFriendClicked(PersonItemViewModel person)
        {
            person.IsBusy = true;

            var result = await ApiClient.AddPersonToGroup(person.UserId, GroupId);
            string toastMessage = "Added";

            if (result == null || result.StatusCode != System.Net.HttpStatusCode.OK)
            {
                toastMessage = "error";
                person.IsBusy = false;
            }
            else
            {
                changed = true;
                await Device.InvokeOnMainThreadAsync(() =>
                {
                    person.PositiveButtonText = "Invited";
                });
            }

            Acr.UserDialogs.UserDialogs.Instance.Toast(toastMessage);
        }

        private async Task Ok()
        {
            await Rg.Plugins.Popup.Services.PopupNavigation.Instance.PopAsync();
            OnPoped?.Invoke(changed);
        }
    }
}
