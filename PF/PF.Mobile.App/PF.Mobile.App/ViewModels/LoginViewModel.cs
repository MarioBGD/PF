using PF.Common.Crypto;
using PF.DTO.Users;
using PF.Mobile.App.DAL;
using PF.Mobile.App.DAL.Api;
using PF.Mobile.App.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PF.Mobile.App.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private IHasher hasher;

        private bool isRegisterForm;
        private string submitText;
        private string errorText;
        private string secondOptionTextInfo;
        private string secondOptionTextButton;


        public LoginModel AuthData { get; set; }

        public bool IsRegisterForm
        {
            get => isRegisterForm;
            set => SetProperty(ref isRegisterForm, value);
        }

        public string SubmitText
        {
            get => submitText;
            set => SetProperty(ref submitText, value);
        }

        public string ErrorText
        {
            get => errorText;
            set => SetProperty(ref errorText, value);
        }

        public string SecondOptionTextInfo
        {
            get => secondOptionTextInfo;
            set => SetProperty(ref secondOptionTextInfo, value);
        }

        public string SecondOptionTextButton
        {
            get => secondOptionTextButton;
            set => SetProperty(ref secondOptionTextButton, value);
        }

        public Command SubmitCommand { get; }
        public Command LogRegToggleCommand { get; }

        public LoginViewModel()
        {
            AuthData = new LoginModel();
            OnLogRegToggleClick();

            SubmitCommand = new Command(async () => await OnSubmitClick(), () => !IsBusy);
            LogRegToggleCommand = new Command(OnLogRegToggleClick, () => !IsBusy);

            hasher = new SHA512();
        }


        private void OnLogRegToggleClick()
        {
            IsRegisterForm = !IsRegisterForm;

            if (IsRegisterForm)
            {
                SubmitText = "Register";
                SecondOptionTextButton = "Sign Up!";
                SecondOptionTextInfo = "Do you have an account?";
            }
            else
            {
                SubmitText = "Login";
                SecondOptionTextButton = "Register!";
                SecondOptionTextInfo = "Don't have an account yet?";
            }
        }

        
        private async Task OnSubmitClick()
        {
            IsBusy = true;

            var authValidator = new Common.Validators.AuthValidator();

            try
            {
                authValidator.ValidateLogin(AuthData.Email);
                authValidator.ValidatePassword(AuthData.Password);

                if (IsRegisterForm)
                {
                    
                }
            }
            catch (Exception e)
            {
                AuthData.ErrorMessage = e.Message;
                ErrorText = e.Message;

                IsBusy = false;
                return;
            }

            AuthDTO authDTO = new AuthDTO
            {
                Login = AuthData.Email,
                HashedPassword = hasher.HashToBase64String(AuthData.Password),
                
                Name = AuthData.Name,
                LastName = AuthData.LastName
            };

            if (IsRegisterForm)
            {
                var regRes = await ApiClient.Register(authDTO);
                if (regRes.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    ErrorText = "User arleady exist";

                    IsBusy = false;
                    return;
                }
            }

            bool logged = await SessionManager.Authorize(authDTO.Login, authDTO.HashedPassword) == SessionManager.SessionState.Authorized;

            if (logged)
            {
                Device.InvokeOnMainThreadAsync(() => { Application.Current.MainPage = new NavigationPage(new Views.MainView()); });
                await OnLogged(authDTO);
            }
            else
            {
                ErrorText = "Login problem";
            }

            IsBusy = false;
        }



        private async Task OnLogged(AuthDTO authDTO)
        {
            await SecureStorage.SetAsync("usn", authDTO.Login);
            await SecureStorage.SetAsync("psw", authDTO.HashedPassword);
        }

        public override void OnIsBusyChanged()
        {
            SubmitCommand.ChangeCanExecute();
            LogRegToggleCommand.ChangeCanExecute();
        }
    }
}
