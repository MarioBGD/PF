using System.Threading.Tasks;
using System.Windows.Input;
using PF.App.Core.LibUI;

namespace PF.App.Core.ViewModels
{
    public class PersonComponentViewModel : BaseViewModel
    {
        public enum Mode
        { Invitation, ViewWithoutBalance, ViewWithBalance, OnePosBtn, Selector }

        private bool isCheckded;
        private string name;
        //private ImageSource avatarImage;
        private string positiveButtonText;
        private string negativeButtonText;
        private string checkStatusImageSource;


        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        //TODO:images
        // public ImageSource AvatarImage
        // {
        //     get => avatarImage;
        //     set => SetProperty(ref avatarImage, value);
        // }

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

        public AmountComponentViewModel Amount { get; set; }

        public bool VisibleBalance { get; set; }
        public bool VisiblePosButton { get; set; }
        public bool VisibleNegButton { get; set; }
        public bool VisibleSelector { get; set; }

        public long UserId { get; set; }


        public delegate Task PersonButtonClickDel(PersonComponentViewModel person);
        public PersonButtonClickDel OnItemTapped;
        public PersonButtonClickDel OnPositiveButtonClick;
        public PersonButtonClickDel OnNegativeButtonClick;

        public ICommand TapCommand { get; }
        public ICommand PositiveCommand { get; }
        public ICommand NegativeCommand { get; }
        //public PersonItemViewModel(PersonButtonClickDel positive, PersonButtonClickDel negative)
        //{
        //    //PositiveCommand = new Command(async () => await positive(this), () => !IsBusy);
        //    //NegativeCommand = new Command(async () => await negative(this), () => !IsBusy);
        //}


        public PersonComponentViewModel(long id,
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

            PositiveCommand = new SimpleCommand(() => OnPositiveButtonClick?.Invoke(this));
            NegativeCommand = new SimpleCommand(() => OnNegativeButtonClick?.Invoke(this));

            SetMode(PersonComponentViewModel.Mode.Invitation);
        }

        //view mode with balance
        public PersonComponentViewModel(long id,
            PersonButtonClickDel onItemTapped)
        {

            UserId = id;
            Name = "user " + id;
            OnItemTapped = onItemTapped;

            TapCommand = new SimpleCommand(() => OnItemTapped?.Invoke(this));

            SetMode(Mode.ViewWithoutBalance);
        }

        public PersonComponentViewModel(long id,
            PersonButtonClickDel onItemTapped,
            decimal amount)
        {

            UserId = id;
            Name = "user " + id;
            OnItemTapped = onItemTapped;

            TapCommand = new SimpleCommand(() => OnItemTapped?.Invoke(this));

            SetMode(Mode.ViewWithBalance);

            Amount.Amount = amount;
        }

        public PersonComponentViewModel(long id,
            PersonButtonClickDel onPositiveButtonClick,
            string positiveButtonText)
        {

            UserId = id;
            Name = "user " + id;
            OnPositiveButtonClick = onPositiveButtonClick;
            PositiveButtonText = positiveButtonText;

            PositiveCommand = new SimpleCommand(() => OnPositiveButtonClick?.Invoke(this));

            SetMode(PersonComponentViewModel.Mode.OnePosBtn);
        }

        public PersonComponentViewModel(long id,
           PersonButtonClickDel onItemTapped,
           bool isChecked)
        {

            UserId = id;
            Name = "user " + id;
            OnItemTapped = onItemTapped;
            Check(isChecked);

            TapCommand = new SimpleCommand(() => OnItemTapped?.Invoke(this));

            SetMode(PersonComponentViewModel.Mode.Selector);
        }

        public void Check(bool check)
        {
            IsChecked = check;

            CheckStatusImageSource = IsChecked ? "+" : "-";
        }

        public void UpdateUserData(PF.DTO.Users.UserDTO userData)
        {
            Name = userData.Name + " " + userData.LastName;

            //TODO:images
            //AvatarImage = AvatarSourceToImageConverter.Convert(userData.AvatarSrc);
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
                Amount = new AmountComponentViewModel();
            }

            OnPropertyChanged("VisibleBalance");
            OnPropertyChanged("VisiblePosButton");
            OnPropertyChanged("VisibleNegButton");
            OnPropertyChanged("VisibleSelector");
        }
    }
}