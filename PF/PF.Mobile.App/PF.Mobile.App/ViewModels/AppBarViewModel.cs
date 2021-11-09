using PF.DTO.Users;
using PF.Mobile.App.DAL;
using PF.Mobile.App.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PF.Mobile.App.ViewModels
{
    public class AppBarViewModel : BaseViewModel
    {
        private static AppBarViewModel instance;
        private string barName;
        private string header;
        private string barNameInitials;
        private string barLogin;
        private string barBalance;
        private ImageSource avatarImage;
        private bool showPersonalInfo;


        public static AppBarViewModel Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AppBarViewModel();
                }

                return instance;
            }

            private set => instance = value;
        }
        
        
        public string Header
        {
            get => header;
            set => SetProperty(ref header, value);
        }
        public string BarName
        {
            get => barName;
            set => SetProperty(ref barName, value);
        }
        public string BarNameInitials
        {
            get => barNameInitials;
            set => SetProperty(ref barNameInitials, value);
        }
        public string BarLogin
        {
            get => barLogin;
            set => SetProperty(ref barLogin, value);
        }
        public string BarBalance
        {
            get => barBalance;
            set => SetProperty(ref barBalance, value);
        }
        public ImageSource AvatarImage
        {
            get => avatarImage;
            set => SetProperty(ref avatarImage, value);
        }
        public bool ShowPersonalInfo
        {
            get => showPersonalInfo;
            set => SetProperty(ref showPersonalInfo, value);
        }

        public Command ProfileCommand { get; }
          

        private AppBarViewModel()
        {
            ProfileCommand = new Command(async ()=> await OnProfileClick(), () => !IsBusy);

            DataManager.Init();
        }

        public void Refresh()
        {
            DataManager.GetData(DataManager.DataType.MyUser, OnMyUserDataUpdate);
        }

        public void OnMyUserDataUpdate(object data)
        {
            try
            {
                UserDTO userDTO = (UserDTO)data;
                BarLogin = userDTO.Login;
                BarName = userDTO.Name + " " + userDTO.LastName;
                BarNameInitials = PF.Common.Util.GetInitials(userDTO.Name, userDTO.LastName);
                AvatarImage = AvatarSourceToImageConverter.Convert(userDTO.AvatarSrc);
                SessionManager.UserDTO = userDTO;
            }
            catch (Exception e)
            {

            }
        }

        public async Task OnProfileClick()
        {
            IsBusy = true;

            string[] buttons = new string[] { "Log out", "Change profile image" };

            var choice = await Acr.UserDialogs.UserDialogs.Instance.ActionSheetAsync(
                null, "Cancel", null, System.Threading.CancellationToken.None, buttons);

            if (!string.IsNullOrEmpty(choice))
            {
                if (choice == "Log out")
                {
                    await App.LogOut();
                }
                else if (choice == "Change profile image")
                {
                    var view = new Views.Popups.SelectProfilePicturePopupView();
                    await Rg.Plugins.Popup.Services.PopupNavigation.Instance.PushAsync(view);
                }
            }

            IsBusy = false;
        }
    }
}
