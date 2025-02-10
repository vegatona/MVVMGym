using System.Text.RegularExpressions;

namespace Mockup;

public partial class RegisterPage : ContentPage
{
    public RegisterPage()
    {
        InitializeComponent();
        BindingContext = this;
    }

    // Validación del usuario (solo 6 dígitos)
    private void OnUsernameTextChanged(object sender, TextChangedEventArgs e)
    {
        string username = UsernameEntry.Text ?? "";

        if (username.Length == 6)
        {
            UsernameErrorLabel.IsVisible = false;
            RegisterButton.IsEnabled = IsValidPassword(PasswordEntry.Text) && PasswordEntry.Text == ConfirmPasswordEntry.Text;
        }
        else
        {
            UsernameErrorLabel.Text = "El usuario debe tener 6 dígitos.";
            UsernameErrorLabel.IsVisible = true;
            RegisterButton.IsEnabled = false;
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
        if (string.IsNullOrWhiteSpace(username) || username.Length != 6)
        {
            await DisplayAlert("Error", "El usuario debe tener 6 dígitos.", "OK");
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
