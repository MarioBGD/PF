using PF.Mobile.App.Models;
using PF.Mobile.App.ViewModels.Popups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PF.Mobile.App.Views.Popups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ComboBoxPopupView : Rg.Plugins.Popup.Pages.PopupPage
    {
        public ComboBoxPopupViewModel ViewModel;
        public ComboBoxPopupView(IEnumerable<ComboBoxItemModel> items, double yPos)
        {
            InitializeComponent();

            StackLayout layout = (StackLayout)FindByName("MainLayout");
            layout.Margin = new Thickness(10, yPos, 10, 10);

            ViewModel = new ComboBoxPopupViewModel(items);
            BindingContext = ViewModel;

        }
    }
}