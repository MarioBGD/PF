using PF.DTO.Users;
using PF.Mobile.App.DAL;
using PF.Mobile.App.DAL.Api;
using PF.Mobile.App.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PF.Mobile.App.ViewModels.Popups
{
    public class SelectProfilePicturePopupViewModel : BaseViewModel
    {
        public SelectProfilePicturePopupViewModel()
        {
            Avatars = new ObservableCollection<AvatarModel>();

            OnCancelCommand = new Command(async () => await OnCancel());
            OnSaveCommand = new Command(async () => await OnSave());

            for (int i = 1; i <= 2; i++)
            {
                Avatars.Add(new AvatarModel(OnAvatarSelected)
                {
                    AvatarName = "av" + i.ToString(),
                    AvatarSource = ImageSource.FromResource("PF.Mobile.App.Resources.Avatars.av" + i.ToString() + ".png")
                });
            }
        }

        public AvatarModel SelectedAvatar { get; set; }
        public ObservableCollection<AvatarModel> Avatars { get; set; }
        public Command OnCancelCommand { get; }
        public Command OnSaveCommand { get; }


        private void OnAvatarSelected(AvatarModel avatarModel)
        {
            SelectedAvatar = avatarModel;
        }

        private async Task OnCancel()
        {
            IsBusy = true;

            await Rg.Plugins.Popup.Services.PopupNavigation.Instance.PopAsync();
        }

        private async Task OnSave()
        {
            if (SelectedAvatar == null)
            {
                return;
            }

            IsBusy = true;

            var oldAvatarSrc = SessionManager.UserDTO.AvatarSrc;
            SessionManager.UserDTO.AvatarSrc = SelectedAvatar.AvatarName;

            var apiRes = await ApiClient.UpdateMyUser(SessionManager.UserDTO);

            if (apiRes.StatusCode != System.Net.HttpStatusCode.OK)
            {
                SessionManager.UserDTO.AvatarSrc = oldAvatarSrc;
                Acr.UserDialogs.UserDialogs.Instance.Toast("Cannot change profile picture");
            }
            else
            {
                await Rg.Plugins.Popup.Services.PopupNavigation.Instance.PopAsync();
            }

            IsBusy = false;
        }
    }
}
