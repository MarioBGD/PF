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
    public partial class InviteFriendPopupView : Rg.Plugins.Popup.Pages.PopupPage
    {
        public InviteFriendsPopupViewModel ViewModel;
        public InviteFriendPopupView(long groupId, IEnumerable<MembershipDTO> memberships)
        {

            InitializeComponent();

            ViewModel = new InviteFriendsPopupViewModel(groupId, memberships);
            BindingContext = ViewModel;
        }
    }
}