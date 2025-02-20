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

        // Validación del usuario (permite letras y números, entre 3 y 12 caracteres)
        private void OnUsernameTextChanged(object sender, TextChangedEventArgs e)
        {
            string username = UsernameEntry.Text ?? "";

            // Permitir letras, números, puntos, guiones y guiones bajos
            username = Regex.Replace(username, "[^a-zA-Z0-9._,@!*+-]", "");

            // Limitar a 12 caracteres
            if (username.Length > 12)
            {
                username = username.Substring(0, 12);
            }

            // Asignar el texto filtrado nuevamente al Entry
            UsernameEntry.TextChanged -= OnUsernameTextChanged; // Evitar bucles de actualización
            UsernameEntry.Text = username;
            UsernameEntry.TextChanged += OnUsernameTextChanged;

            // Validación de longitud
            if (username.Length < 3 || username.Length > 12)
            {
                ErrorLabel.Text = "El usuario debe tener entre 3 y 12 caracteres.";
                ErrorLabel.IsVisible = true;
                LoginButton.IsEnabled = false;
            }
            else
            {
                ErrorLabel.IsVisible = false;
                LoginButton.IsEnabled = !string.IsNullOrWhiteSpace(PasswordEntry.Text) && IsValidPassword(PasswordEntry.Text);
            }
        }


        // Validación de la contraseña
        private void OnPasswordTextChanged(object sender, TextChangedEventArgs e)
        {
            string password = PasswordEntry.Text ?? "";

            // Validación de la contraseña
            if (string.IsNullOrWhiteSpace(password))
            {
                PasswordErrorLabel.Text = "La contraseña debe tener al menos 8 caracteres.\nUna mayúscula.\nUna minúscula.\nUn número y un caracter.";
                PasswordErrorLabel.IsVisible = true;
                LoginButton.IsEnabled = false;
            }
            else if (!IsValidPassword(password))
            {
                PasswordErrorLabel.Text = "La contraseña debe tener al menos 8 caracteres.\nUna mayúscula.\nUna minúscula.\nUn número y un caracter.";
                PasswordErrorLabel.IsVisible = true;
                LoginButton.IsEnabled = false;
            }
            else
            {
                PasswordErrorLabel.IsVisible = false;
                LoginButton.IsEnabled = UsernameEntry.Text.Length >= 3 && UsernameEntry.Text.Length <= 12;
            }
        }

        // Expresión regular para validar la contraseña (incluyendo un carácter especial)
        private bool IsValidPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password)) return false;

            // Expresión regular actualizada para incluir al menos un carácter especial
            var regex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%_\-*.?&])[A-Za-z\d@$!%_\-*.?&]{8,}$");
            return regex.IsMatch(password);
        }

        // Método de inicio de sesión (Redirige a la página del dashboard)
        private async void OnLoginClicked(object sender, EventArgs e)
        {
            string userNumber = UsernameEntry.Text ?? "";

            if (userNumber.Length < 3 || userNumber.Length > 12)
            {
                await DisplayAlert("Error", "El usuario debe tener entre 3 y 12 caracteres.", "OK");
                return;
            }

            // Redirigir a la pantalla del dashboard y pasar el nombre de usuario
            await Shell.Current.GoToAsync($"//UserDashboardPage?username=Ruben González&userNumber={userNumber}");
        }
    }
}
