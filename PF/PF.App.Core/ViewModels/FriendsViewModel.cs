using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using PF.App.Contracts;
using PF.App.Contracts.Components;
using PF.App.Core.DAL.Contracts;
using PF.App.Core.DAL.Contracts.Models;
using PF.App.Core.LibUI;
using PF.App.Core.Models;

namespace PF.App.Core.ViewModels
{
    public class FriendsViewModel : BaseViewModel
    {
        private readonly IFriendsService _friendsService;
        private readonly IUiThreadInvoker _uiThreadInvoker;
        private readonly IDialogsManager _dialogsManager;
        
        private bool noPeople;
        private bool allowGrouping = true;

        public FriendsViewModel(IFriendsService friendsService, IUiThreadInvoker uiThreadInvoker, IDialogsManager dialogsManager)
        {
            _friendsService = friendsService;
            _uiThreadInvoker = uiThreadInvoker;
            _dialogsManager = dialogsManager;
            
            AddFriendCommand = new SimpleCommand(OnAddFriendClick);
            GroupedPeople = new ObservableCollection<PeopleGroup>();

            //_friendsService.GetFriends(OnFriendsUpdate);
            _friendsService.GetTest(OnFriendsUpdate);
        }
        
        public ICommand AddFriendCommand { get; }
        public ObservableCollection<PeopleGroup> GroupedPeople { get; }
        public PeopleGroup InvitationsGroup { get; set; }
        public PeopleGroup FriendsGroup { get; set; }
        
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

        private void OnFriendsUpdate(IEnumerable<Friend> friends)
        {
            _uiThreadInvoker.InvokeOnUIThreadAsync(() =>
            {
                GroupedPeople.Clear();
                FriendsGroup = new PeopleGroup("Friends");
                InvitationsGroup = new PeopleGroup("Invitations");

                foreach (var friend in friends)
                {
                    PersonComponentViewModel person = null;
                    
                    if (friend.Status == Friend.FriendshipStatus.Invitation)
                    {
                        person = new PersonComponentViewModel
                        (
                            id: friend.FriendId,
                            onPositiveButtonClick: AddFriend,
                            "+",
                            onNegativeButtonClick: RemoveFriend,
                            "-"
                        );
                        
                        InvitationsGroup.Add(person);
                    }
                    else
                    {
                        person = new PersonComponentViewModel
                        (
                            id: friend.FriendId,
                            onItemTapped: OnFriendTapped
                        );
                        
                        FriendsGroup.Add(person);
                    }

                    person.Name = friend.Name + " " + friend.LastName;
                }
                
                if (InvitationsGroup.Count > 0)
                {
                    GroupedPeople.Add(InvitationsGroup);
                }
                if (FriendsGroup.Count > 0)
                {
                    GroupedPeople.Add(FriendsGroup);
                }
            });
        }
        
        private async Task OnAddFriendClick()
        {
            var input = await _dialogsManager.ShowPrompt("Add new friend", "Enter your friend name", "Add", "Cancel");
            
            if (!string.IsNullOrEmpty(input))
            {
                var result = await _friendsService.AddFriendByName(input);

                if (result.Success)
                {
                    _dialogsManager.ShowToast("Invited");
                }
                else
                {
                    _dialogsManager.ShowToast("Something went wrong");
                }
            }
        }
        
        private async Task OnFriendTapped(PersonComponentViewModel person)
        {
           IsBusy = true;
           
           string[] buttons = new string[] { "Remove friend" };
           
           var choice = await _dialogsManager.ShowList(buttons);
           
           if (!string.IsNullOrEmpty(choice))
           {
               if (choice == "Remove friend")
               {
                   await RemoveFriend(person);
               }
           }
           
           IsBusy = false;
        }

        private async Task AddFriend(PersonComponentViewModel person)
        {
            person.IsBusy = true;
            
            var result = await _friendsService.AddFriendAsync(person.UserId);
            
            if (result.Success)
            {
                _dialogsManager.ShowToast("Added");
                _friendsService.GetFriends(OnFriendsUpdate);
            }
            else
            {
                _dialogsManager.ShowToast("Something went wrong");
            }

            person.IsBusy = false;
        }

        private async Task RemoveFriend(PersonComponentViewModel person)
        {
            person.IsBusy = true;
            
            var result = await _friendsService.RemoveFriendAsync(person.UserId);
            if (result.Success)
            {
                _dialogsManager.ShowToast("Friend removed successfully");
                _friendsService.GetFriends(OnFriendsUpdate);
            }
            else
            {
                _dialogsManager.ShowToast("Something went wrong");
            }

            person.IsBusy = false;
        }
    }
}