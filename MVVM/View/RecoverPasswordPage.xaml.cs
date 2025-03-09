using System.Text.RegularExpressions;

namespace Mockup;

public partial class RecoverPasswordPage : ContentPage
{
    public RecoverPasswordPage()
    {
        InitializeComponent();
        BindingContext = new MVVM.ViewModel.RecoverPasswordViewModel();
    }
}
