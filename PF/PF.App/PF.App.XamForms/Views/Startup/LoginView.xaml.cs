using PF.App.Core.ViewModels.Startup;
using Xamarin.Forms.Xaml;

namespace PF.App.XamForms.Views.Startup
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginView
    {
        public LoginView(LoginViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = viewModel;
        }
    }
}