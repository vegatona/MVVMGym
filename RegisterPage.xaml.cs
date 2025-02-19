using System.Text.RegularExpressions;

namespace Mockup;

public partial class RegisterPage : ContentPage
{
    public RegisterPage()
    {
        InitializeComponent();
        BindingContext = this;
    }

    // Validación del usuario (permite letras y números, entre 3 y 12 caracteres)
    private void OnUsernameTextChanged(object sender, TextChangedEventArgs e)
    {
        string username = UsernameEntry.Text ?? "";

        // Permitir letras, números y algunos caracteres especiales
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
            UsernameErrorLabel.Text = "El usuario debe tener entre 3 y 12 caracteres.";
            UsernameErrorLabel.IsVisible = true;
            RegisterButton.IsEnabled = false;
        }
        else
        {
            UsernameErrorLabel.IsVisible = false;
            // Solo habilitar el botón de registro si las contraseñas son correctas
            RegisterButton.IsEnabled = IsValidPassword(PasswordEntry.Text) && PasswordEntry.Text == ConfirmPasswordEntry.Text;
        }
    }

    // Validación de la contraseña y la confirmación de la contraseña
    private void OnPasswordTextChanged(object sender, TextChangedEventArgs e)
    {
        string password = PasswordEntry.Text ?? "";
        string confirmPassword = ConfirmPasswordEntry.Text ?? "";

        if (!IsValidPassword(password))
        {
            PasswordErrorLabel.Text = "La contraseña debe tener al menos 8 caracteres.\nUna mayúscula.\nUna minúscula.\nUn número y un carácter especial.";
            PasswordErrorLabel.IsVisible = true;
            RegisterButton.IsEnabled = false;
        }
        else
        {
            PasswordErrorLabel.IsVisible = false;

            if (password != confirmPassword)
            {
                ConfirmPasswordErrorLabel.Text = "Las contraseñas no coinciden.";
                ConfirmPasswordErrorLabel.IsVisible = true;
                RegisterButton.IsEnabled = false;
            }
            else
            {
                ConfirmPasswordErrorLabel.IsVisible = false;
                // Habilitar el botón de registro si todo es válido
                RegisterButton.IsEnabled = true;
            }
        }
    }

    // Método para manejar el clic en "Registrarse"
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
            await DisplayAlert("Error", "La contraseña debe cumplir con los requisitos.", "OK");
            return;
        }

        if (password != confirmPassword)
        {
            await DisplayAlert("Error", "Las contraseñas no coinciden.", "OK");
            return;
        }

        // Mostrar mensaje de éxito antes de redirigir al LoginPage
        await DisplayAlert("Éxito", "Tu registro ha sido exitoso.", "Iniciar sesión");

        // Navegar a LoginPage
        await Shell.Current.GoToAsync("//LoginPage");
    }

    // Expresión regular para validar la contraseña
    private bool IsValidPassword(string password)
    {
        if (string.IsNullOrWhiteSpace(password)) return false;

        var regex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%-_*?&])[A-Za-z\d@$!%-_*?&]{8,}$");
        return regex.IsMatch(password);
    }
}
