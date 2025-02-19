using System.Text.RegularExpressions;

namespace Mockup;

public partial class RegisterPage : ContentPage
{
    public RegisterPage()
    {
        InitializeComponent();
        BindingContext = this;
    }

    // Validaci�n del usuario (permite letras y n�meros, entre 3 y 12 caracteres)
    private void OnUsernameTextChanged(object sender, TextChangedEventArgs e)
    {
        string username = UsernameEntry.Text ?? "";

        // Permitir letras, n�meros y algunos caracteres especiales
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
            UsernameErrorLabel.Text = "El usuario debe tener entre 3 y 12 caracteres.";
            UsernameErrorLabel.IsVisible = true;
            RegisterButton.IsEnabled = false;
        }
        else
        {
            UsernameErrorLabel.IsVisible = false;
            // Solo habilitar el bot�n de registro si las contrase�as son correctas
            RegisterButton.IsEnabled = IsValidPassword(PasswordEntry.Text) && PasswordEntry.Text == ConfirmPasswordEntry.Text;
        }
    }

    // Validaci�n de la contrase�a y la confirmaci�n de la contrase�a
    private void OnPasswordTextChanged(object sender, TextChangedEventArgs e)
    {
        string password = PasswordEntry.Text ?? "";
        string confirmPassword = ConfirmPasswordEntry.Text ?? "";

        if (!IsValidPassword(password))
        {
            PasswordErrorLabel.Text = "La contrase�a debe tener al menos 8 caracteres.\nUna may�scula.\nUna min�scula.\nUn n�mero y un car�cter especial.";
            PasswordErrorLabel.IsVisible = true;
            RegisterButton.IsEnabled = false;
        }
        else
        {
            PasswordErrorLabel.IsVisible = false;

            if (password != confirmPassword)
            {
                ConfirmPasswordErrorLabel.Text = "Las contrase�as no coinciden.";
                ConfirmPasswordErrorLabel.IsVisible = true;
                RegisterButton.IsEnabled = false;
            }
            else
            {
                ConfirmPasswordErrorLabel.IsVisible = false;
                // Habilitar el bot�n de registro si todo es v�lido
                RegisterButton.IsEnabled = true;
            }
        }
    }

    // M�todo para manejar el clic en "Registrarse"
    private async void OnRegisterClicked(object sender, EventArgs e)
    {
        string username = UsernameEntry.Text ?? "";
        string password = PasswordEntry.Text ?? "";
        string confirmPassword = ConfirmPasswordEntry.Text ?? "";

        // Validaciones antes de proceder
        if (string.IsNullOrWhiteSpace(username) || username.Length < 3 || username.Length > 12)
        {
            await DisplayAlert("Error", "El usuario debe tener entre 3 y 12 caracteres.", "OK");
            return;
        }

        if (!IsValidPassword(password))
        {
            await DisplayAlert("Error", "La contrase�a debe cumplir con los requisitos.", "OK");
            return;
        }

        if (password != confirmPassword)
        {
            await DisplayAlert("Error", "Las contrase�as no coinciden.", "OK");
            return;
        }

        // Mostrar mensaje de �xito antes de redirigir al LoginPage
        await DisplayAlert("�xito", "Tu registro ha sido exitoso.", "Iniciar sesi�n");

        // Navegar a LoginPage
        await Shell.Current.GoToAsync("//LoginPage");
    }

    // Expresi�n regular para validar la contrase�a
    private bool IsValidPassword(string password)
    {
        if (string.IsNullOrWhiteSpace(password)) return false;

        var regex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%-_*?&])[A-Za-z\d@$!%-_*?&]{8,}$");
        return regex.IsMatch(password);
    }
}
