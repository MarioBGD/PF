using PF.Common.Currencies;

namespace PF.App.Core.ViewModels
{
    public class AmountComponentViewModel : BaseViewModel
    {
        private string amountFormatted;
        private decimal amount;
        private string amountColor;
        private bool visibleText;
        private string text;
        private Currencies.CurrencyCodes currency;

        public string AmountFormatted
        {
            get => amountFormatted;
            set => SetProperty(ref amountFormatted, value);
        }

        public Currencies.CurrencyCodes Currency
        {
            get => currency;
            set
            {
                currency = value;
                Amount = amount; //refreshing amount
            }
        }

        public decimal Amount
        {
            get => amount;
            set
            {
                amount = value;
                AmountFormatted = amount.ToString("0.00") + ' ' + Currency.ToString();

                if (amount > 0)
                {
                    AmountColor = "Green";
                    Text = "You lent";
                }
                else if (amount < 0)
                {
                    AmountColor = "OrangeRed";
                    Text = "You borrowed";
                }
                else
                {
                    AmountColor = "Gray";
                    Text = "Not involved";
                }
            }
        }

        public string AmountColor
        {
            get => amountColor;
            set => SetProperty(ref amountColor, value);
        }

        public bool VisibleText
        {
            get => visibleText;
            set => SetProperty(ref visibleText, value);
        }

        public string Text
        {
            get => text;
            set => SetProperty(ref text, value);
        }

        public AmountComponentViewModel()
        {
            amountFormatted = "88.88 PLN";
        }
    }
}