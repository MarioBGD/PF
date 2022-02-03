using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PF.App.XamForms.Views.Components
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AmountComponentView
    {
        private static decimal _amount;
        private static string _currency;
        
        public static readonly BindableProperty AmountProperty = BindableProperty.Create(
            nameof(Amount),
            typeof(decimal),
            typeof(AmountComponentView),
            default(decimal),
            propertyChanged:OnAmountPropertyChanged);
        
        public static readonly BindableProperty CurrencyProperty = BindableProperty.Create(
            nameof(Currency),
            typeof(string),
            typeof(AmountComponentView),
            string.Empty,
            propertyChanged:OnCurrencyPropertyChanged);

        public decimal Amount
        {
            get => (decimal) GetValue(AmountProperty);
            set => SetValue(AmountProperty, value);
        }
        
        public string Currency
        {
            get => (string) GetValue(CurrencyProperty);
            set => SetValue(CurrencyProperty, value);
        }
        
        public AmountComponentView()
        {
            InitializeComponent();
        }
        
        private static void OnAmountPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            if (bindable is AmountComponentView element && newvalue is decimal newNumber)
            {
                _amount = newNumber;
                UpdateAmount(element);
            }
        }
        
        private static void OnCurrencyPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            if (bindable is AmountComponentView element && newvalue is string newCurrency)
            {
                _currency = newCurrency;
                UpdateAmount(element);
            }
        }

        private static void UpdateAmount(AmountComponentView element)
        {
            Color amountColor;

            if (_amount > 0)
            {
                amountColor = Color.Green;
            }
            else if (_amount < 0)
            {
                amountColor = Color.OrangeRed;
            }
            else
            {
                amountColor = Color.Gray;
            }
            
            element.AmountLabel.Text = $"{_amount:F} {_currency}";
            element.AmountLabel.TextColor = amountColor;
        }
    }
}