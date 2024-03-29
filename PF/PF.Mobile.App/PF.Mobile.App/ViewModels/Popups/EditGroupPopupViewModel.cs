﻿using PF.Common.Currencies;
using PF.DTO.Groups;
using PF.Mobile.App.DAL.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PF.Mobile.App.ViewModels.Popups
{
    public class EditGroupPopupViewModel : BaseViewModel
    {
       private GroupDTO editingGroupDTO;

        private string groupName;
        private string groupDefaultCurrency;
        private string confirmButtonText;

        public string GroupName
        {
            get => groupName;
            set => SetProperty(ref groupName, value);
        }

        public string GroupDefaultCurrency
        {
            get => groupDefaultCurrency;
            set => SetProperty(ref groupDefaultCurrency, value);
        }

        public string ConfirmButtonText
        {
            get => confirmButtonText;
            set => SetProperty(ref confirmButtonText, value);
        }

        public Command SelectCurencyCommand { get; }
        public Command ConfirmCommand { get; }
        public Command CancelCommand { get; }

        public EditGroupPopupViewModel(GroupDTO groupDTO)
        {
            SelectCurencyCommand = new Command(async () => await SelectCurrency(), () => !IsBusy);
            ConfirmCommand = new Command(async () => await Confirm(), () => !IsBusy);
            CancelCommand = new Command(async () => await Cancel(), () => !IsBusy);

            editingGroupDTO = groupDTO;
            if (groupDTO != null)
            {
                GroupName = groupDTO.Name;
                //GroupDefaultCurrency //TODO: C 
                confirmButtonText = "Apply";
            }
            else
            {
                GroupDefaultCurrency = "PLN";
                confirmButtonText = "Create";
            }
        }

        public async Task SelectCurrency()
        {
            IsBusy = true;

            var choice = await Acr.UserDialogs.UserDialogs.Instance.ActionSheetAsync(
                null, "Cancel", null, System.Threading.CancellationToken.None, Currencies.CurrenciesList);

           
            if (!string.IsNullOrEmpty(choice) && Currencies.CurrenciesList.Contains(choice))
            {
                GroupDefaultCurrency = choice;
            }

            IsBusy = false;
        }

        public async Task Confirm()
        {
            IsBusy = true;

            if (!string.IsNullOrEmpty(GroupName) &&
                !string.IsNullOrEmpty(GroupDefaultCurrency) &&
                Currencies.CurrenciesList.Contains(GroupDefaultCurrency))
            {
                GroupDTO group = new GroupDTO
                {
                    Name = GroupName,
                    DefaultCurrencyId = 3 //TODO: C
                };
                var res = await ApiClient.CreateGroup(group);

                if (res.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    OnPoped(true);
                    Acr.UserDialogs.UserDialogs.Instance.Toast("Created");
                    await Rg.Plugins.Popup.Services.PopupNavigation.Instance.PopAsync();
                }
                else
                    Acr.UserDialogs.UserDialogs.Instance.Toast("Error");
            }

            IsBusy = false;
        }

        public async Task Cancel()
        {
            await Rg.Plugins.Popup.Services.PopupNavigation.Instance.PopAsync();
        }
    }
}
