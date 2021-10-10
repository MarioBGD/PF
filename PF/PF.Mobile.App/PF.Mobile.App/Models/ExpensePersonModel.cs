using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PF.Mobile.App.Models
{
    public class ExpensePersonModel : ViewModels.BaseViewModel
    {
        public delegate Task ExpensePersonButtonClickDel(ExpensePersonModel person);
        private ExpensePersonButtonClickDel OnRemoveButtonClick;

        private string amount;
        private string name;

        public long UserId { get; set; }
        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        public string Amount
        {
            get => amount;
            set => SetProperty(ref amount, value);
        }

        public Command RemoveCommand { get; }

        public ExpensePersonModel(long userId, ExpensePersonButtonClickDel onRemoveButtonClick)
        {
            UserId = userId;
            OnRemoveButtonClick = onRemoveButtonClick;

            Name = "USER" + userId;

            RemoveCommand = new Command(async () => await OnRemoveButtonClick(this), () => !IsBusy);
        }
    }
}
