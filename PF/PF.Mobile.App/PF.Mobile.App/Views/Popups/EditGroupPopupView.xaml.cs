using PF.DTO.Groups;
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
    public partial class EditGroupPopupView : Rg.Plugins.Popup.Pages.PopupPage
    {
        public EditGroupPopupViewModel ViewModel;

        public EditGroupPopupView(GroupDTO groupDTO)
        {
            CloseWhenBackgroundIsClicked = false;
            
            InitializeComponent();
            ViewModel = new EditGroupPopupViewModel(groupDTO);
            BindingContext = ViewModel;
        }
    }
}