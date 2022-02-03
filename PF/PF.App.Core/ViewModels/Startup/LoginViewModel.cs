using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using PF.App.Contracts.Navigation;
using PF.App.Contracts.Storage;
using PF.App.Core.DAL.Contracts;
using PF.App.Core.LibUI;
using PF.App.Core.Models;
using PF.Common.Crypto;
using PF.DTO.Users;

namespace PF.App.Core.ViewModels.Startup
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly INavigationService _navigationService;
        private readonly IHasher _hasher;
        private readonly ISecureStorageService _secureStorageService;
        
        private bool isRegisterForm;
        private string submitText;
        private string errorText;
        private string secondOptionTextInfo;
        private string secondOptionTextButton;

        public LoginViewModel(IAuthenticationService authenticationService, IHasher hasher, ISecureStorageService secureStorageService, INavigationService navigationService)
        {
            _authenticationService = authenticationService;
            _hasher = hasher;
            _secureStorageService = secureStorageService;
            _navigationService = navigationService;

            SubmitCommand = new SimpleCommand(OnSubmitCommand);
            LogRegToggleCommand = new SimpleCommand(OnLogRegToggleCommand); //() => !IsBusy
            
            OnLogRegToggleCommand();
        }

        public ICommand SubmitCommand { get; }
        public ICommand LogRegToggleCommand { get; }
        
        public string Email { get; set; }
        public string Password { get; set; }
        public string RepeatPassword { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        
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
        
        
        private void OnLogRegToggleCommand()
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

        private async Task OnSubmitCommand()
        {
            IsBusy = true;
            
            if (Validate() == false)
            {
                IsBusy = false;
                return;
            }

            string hashedPassword = _hasher.HashToBase64String(Password);
            
            if (IsRegisterForm)
            {
                var registerRes = await _authenticationService.RegisterAsync(Email, hashedPassword, Name, LastName);
                
                if (!registerRes.Success)
                {
                    ErrorText = registerRes.Message;
                    IsBusy = false;
                    return;
                }
            }

            var loginRes = await _authenticationService.LoginAsync(Email, hashedPassword);

            if (loginRes?.Success == true)
            {
                await _secureStorageService.SaveStringAsync("usn", Email);
                await _secureStorageService.SaveStringAsync("psw", hashedPassword);

                await _navigationService.RemoveAllPagesAsync();
                await _navigationService.NavigateNextToAsync<MainViewModel>();
            }
            else
            {
                ErrorText = "Login problem:";
            }
        
            IsBusy = false;
        }

        private bool Validate()
        {
            try
            {
                var authValidator = new Common.Validators.AuthValidator();
                
                authValidator.ValidateLogin(Email);
                authValidator.ValidatePassword(Password);

                if (IsRegisterForm)
                {
                    if (RepeatPassword != Password)
                    {
                        throw new Exception("Niezgodnosc hasel");
                    }
                    
                    //TODO: Validate register data
                }
            }
            catch (Exception e)
            {
                ErrorText = e.Message;
                return false;
            }

            return true;
        }
    }
}
