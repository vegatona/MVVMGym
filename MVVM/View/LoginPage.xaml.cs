using Mockup.MVVM.ViewModel;
using Microsoft.Maui.Controls;

namespace Mockup
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            BindingContext = new LoginViewModel();
        }
    }
}
