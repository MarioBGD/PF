using PF.DTO.Expenses;
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

namespace PF.Mobile.App.ViewModels
{
    public class MembersViewModel : BaseViewModel
    {
        public static IEnumerable<MembershipDTO> Memberships;

        public Command AddMemberCommand { get; }
        public ObservableCollection<PersonItemViewModel> Members { get; set; }


        public MembersViewModel()
        {
            AddMemberCommand = new Command(async () => await OnAddMemberClick(), () => !IsBusy);
            Members = new ObservableCollection<PersonItemViewModel>();

            DataManager.GetData(DataManager.DataType.Memberships, OnMembershipsUpdate, App.CurrentGroupId);
        }

        private async Task OnAddMemberClick()
        {
            IsBusy = true;
            var view = new Views.Popups.InviteFriendPopupView(App.CurrentGroupId, Memberships);
            view.ViewModel.OnPoped += (object result) =>
            {
                if ((bool)result == true)
                {
                    DataManager.GetData(DataManager.DataType.Memberships, OnMembershipsUpdate, App.CurrentGroupId);
                }
            };
            await Rg.Plugins.Popup.Services.PopupNavigation.Instance.PushAsync(view);
            IsBusy = false;
        }

        private void OnMembershipsUpdate(object data)
        {
            Memberships = (IEnumerable<MembershipDTO>)data;

            Device.InvokeOnMainThreadAsync(() =>
             {
                 Members.Clear();

                 foreach (var membership in Memberships)
                 {
                     Members.Add(new PersonItemViewModel(membership.UserId, OnMembershipTapped, 0));
                 }

                 List<long> ids = Memberships.Select(x => x.UserId).ToList();
                 DataManager.GetData(DataManager.DataType.Users, OnUsersDataUpdate, ids);
                 DataManager.GetData(DataManager.DataType.Balances, OnBalancesDataUpdate, new Models.BalanceRequestModel { UserIds = ids, GroupId = App.CurrentGroupId});
             });
        }

        private void OnUsersDataUpdate(object data)
        {
            IEnumerable<UserDTO> users = (IEnumerable<UserDTO>)data;

            Device.InvokeOnMainThreadAsync(() =>
            {
                foreach (var user in users)
                {
                    PersonItemViewModel person = Members.Where(x => x.UserId == user.Id).FirstOrDefault();

                    if (person != null)
                    {
                        person.UpdateUserData(user);
                    }
                }
            });
        }

        private void OnBalancesDataUpdate(object data)
        {
            IEnumerable<BalanceDTO> balances = (IEnumerable<BalanceDTO>)data;

            Device.InvokeOnMainThreadAsync(() =>
            {
                foreach (var balance in balances)
                {
                    PersonItemViewModel person = Members.Where(x => x.UserId == balance.UserId).FirstOrDefault();

                    if (person != null && person.Amount != null)
                    {
                        person.Amount.Currency = (Common.Currencies.Currencies.CurrencyCodes)App.CurrentGroup.DefaultCurrencyId;
                        person.Amount.Amount = balance.Balance;
                    }
                }
            });
        }

        private async Task OnMembershipTapped(PersonItemViewModel person)
        {
           IsBusy = true;

            if (person.UserId == SessionManager.UserId)
            {
                return;
            }

            List<string> buttons = new List<string>();
            
            if (SessionManager.UserId != person.UserId)
            {
                buttons.Add("Add expense");
            }
            else
            {

            }

            var choice = await Acr.UserDialogs.UserDialogs.Instance.ActionSheetAsync(
                null, "Cancel", null, System.Threading.CancellationToken.None, buttons.ToArray());

            if (!string.IsNullOrEmpty(choice))
            {
                if (choice == "Add expense")
                {
                    var expense = new ExpenseDTO
                    {
                        GroupId = App.CurrentGroupId,
                        Payments = new List<PaymentDTO>
                        {
                            new PaymentDTO { UserId = SessionManager.UserId, Amount = 10 },
                            new PaymentDTO { UserId = person.UserId, Amount = -10 },
                        }
                    };

                    var view = new Views.Popups.ExpensePopupView(expense);
                    await Rg.Plugins.Popup.Services.PopupNavigation.Instance.PushAsync(view);
                }
            }

            IsBusy = false;
        }
    }
}
