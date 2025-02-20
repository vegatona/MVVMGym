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
        // Limpiar las credenciales almacenadas antes de redirigir
        ClearUserCredentials();

        // Mostrar el mensaje de �xito
        await DisplayAlert("�xito", "Tu contrase�a ha sido cambiada exitosamente.", "Iniciar sesi�n");

        // Redirigir a la p�gina de login
        await Shell.Current.GoToAsync("//LoginPage");
    }

    // M�todo para limpiar las credenciales del usuario
    private void ClearUserCredentials()
    {
        try
        {
            // Eliminar el nombre de usuario y la contrase�a de SecureStorage
            SecureStorage.Remove("username"); // Eliminar el nombre de usuario
            SecureStorage.Remove("password"); // Eliminar la contrase�a
            Console.WriteLine("Credenciales borradas exitosamente.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error al borrar credenciales: " + ex.Message);
        }
    }

    // Validar contrase�a al escribir en la primera casilla
    private void OnPasswordTextChanged(object sender, TextChangedEventArgs e)
    {
        string password = NewPasswordEntry.Text ?? "";
        string confirmPassword = ConfirmPasswordEntry.Text ?? "";

        bool isPasswordValid = IsValidPassword(password);

        // Mostrar error de formato si la contrase�a no es v�lida
        if (!isPasswordValid)
        {
            PasswordErrorLabel.Text = "La contrase�a debe tener al menos 8 caracteres.\nUna may�scula.\nUna min�scula.\nUn n�mero y un car�cter especial.";
            PasswordErrorLabel.IsVisible = true;
            ChangePasswordButton.IsEnabled = false;
        }
        else
        {
            PasswordErrorLabel.IsVisible = false;
        }

        // Si la contrase�a es v�lida y la segunda casilla est� vac�a, mostrar "Las contrase�as no coinciden"
        if (isPasswordValid && string.IsNullOrEmpty(confirmPassword))
        {
            ConfirmPasswordErrorLabel.Text = "Las contrase�as no coinciden.";
            ConfirmPasswordErrorLabel.IsVisible = true;
            ChangePasswordButton.IsEnabled = false;
        }
        else if (isPasswordValid && password != confirmPassword)
        {
            ConfirmPasswordErrorLabel.Text = "Las contrase�as no coinciden.";
            ConfirmPasswordErrorLabel.IsVisible = true;
            ChangePasswordButton.IsEnabled = false;
        }
        else
        {
            ConfirmPasswordErrorLabel.IsVisible = false;
        }

        // Activar bot�n solo si ambas contrase�as son v�lidas
        ChangePasswordButton.IsEnabled = isPasswordValid && password == confirmPassword;
    }

    // Validar la confirmaci�n al escribir en la segunda casilla
    private void OnConfirmPasswordTextChanged(object sender, TextChangedEventArgs e)
    {
        string password = NewPasswordEntry.Text ?? "";
        string confirmPassword = ConfirmPasswordEntry.Text ?? "";

        if (IsValidPassword(password))
        {
            if (password != confirmPassword)
            {
                ConfirmPasswordErrorLabel.Text = "Las contrase�as no coinciden.";
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

    // Limpiar los campos al navegar a otra p�gina
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
