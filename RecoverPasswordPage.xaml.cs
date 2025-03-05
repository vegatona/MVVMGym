using System.Text.RegularExpressions;

namespace Mockup;

public partial class RecoverPasswordPage : ContentPage
{
    public RecoverPasswordPage()
    {
        InitializeComponent();
        BindingContext = this;

        ContinueButton.IsEnabled = false;
        UserEntry.TextChanged += OnUserEntryTextChanged;
    }

    private void OnUserEntryTextChanged(object sender, TextChangedEventArgs e)
    {
        string username = UserEntry.Text ?? "";
        username = Regex.Replace(username, "[^a-zA-Z0-9._,@!*+-]", "");

        if (username.Length > 12)
        {
            username = username.Substring(0, 12);
        }

        if (UserEntry.Text != username)
        {
            UserEntry.Text = username;
        }

        ContinueButton.IsEnabled = username.Length >= 3 && username.Length <= 12;
    }

    private async void OnContinueClicked(object sender, EventArgs e)
    {
        string username = UserEntry.Text ?? "";

        if (string.IsNullOrWhiteSpace(username)) return;

        await Shell.Current.GoToAsync("//EnterCodePage");
    }

    public Command GoToRegister => new Command(async () =>
    {
        await Shell.Current.GoToAsync("//RegisterPage");
    });
}
