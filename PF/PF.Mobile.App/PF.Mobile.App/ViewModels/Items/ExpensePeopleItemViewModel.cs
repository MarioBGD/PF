using PF.DTO.Users;
using PF.Mobile.App.DAL;
using PF.Mobile.App.Models;
using PF.Mobile.App.Views.Items;
using PF.Mobile.App.Views.Popups;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PF.Mobile.App.ViewModels.Items
{
    public class ExpensePeopleItemViewModel : BaseViewModel
    {
        public delegate void OnChanged();
        private OnChanged onChanged;

        private const int _ItemHeight = 65;
        private bool showSmallAddButton;
        private bool showBigAddButton;
        private double height;

        public bool DisableMultiPeople;

        public bool ShowSmallAddButton
        {
            get => showSmallAddButton;
            set => SetProperty(ref showSmallAddButton, value);
        }

        public bool ShowBigAddButton
        {
            get => showBigAddButton;
            set => SetProperty(ref showBigAddButton, value);
        }

        public double Height
        {
            get => height;
            set => SetProperty(ref height, value);
        }

        public ObservableCollection<ExpensePersonModel> People { get; set; }

        public Command AddPersonCommand { get; set; }
        

        public ExpensePeopleItemViewModel(OnChanged onChanged)
        {
            this.onChanged = onChanged;
            People = new ObservableCollection<ExpensePersonModel>();

            AddPersonCommand = new Command(async () => await OnAddPeople(), () => !IsBusy);
            RefreshView();
        }

        private void RefreshView()
        {
            //resize list
            int height = People.Count * _ItemHeight;
            Height = height;

            //buttons
            ShowBigAddButton = People.Count == 0;
            ShowSmallAddButton = !ShowBigAddButton;
            if (DisableMultiPeople)
                ShowSmallAddButton = false;
        }

        private async Task OnAddPeople()
        {
            var view = new PeopleSelectPopupView(MembersViewModel.Memberships.Select(p => p.UserId), People.Select(p => p.UserId), OnSelectorClose, !DisableMultiPeople);
            await Rg.Plugins.Popup.Services.PopupNavigation.Instance.PushAsync(view);
        }

        public void AddPerson(long personId)
        {
            var person = new ExpensePersonModel(personId, OnRemoveClick);
            People.Add(person);

            DataManager.GetData(DataManager.DataType.Users,
                (object data) =>
                {
                    var users = ((IEnumerable<UserDTO>)data).ToList();

                    person.Name = users[0].Name;
                },
                new List<long> { personId });

            RefreshView();
        }

        private void OnSelectorClose(IEnumerable<long> selected)
        {
            Device.InvokeOnMainThreadAsync(() =>
            {
                People.Clear();

                foreach (long id in selected)
                {
                    AddPerson(id);
                }

                RefreshView();
                onChanged();
            });
        }

        private async Task OnRemoveClick(ExpensePersonModel person)
        {
            await Device.InvokeOnMainThreadAsync(() =>
            {
                 People.Remove(person);
                 onChanged();
            });
        }
    }
}
