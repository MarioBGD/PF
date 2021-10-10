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
    public partial class PeopleSelectPopupView : Rg.Plugins.Popup.Pages.PopupPage
    {
        public PeopleSelectPopupView(IEnumerable<long> peopleIds, IEnumerable<long> selected, PeopleSelectPopupViewModel.OnSelectorCloseDel onSelectorClose, bool multiChoice = true)
        {
            InitializeComponent();
            BindingContext = new PeopleSelectPopupViewModel(peopleIds, selected, onSelectorClose, multiChoice);
        }
    }
}