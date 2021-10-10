using PF.DTO.Users;
using PF.Mobile.App.DAL;
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
    public class PeopleSelectPopupViewModel : BaseViewModel
    {
        public delegate void OnSelectorCloseDel(IEnumerable<long> selected);

        private IEnumerable<long> PeopleIds { get; }
        private OnSelectorCloseDel OnSelectorClose { get; }

        public ObservableCollection<PersonItemViewModel> People { get; set; }

        public Command OkCommand { get; }

        public PeopleSelectPopupViewModel(IEnumerable<long> peopleIds, IEnumerable<long> selected, OnSelectorCloseDel onSelectorClose, bool multiChoice = true)
        {
            PeopleIds = peopleIds;
            OnSelectorClose = onSelectorClose;

            OkCommand = new Command(async () => await OkClick(), () => !IsBusy);

            People = new ObservableCollection<PersonItemViewModel>();

            Device.InvokeOnMainThreadAsync(() =>
            {
                foreach (long id in peopleIds)
                {
                    People.Add(new PersonItemViewModel(id, OnPersonTapped, selected != null && selected.Contains(id)));
                }

                DataManager.GetData(DataManager.DataType.Users, OnUsersDataUpdate, peopleIds.ToList());
            });
        }

        public async Task OkClick()
        {
            IEnumerable<long> selected = People.Where(p => p.IsChecked).Select(p => p.UserId); 
            OnSelectorClose(selected);

            await Rg.Plugins.Popup.Services.PopupNavigation.Instance.PopAsync();
        }

        public void OnUsersDataUpdate(object data)
        {
            IEnumerable<UserDTO> users = (IEnumerable<UserDTO>)data;

            Device.InvokeOnMainThreadAsync(() =>
            {
                foreach (var user in users)
                {
                    PersonItemViewModel person = People.Where(x => x.UserId == user.Id).FirstOrDefault();
                    
                    if (person != null)
                    {
                        person.UpdateUserData(user);
                    }
                }
            });
        }

        private async Task OnPersonTapped(PersonItemViewModel person)
        {
            person.Check(!person.IsChecked);
        }
    }
}
