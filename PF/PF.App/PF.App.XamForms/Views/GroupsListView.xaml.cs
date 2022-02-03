using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PF.App.Core.ViewModels;
using Splat;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PF.App.XamForms.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GroupsListView
    {
        public GroupsListView()
        {
            BindingContext = Locator.Current.GetService<GroupsListViewModel>();
            InitializeComponent();
            
            GroupsList.ItemTapped += (object sender, ItemTappedEventArgs e) =>
            {
                GroupsList.SelectedItem = null;
            };
        }
    }
}