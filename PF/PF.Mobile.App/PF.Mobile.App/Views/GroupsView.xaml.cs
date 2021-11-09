using PF.Mobile.App.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PF.Mobile.App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GroupsView : ContentPage
    {
        public static View VVV;

        public GroupsView()
        {
            InitializeComponent();
            BindingContext = new GroupsViewModel();
            VVV = (View)FindByName("cb");
            GroupsListView.ItemTapped += (object sender, ItemTappedEventArgs e) =>
            {
                GroupsListView.SelectedItem = null;
            };
        }
    }
}