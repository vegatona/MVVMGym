using System.Text.RegularExpressions;
namespace Mockup
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            BindingContext = this;
        }

        public Command GoToRegister => new Command(async () => await Navigation.PushAsync(new RegisterPage()));
        public Command GoToRecoverPassword => new Command(async () => await Navigation.PushAsync(new RecoverPasswordPage()));

        // Validación de los 6 dígitos del usuario
        private void OnUsernameTextChanged(object sender, TextChangedEventArgs e)
        {
            string username = UsernameEntry.Text ?? "";

            if (username.Length > 6)
            {
                UsernameEntry.Text = username.Substring(0, 6);
            }

            if (username.Length == 6)
            {
                ErrorLabel.IsVisible = false;
                LoginButton.IsEnabled = !string.IsNullOrWhiteSpace(PasswordEntry.Text) && IsValidPassword(PasswordEntry.Text);
            }
            else
            {
                ErrorLabel.Text = "El usuario debe tener 6 dígitos.";
                ErrorLabel.IsVisible = true;
                LoginButton.IsEnabled = false;
            }
        }

        // Validación de la contraseña
        private void OnPasswordTextChanged(object sender, TextChangedEventArgs e)
        {
            string password = PasswordEntry.Text ?? "";

            if (!IsValidPassword(password))
            {
                PasswordErrorLabel.Text = "La contraseña debe tener al menos 8 caracteres.\nUna mayúscula.\nUna minúscula.\nUn número y un carácter especial.";
                PasswordErrorLabel.IsVisible = true;
                LoginButton.IsEnabled = false;
            }
            else if (UsernameEntry.Text?.Length != 6)
            {
                PasswordErrorLabel.Text = "El usuario debe tener 6 dígitos.";
                PasswordErrorLabel.IsVisible = true;
                LoginButton.IsEnabled = false;
            }
            else
            {
                PasswordErrorLabel.IsVisible = false;
                LoginButton.IsEnabled = true;
            }
        }

        // Expresión regular para validar la contraseña
        private bool IsValidPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password)) return false;

            var regex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%-_.*?&])[A-Za-z\d@$!%-_*.?&]{8,}$");
            return regex.IsMatch(password);
        }

        // Método de inicio de sesión (Redirige a la página del dashboard)
        private async void OnLoginClicked(object sender, EventArgs e)
        {
            string userNumber = UsernameEntry.Text ?? "";

            if (userNumber.Length != 6)
            {
                await DisplayAlert("Error", "El usuario debe tener 6 dígitos.", "OK");
                return;
            }

            // Redirigir a la pantalla del dashboard y pasar el número de usuario
            await Shell.Current.GoToAsync($"//UserDashboardPage?username=Rubén González&userNumber={userNumber}");
        }
    }
}
