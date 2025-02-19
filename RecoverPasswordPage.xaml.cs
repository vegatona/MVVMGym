using System.Text.RegularExpressions;

namespace Mockup;

public partial class RecoverPasswordPage : ContentPage
{
    public RecoverPasswordPage()
    {
        InitializeComponent();
        BindingContext = this;

        // Desactivar el bot�n al principio
        ContinueButton.IsEnabled = false;

        // Asignar evento al Entry para controlar la longitud del texto
        UserEntry.TextChanged += OnUserEntryTextChanged;
    }

    // Validaci�n del usuario (permite letras, n�meros y algunos s�mbolos, entre 3 y 12 caracteres)
    private void OnUserEntryTextChanged(object sender, TextChangedEventArgs e)
    {
        string username = UserEntry.Text ?? "";

        // Permitir solo letras, n�meros y algunos caracteres especiales
        username = Regex.Replace(username, "[^a-zA-Z0-9._,@!*+-]", "");

        // Limitar a 12 caracteres
        if (username.Length > 12)
        {
            username = username.Substring(0, 12);
        }

        // Evitar la reasignaci�n del texto si no es necesario
        if (UserEntry.Text != username)
        {
            UserEntry.Text = username;
        }

        // Si el campo est� vac�o, ocultar la alerta y deshabilitar el bot�n
        if (string.IsNullOrWhiteSpace(username))
        {
            ErrorLabel.IsVisible = false;
            ContinueButton.IsEnabled = false;
            return;
        }

        // Si el usuario tiene entre 3 y 12 caracteres, ocultar la alerta y habilitar el bot�n
        if (username.Length >= 3 && username.Length <= 12)
        {
            ErrorLabel.IsVisible = false; // Ocultamos la alerta si es v�lido
            ContinueButton.IsEnabled = true; // Habilitamos el bot�n
        }
        else
        {
            // Si el usuario tiene menos de 3 caracteres o m�s de 12, mostrar la alerta
            ErrorLabel.Text = "El usuario debe tener entre 3 y 12 caracteres.";
            ErrorLabel.IsVisible = true;
            ContinueButton.IsEnabled = false; // Deshabilitar el bot�n
        }
    }

    // Navegar a la p�gina de "Ingresar C�digo"
    private async void OnContinueClicked(object sender, EventArgs e)
    {
        string username = UserEntry.Text ?? "";

        // Verificar si el campo est� vac�o o no tiene entre 3 y 12 caracteres
        if (string.IsNullOrWhiteSpace(username))
        {
            ErrorLabel.Text = "Por favor ingresa un n�mero de usuario.";
            ErrorLabel.IsVisible = true;
            return;
        }

        // Si la validaci�n es correcta, navegar a la siguiente p�gina
        await Shell.Current.GoToAsync("//EnterCodePage");
    }

    // Navegar a la p�gina de registro
    public Command GoToRegister => new Command(async () =>
    {
        await Shell.Current.GoToAsync("//RegisterPage");
    });
}
