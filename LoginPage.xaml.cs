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

        // Validaci�n del usuario (permite letras y n�meros, entre 3 y 12 caracteres)
        private void OnUsernameTextChanged(object sender, TextChangedEventArgs e)
        {
            string username = UsernameEntry.Text ?? "";

            // Permitir letras, n�meros, puntos, guiones y guiones bajos
            username = Regex.Replace(username, "[^a-zA-Z0-9._,@!*+-]", "");

            // Limitar a 12 caracteres
            if (username.Length > 12)
            {
                username = username.Substring(0, 12);
            }

            // Asignar el texto filtrado nuevamente al Entry
            UsernameEntry.TextChanged -= OnUsernameTextChanged; // Evitar bucles de actualizaci�n
            UsernameEntry.Text = username;
            UsernameEntry.TextChanged += OnUsernameTextChanged;

            // Validaci�n de longitud
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


        // Validaci�n de la contrase�a
        private void OnPasswordTextChanged(object sender, TextChangedEventArgs e)
        {
            string password = PasswordEntry.Text ?? "";

            // Validaci�n de la contrase�a
            if (string.IsNullOrWhiteSpace(password))
            {
                PasswordErrorLabel.Text = "La contrase�a debe tener al menos 8 caracteres.\nUna may�scula.\nUna min�scula.\nUn n�mero y un caracter.";
                PasswordErrorLabel.IsVisible = true;
                LoginButton.IsEnabled = false;
            }
            else if (!IsValidPassword(password))
            {
                PasswordErrorLabel.Text = "La contrase�a debe tener al menos 8 caracteres.\nUna may�scula.\nUna min�scula.\nUn n�mero y un caracter.";
                PasswordErrorLabel.IsVisible = true;
                LoginButton.IsEnabled = false;
            }
            else
            {
                PasswordErrorLabel.IsVisible = false;
                LoginButton.IsEnabled = UsernameEntry.Text.Length >= 3 && UsernameEntry.Text.Length <= 12;
            }
        }

        // Expresi�n regular para validar la contrase�a (incluyendo un car�cter especial)
        private bool IsValidPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password)) return false;

            // Expresi�n regular actualizada para incluir al menos un car�cter especial
            var regex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%_\-*.?&])[A-Za-z\d@$!%_\-*.?&]{8,}$");
            return regex.IsMatch(password);
        }

        // M�todo de inicio de sesi�n (Redirige a la p�gina del dashboard)
        private async void OnLoginClicked(object sender, EventArgs e)
        {
            string userNumber = UsernameEntry.Text ?? "";

            if (userNumber.Length < 3 || userNumber.Length > 12)
            {
                await DisplayAlert("Error", "El usuario debe tener entre 3 y 12 caracteres.", "OK");
                return;
            }

            // Redirigir a la pantalla del dashboard y pasar el nombre de usuario
            await Shell.Current.GoToAsync($"//UserDashboardPage?username=Ruben Gonz�lez&userNumber={userNumber}");
        }
    }
}
