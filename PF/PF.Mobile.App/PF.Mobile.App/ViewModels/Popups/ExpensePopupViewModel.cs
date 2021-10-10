using PF.Common.Currencies;
using PF.DTO.Expenses;
using PF.Mobile.App.DAL.Api;
using PF.Mobile.App.Models;
using PF.Mobile.App.ViewModels.Items;
using PF.Mobile.App.Views;
using PF.Mobile.App.Views.Items;
using PF.Mobile.App.Views.Popups;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PF.Mobile.App.ViewModels.Popups
{
    public class ExpensePopupViewModel : BaseViewModel
    {
        private ExpenseDTO expense;

        private string mainAmount;
        private string name;
        private bool swapButtonVisible;
        private string currencyText;
        public Currencies.CurrencyCodes currency;

        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        public string MainAmount
        {
            get => mainAmount;
            set => SetProperty(ref mainAmount, value);
        }

        public bool SwapButtonVisible
        {
            get => swapButtonVisible;
            set => SetProperty(ref swapButtonVisible, value);
        }

        public string CurrencyText
        {
            get => currencyText;
            set => SetProperty(ref currencyText, value);
        }

        public Currencies.CurrencyCodes Currency
        {
            get => currency;
            set
            {
                currency = value;
                CurrencyText = value.ToString();
            }
        }

        public int CurrencyId => (int)Currency;

        public ExpensePeopleItemViewModel PayersViewModel { get; set; }
        public ExpensePeopleItemViewModel DebtorsViewModel { get; set; }

        public Command OkCommand { get; set; }
        public Command CancelCommand { get; set; }
        public Command SwapCommand { get; set; }
        public Command OnValueEnteredCommand { get; set; }
        public Command ChangeCurrencyCommand { get; }

        public ExpensePopupViewModel(ExpenseDTO expense = null)
        {
            PayersViewModel = new ExpensePeopleItemViewModel(OnChanged);
            DebtorsViewModel = new ExpensePeopleItemViewModel(OnChanged);

            OkCommand = new Command(async () => await OkClick(), () => !IsBusy);
            CancelCommand = new Command(async () => await CancelClick(), () => !IsBusy);
            SwapCommand = new Command(OnSwapClick, () => !IsBusy);
            OnValueEnteredCommand = new Command(async () => await OnValueEntered(), () => !IsBusy);
            ChangeCurrencyCommand = new Command(async () => await OnChangeCurrencyClick(), () => !IsBusy);

            Currency = Currencies.CurrencyCodes.PLN; //TODO: C

            this.expense = expense;
            if (expense == null)
            {
                expense = new ExpenseDTO();
            }
            else
            {
                SetDataBasingOnExpenseDTO(expense, true);
            }
        }

        public async Task OkClick()
        {
            IsBusy = true;

            OnChanged();

            if (expense == null || expense.Payments == null || expense.Payments.Count == 0)
            {
                return;
            }

            var userDialogs = Acr.UserDialogs.UserDialogs.Instance;

            if (string.IsNullOrWhiteSpace(expense.Name))
            {
                userDialogs.Toast("Wprowadź nazwę");
                return;
            }

            if (expense.Payments.Where(p => p.Amount == 0).Any())
            {
                userDialogs.Toast("Niepoprawne rozłożenie kwot");
                return;
            }

            var result = await ApiClient.AddExpense(expense);

            if (result != null && result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                OnPoped(true);
                await Rg.Plugins.Popup.Services.PopupNavigation.Instance.PopAsync();
            }
            else
            {
                userDialogs.Toast("Wystapił problem");
            }

            IsBusy = false;
        }

        public async Task CancelClick()
        {
            IsBusy = true;
            await Rg.Plugins.Popup.Services.PopupNavigation.Instance.PopAsync();
        }

        private void OnSwapClick()
        {
            //TODO: obsluzyc kwoty
            if (PayersViewModel.People.Count() == 1 && DebtorsViewModel.People.Count() == 1)
            {
                var p = PayersViewModel.People[0];
                PayersViewModel.People[0] = DebtorsViewModel.People[0];
                DebtorsViewModel.People[0] = p;

                OnChanged();
            }
        }

        public async Task OnValueEntered()
        {
            MainAmount = AmountParser.ParseToText(MainAmount);
            OnChanged();
        }

        public void OnChanged()
        {
            var payers = PayersViewModel.People.ToList();
            var debtors = DebtorsViewModel.People.ToList();

            SwapButtonVisible = payers.Count == 1 && debtors.Count == 1;

            if (payers.Count != 0 && debtors.Count != 0 && !string.IsNullOrEmpty(MainAmount))
            {
                var ci = (CultureInfo)CultureInfo.CurrentCulture.Clone();
                ci.NumberFormat.NumberDecimalSeparator = ",";
                decimal amount = decimal.Parse(MainAmount.Replace('.', ','), ci);
                decimal amountPerDebtor = amount / debtors.Count * -1;
                decimal amountPerPayer = amount / payers.Count;
                decimal worthOfCurrency = App.CurrentGroup.DefaultCurrencyId == CurrencyId ? 1 : App.CurrentGroup.Currencies.FirstOrDefault(p => p.CurrencyId == CurrencyId).Worth;

                List<PaymentDTO> payments = new List<PaymentDTO>();

                foreach (var debtor in debtors)
                {
                    payments.Add(new PaymentDTO
                    {
                        UserId = debtor.UserId,
                        OrginalAmount = amountPerDebtor,
                        OrginalCurrencyId = CurrencyId,
                        Amount = amountPerDebtor * worthOfCurrency
                    });

                    debtor.Amount = amountPerDebtor.ToString("0.00");
                }

                foreach (var payer in payers)
                {
                    payments.Add(new PaymentDTO
                    {
                        UserId = payer.UserId,
                        OrginalAmount = amountPerPayer,
                        OrginalCurrencyId = CurrencyId,
                        Amount = amountPerPayer * worthOfCurrency
                    });

                    payer.Amount = amountPerPayer.ToString("0.00");
                }

                expense = new ExpenseDTO()
                {
                    GroupId = App.CurrentGroupId,
                    Name = Name,
                    Payments = payments
                };
            }
        }

        private async Task OnChangeCurrencyClick()
        {
            IsBusy = true;

            List<string> choices = new List<string>();
            choices.Add(((Currencies.CurrencyCodes)App.CurrentGroup.DefaultCurrencyId).ToString());
            foreach (var currencyId in App.CurrentGroup.Currencies.Select(p => p.CurrencyId))
            {
                choices.Add(((Currencies.CurrencyCodes)currencyId).ToString());
            }

            var choice = await Acr.UserDialogs.UserDialogs.Instance.ActionSheetAsync(
                null, "Cancel", null, System.Threading.CancellationToken.None, choices.ToArray());

            object selectedCurrencyObj;
            if (!string.IsNullOrEmpty(choice) && Enum.TryParse(typeof(Currencies.CurrencyCodes), choice, out selectedCurrencyObj))
            {
                Currency = (Currencies.CurrencyCodes)selectedCurrencyObj;
            }

            IsBusy = false;
        }

        private void SetDataBasingOnExpenseDTO(ExpenseDTO expense, bool force = false)
        {
            if (expense == null)
            {
                return;
            }

            Device.InvokeOnMainThreadAsync(() =>
            {

                if (expense.Payments?.Count > 0)
                {
                    PayersViewModel.People.Clear();
                    DebtorsViewModel.People.Clear();

                    var payers = expense.Payments.Where(p => p.Amount > 0);
                    var debtors = expense.Payments.Where(p => p.Amount < 0);

                    foreach (var payer in payers)
                    {
                        PayersViewModel.AddPerson(payer.UserId);
                    }

                    foreach (var debtor in debtors)
                    {
                        DebtorsViewModel.AddPerson(debtor.UserId);
                    }
                }

                OnChanged();
            });
        }

        public async Task OnModeSelect(ComboBoxItemModel item)
        {
            await Rg.Plugins.Popup.Services.PopupNavigation.Instance.PopAsync();
        }

    }
}
