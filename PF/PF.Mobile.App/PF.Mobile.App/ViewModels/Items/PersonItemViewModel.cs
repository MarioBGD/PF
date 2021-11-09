using PF.Mobile.App.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PF.Mobile.App.ViewModels.Items
{
    public class PersonItemViewModel : BaseViewModel
    {
        public enum Mode
        { Invitation, ViewWithoutBalance, ViewWithBalance, OnePosBtn, Selector }

        private bool isCheckded;
        private string name;
        private ImageSource avatarImage;
        private string positiveButtonText;
        private string negativeButtonText;
        private string checkStatusImageSource;


        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        public ImageSource AvatarImage
        {
            get => avatarImage;
            set => SetProperty(ref avatarImage, value);
        }

        public string PositiveButtonText
        {
            get => positiveButtonText;
            set => SetProperty(ref positiveButtonText, value);
        }

        public string NegativeButtonText
        {
            get => negativeButtonText;
            set => SetProperty(ref negativeButtonText, value);
        }

         public bool IsChecked
         {
            get => isCheckded;
            set => SetProperty(ref isCheckded, value);
         }

        public string CheckStatusImageSource
        {
            get => checkStatusImageSource;
            set => SetProperty(ref checkStatusImageSource, value);
        }

        public AmountItemViewModel Amount { get; set; }

        public bool VisibleBalance { get; set; }
        public bool VisiblePosButton { get; set; }
        public bool VisibleNegButton { get; set; }
        public bool VisibleSelector { get; set; }

        public long UserId { get; set; }


        public delegate Task PersonButtonClickDel(PersonItemViewModel person);
        public PersonButtonClickDel OnItemTapped;
        public PersonButtonClickDel OnPositiveButtonClick;
        public PersonButtonClickDel OnNegativeButtonClick;

        public Command TapCommand { get; }
        public Command PositiveCommand { get; }
        public Command NegativeCommand { get; }
        //public PersonItemViewModel(PersonButtonClickDel positive, PersonButtonClickDel negative)
        //{
        //    //PositiveCommand = new Command(async () => await positive(this), () => !IsBusy);
        //    //NegativeCommand = new Command(async () => await negative(this), () => !IsBusy);
        //}


        public PersonItemViewModel(long id,
            PersonButtonClickDel onPositiveButtonClick,
            string positiveButtonText,
            PersonButtonClickDel onNegativeButtonClick,
            string negativeButtonText)
        {

            UserId = id;
            Name = "user " + id;

            OnPositiveButtonClick = onPositiveButtonClick;
            OnNegativeButtonClick = onNegativeButtonClick;
            PositiveButtonText = positiveButtonText;
            NegativeButtonText = negativeButtonText;

            PositiveCommand = new Command(async () => await OnPositiveButtonClick?.Invoke(this), () => !IsBusy);
            NegativeCommand = new Command(async () => await OnNegativeButtonClick?.Invoke(this), () => !IsBusy);

            SetMode(PersonItemViewModel.Mode.Invitation);
        }

        //view mode with balance
        public PersonItemViewModel(long id,
            PersonButtonClickDel onItemTapped)
        {

            UserId = id;
            Name = "user " + id;
            OnItemTapped = onItemTapped;

            TapCommand = new Command(async () => await OnItemTapped?.Invoke(this), () => !IsBusy);

            SetMode(Mode.ViewWithoutBalance);
        }

        public PersonItemViewModel(long id,
            PersonButtonClickDel onItemTapped,
            decimal amount)
        {

            UserId = id;
            Name = "user " + id;
            OnItemTapped = onItemTapped;

            TapCommand = new Command(async () => await OnItemTapped?.Invoke(this), () => !IsBusy);

            SetMode(Mode.ViewWithBalance);

            Amount.Amount = amount;
        }

        public PersonItemViewModel(long id,
            PersonButtonClickDel onPositiveButtonClick,
            string positiveButtonText)
        {

            UserId = id;
            Name = "user " + id;
            OnPositiveButtonClick = onPositiveButtonClick;
            PositiveButtonText = positiveButtonText;

            PositiveCommand = new Command(async () => await OnPositiveButtonClick?.Invoke(this), () => !IsBusy);

            SetMode(PersonItemViewModel.Mode.OnePosBtn);
        }

        public PersonItemViewModel(long id,
           PersonButtonClickDel onItemTapped,
           bool isChecked)
        {

            UserId = id;
            Name = "user " + id;
            OnItemTapped = onItemTapped;
            Check(isChecked);

            TapCommand = new Command(async () => 
            await OnItemTapped?.Invoke(this), () => !IsBusy);

            SetMode(PersonItemViewModel.Mode.Selector);
        }

        public void Check(bool check)
        {
            IsChecked = check;

            CheckStatusImageSource = IsChecked ? "+" : "-";
        }

        public void UpdateUserData(PF.DTO.Users.UserDTO userData)
        {
            Name = userData.Name + " " + userData.LastName;

            AvatarImage = AvatarSourceToImageConverter.Convert(userData.AvatarSrc);
        }

        private void SetMode(Mode mode)
        {
            VisibleBalance = false;
            VisiblePosButton = false;
            VisibleNegButton = false;
            VisibleSelector = false;

            switch (mode)
            {
                case Mode.Invitation:
                    VisiblePosButton = true;
                    VisibleNegButton = true;
                    break;

                case Mode.ViewWithBalance:
                    VisibleBalance = true;
                    break;

                case Mode.OnePosBtn:
                    VisiblePosButton = true;
                    break;

                case Mode.Selector:
                    VisibleSelector = true;
                    break;
            }

            if (VisibleBalance && Amount == null)
            {
                Amount = new AmountItemViewModel();
            }

            OnPropertyChanged("VisibleBalance");
            OnPropertyChanged("VisiblePosButton");
            OnPropertyChanged("VisibleNegButton");
            OnPropertyChanged("VisibleSelector");
        }
    }
}
