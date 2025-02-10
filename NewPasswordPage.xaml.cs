using System.Text.RegularExpressions;

namespace Mockup;

public partial class NewPasswordPage : ContentPage
{
    public NewPasswordPage()
    {
        InitializeComponent();
    }

    private async void OnPasswordChanged(object sender, EventArgs e)
    {
        await DisplayAlert("Éxito", "Tu contraseña ha sido cambiada exitosamente.", "Iniciar sesión");
        await Shell.Current.GoToAsync("//LoginPage");
    }

    // Validar contraseña al escribir en la primera casilla
    private void OnPasswordTextChanged(object sender, TextChangedEventArgs e)
    {
        string password = NewPasswordEntry.Text ?? "";
        string confirmPassword = ConfirmPasswordEntry.Text ?? "";

        bool isPasswordValid = IsValidPassword(password);

        // Mostrar error de formato si la contraseña no es válida
        if (!isPasswordValid)
        {
            PasswordErrorLabel.Text = "La contraseña debe tener al menos 8 caracteres.\nUna mayúscula.\nUna minúscula.\nUn número y un carácter especial.";
            PasswordErrorLabel.IsVisible = true;
            ChangePasswordButton.IsEnabled = false;
        }
        else
        {
            PasswordErrorLabel.IsVisible = false;
        }

        // Si la contraseña es válida y la segunda casilla está vacía, mostrar "Las contraseñas no coinciden"
        if (isPasswordValid && string.IsNullOrEmpty(confirmPassword))
        {
            ConfirmPasswordErrorLabel.Text = "Las contraseñas no coinciden.";
            ConfirmPasswordErrorLabel.IsVisible = true;
            ChangePasswordButton.IsEnabled = false;
        }
        else if (isPasswordValid && password != confirmPassword)
        {
            ConfirmPasswordErrorLabel.Text = "Las contraseñas no coinciden.";
            ConfirmPasswordErrorLabel.IsVisible = true;
            ChangePasswordButton.IsEnabled = false;
        }
        else
        {
            ConfirmPasswordErrorLabel.IsVisible = false;
        }

        // Activar botón solo si ambas contraseñas son válidas
        ChangePasswordButton.IsEnabled = isPasswordValid && password == confirmPassword;
    }

    // Validar la confirmación al escribir en la segunda casilla
    private void OnConfirmPasswordTextChanged(object sender, TextChangedEventArgs e)
    {
        string password = NewPasswordEntry.Text ?? "";
        string confirmPassword = ConfirmPasswordEntry.Text ?? "";

        if (IsValidPassword(password))
        {
            if (password != confirmPassword)
            {
                ConfirmPasswordErrorLabel.Text = "Las contraseñas no coinciden.";
                ConfirmPasswordErrorLabel.IsVisible = true;
                ChangePasswordButton.IsEnabled = false;
            }
            else
            {
                ConfirmPasswordErrorLabel.IsVisible = false;
            }

            ChangePasswordButton.IsEnabled = password == confirmPassword;
        }
        else
        {
            ConfirmPasswordErrorLabel.IsVisible = false;
            ChangePasswordButton.IsEnabled = false;
        }
    }

    private bool IsValidPassword(string password)
    {
        if (string.IsNullOrWhiteSpace(password)) return false;

        var regex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%-_*?&])[A-Za-z\d@$!%-_*?&]{8,}$");
        return regex.IsMatch(password);
    }

    // Limpiar los campos al navegar a otra página
    protected override void OnDisappearing()
    {
        base.OnDisappearing();

        // Limpiar los campos de texto y restablecer el estado del formulario
        NewPasswordEntry.Text = string.Empty;
        ConfirmPasswordEntry.Text = string.Empty;
        PasswordErrorLabel.IsVisible = false;
        ConfirmPasswordErrorLabel.IsVisible = false;
        ChangePasswordButton.IsEnabled = false;
    }
}
