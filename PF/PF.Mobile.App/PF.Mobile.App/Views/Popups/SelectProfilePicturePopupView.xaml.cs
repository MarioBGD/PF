using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PF.Mobile.App.ViewModels.Popups;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PF.Mobile.App.Views.Popups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SelectProfilePicturePopupView : Rg.Plugins.Popup.Pages.PopupPage
    {
        public SelectProfilePicturePopupView()
        {
            InitializeComponent();
            BindingContext = new SelectProfilePicturePopupViewModel();
        }
    }
}