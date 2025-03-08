using System.Text.RegularExpressions;

namespace Mockup
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            BindingContext = this;

            // Agregar eventos TextChanged
            UsernameEntry.TextChanged += OnUsernameTextChanged;
            PasswordEntry.TextChanged += OnPasswordTextChanged;
        }

        public Command GoToRegister => new Command(async () => await Navigation.PushAsync(new RegisterPage()));
        public Command GoToRecoverPassword => new Command(async () => await Navigation.PushAsync(new RecoverPasswordPage()));

        protected override void OnNavigatedFrom(NavigatedFromEventArgs args)
        {
            base.OnNavigatedFrom(args);

            UsernameEntry.Text = string.Empty;
            PasswordEntry.Text = string.Empty;
            ErrorLabel.IsVisible = false;
            PasswordErrorLabel.IsVisible = false;
            LoginButton.IsEnabled = false;
        }

        private void OnUsernameTextChanged(object sender, TextChangedEventArgs e)
        {
            string username = UsernameEntry.Text ?? "";
            username = Regex.Replace(username, "[^a-zA-Z0-9._,@!*+-]", "");

            if (username.Length > 12)
            {
                username = username.Substring(0, 12);
            }

            UsernameEntry.TextChanged -= OnUsernameTextChanged;
            UsernameEntry.Text = username;
            UsernameEntry.TextChanged += OnUsernameTextChanged;

            LoginButton.IsEnabled = username.Length >= 3 && username.Length <= 12 && IsValidPassword(PasswordEntry.Text);
        }

        private void OnPasswordTextChanged(object sender, TextChangedEventArgs e)
        {
            string password = PasswordEntry.Text ?? "";
            LoginButton.IsEnabled = IsValidPassword(password) && UsernameEntry.Text.Length >= 3 && UsernameEntry.Text.Length <= 12;
        }

        private bool IsValidPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password)) return false;

            var regex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%_\-*.?&])[A-Za-z\d@$!%_\-*.?&]{8,}$");
            return regex.IsMatch(password);
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            string userNumber = UsernameEntry.Text ?? "";

            if (userNumber.Length < 3 || userNumber.Length > 12)
            {
                await DisplayAlert("Error", "El usuario debe tener entre 3 y 12 caracteres.", "OK");
                return;
            }

            await Shell.Current.GoToAsync($"//UserDashboardPage?username=Ruben González&userNumber={userNumber}");
        }
    }
}
