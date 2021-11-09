using PF.DTO.Expenses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PF.Mobile.App.ViewModels.Items
{
    public class ExpenseItemViewModel : BaseViewModel
    {
        private string name;
        private string amount;
        private bool visibleUserAmount;

        private DateTime createdDate;
        private string createdUser;
        private string debtors;
        private string payers;
        private bool showHeader;
        private string headerText;

        public ExpenseDTO Expense { get; set; }

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

        public bool VisibleUserAmount
        {
            get => visibleUserAmount;
            set => SetProperty(ref visibleUserAmount, value);
        }


        public DateTime CreatedDate
        {
            get => createdDate;
            set => SetProperty(ref createdDate, value);
        }

        public string CreatedUser
        {
            get => createdUser;
            set => SetProperty(ref createdUser, value);
        }

        public string Debtors
        {
            get => debtors;
            set => SetProperty(ref debtors, value);
        }

        public string Payers
        {
            get => payers;
            set => SetProperty(ref payers, value);
        }

        public bool ShowHeader
        {
            get => showHeader;
            set => SetProperty(ref showHeader, value);
        }

        public string HeaderText
        {
            get => headerText;
            set => SetProperty(ref headerText, value);
        }

        public AmountItemViewModel UserAmount { get; set; }

        public ExpenseItemViewModel(ExpenseDTO expense)
        {
            Expense = expense;
            UserAmount = new AmountItemViewModel();
            UserAmount.VisibleText = true;

            Name = Expense.Name;
            Amount = expense.Payments.Where(p => p.OrginalAmount > 0).Sum(p => p.OrginalAmount).ToString("0.00")
                + ' '  + ((Common.Currencies.Currencies.CurrencyCodes)expense.Payments[0].OrginalCurrencyId).ToString(); //TODO: przeniesc do AmountItemView
            UserAmount.Amount = expense.Payments.Where(p => p.UserId == DAL.SessionManager.UserId).Sum(p => p.OrginalAmount);
            VisibleUserAmount = UserAmount.Amount != 0;
            UserAmount.Currency = (Common.Currencies.Currencies.CurrencyCodes)expense.Payments[0].OrginalCurrencyId;

            CreatedDate = expense.CreatedDate;
            Payers = string.Join(Environment.NewLine, expense.Payments.Where(p => p.Amount > 0).Select(p => p.UserId));
            Debtors = string.Join(Environment.NewLine, expense.Payments.Where(p => p.Amount < 0).Select(p => p.UserId));
        }
    }
}
