using PF.DTO.Users;
using PF.Mobile.App.DAL;
using PF.Mobile.App.DAL.Api;
using PF.Mobile.App.Models;
using PF.Mobile.App.ViewModels.Items;
using PF.Mobile.App.Views.Items;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PF.Mobile.App.ViewModels
{
    public class FriendsViewModel : BaseViewModel
    {
        private bool noPeople;
        private bool allowGrouping = true;
       
        public bool NoPeople
        {
            get => noPeople;
            set => SetProperty(ref noPeople, value);
        }

        public bool AllowGrouping
        {
            get => allowGrouping;
            set => SetProperty(ref allowGrouping, value);
        }

        public Command AddFriendCommand { get; }
        public ObservableCollection<PeopleGroup> GroupedPeople { get; set; }
        public PeopleGroup InvitationsGroup { get; set; }
        public PeopleGroup FriendsGroup { get; set; }

        public class PeopleGroup : ObservableCollection<PersonItemViewModel>
        {
            public string GroupName { get; private set; }

            public PeopleGroup(string name)
            {
                GroupName = name;
            }

            public PeopleGroup(string name, IEnumerable<PersonItemViewModel> source) : base (source)
            {
                GroupName = name;
            }
        }
        

        public FriendsViewModel()
        {
            AddFriendCommand = new Command(async () => await OnAddFriendClick(), () => !IsBusy);
            GroupedPeople = new ObservableCollection<PeopleGroup>();

            DataManager.GetData(DataManager.DataType.Friendships, OnFriendshipsUpdate);
        }

        private void OnFriendshipsUpdate(object data)
        {
            IEnumerable<FriendshipDTO> friendships = (IEnumerable<FriendshipDTO>)data;

            FriendsGroup = new PeopleGroup("Friends");
            InvitationsGroup = new PeopleGroup("Invitations");

            foreach (var friendship in friendships)
            {
                if (friendship.Status == 0)
                {
                    if (friendship.UserOneId != SessionManager.UserId)
                    {
                        InvitationsGroup.Add(
                            new PersonItemViewModel
                            (
                                id: friendship.UserOneId,
                                onPositiveButtonClick: AddFriend,
                                "+",
                                onNegativeButtonClick: RemoveFriend,
                                "-"
                            ));
                    }
                }
                else
                {
                    long friendId = friendship.UserOneId == SessionManager.UserId ? friendship.UserTwoId : friendship.UserOneId;

                    FriendsGroup.Add(
                        new PersonItemViewModel
                            (
                                id: friendId,
                                onItemTapped: OnFriendTapped
                            ));
                }
            }


            Device.InvokeOnMainThreadAsync(() =>
            {
                GroupedPeople.Clear();

                if (InvitationsGroup.Count > 0)
                {
                    GroupedPeople.Add(InvitationsGroup);
                }
                if (FriendsGroup.Count > 0)
                {
                    GroupedPeople.Add(FriendsGroup);
                }

                //AllowGrouping = true;
                //AllowGrouping = GroupedPeople.Count > 1;
                //AllowGrouping = GroupedPeople.Count > 1;


                List<long> ids = friendships.Select(x => x.UserOneId).ToList();
                ids.AddRange(friendships.Select(x => x.UserTwoId));
                ids = ids.Distinct().ToList();

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
                    PersonItemViewModel person = FriendsGroup.Where(x => x.UserId == user.Id).FirstOrDefault();
                    if (person == null)
                    {
                        person = InvitationsGroup.Where(x => x.UserId == user.Id).FirstOrDefault();
                    }

                    if (person != null)
                    {
                        person.UpdateUserData(user);
                    }
                }
            });
        }

        private async Task OnAddFriendClick()
        {
            var userDialogs = Acr.UserDialogs.UserDialogs.Instance;
            var input = await userDialogs.PromptAsync("Add new friend", "Enter your friend name", "Add", "Cancel");

            if (input.Ok)
            {
                string toastMessage = "something goes wrong";
                var result = await ApiClient.AddFriendByName(input.Text);

                if (result == null || result.StatusCode == System.Net.HttpStatusCode.NotFound)
                    toastMessage = $"Not found {input.Text}";
                else if (result.ResultContent != null)
                {
                    if (result.ResultContent.Status == 0)
                        toastMessage = "Invited";
                    else
                        toastMessage = "Accepted";
                }
                else if (result.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    toastMessage = "Unauthorized";


                userDialogs.Toast(toastMessage);
            }
        }


        private async Task OnFriendTapped(PersonItemViewModel person)
        {
           IsBusy = true;

            string[] buttons = new string[] { "Remove friend" };

            var choice = await Acr.UserDialogs.UserDialogs.Instance.ActionSheetAsync(
                null, "Cancel", null, System.Threading.CancellationToken.None, buttons);

            if (!string.IsNullOrEmpty(choice))
            {
                if (choice == "Remove friend")
                {
                    await RemoveFriend(person);
                }
            }

            IsBusy = false;
        }

        private async Task AddFriend(PersonItemViewModel person)
        {
            person.IsBusy = true;
            var result = await ApiClient.AddFriendById(person.UserId);
            DataManager.GetData(DataManager.DataType.Friendships, OnFriendshipsUpdate);
            person.IsBusy = false;
        }

        private async Task RemoveFriend(PersonItemViewModel person)
        {
            person.IsBusy = true;
            var result = await ApiClient.RemoveFriend(person.UserId);
            DataManager.GetData(DataManager.DataType.Friendships, OnFriendshipsUpdate);
            person.IsBusy = false;
        }
    }
}
