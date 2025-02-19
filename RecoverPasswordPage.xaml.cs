using System.Text.RegularExpressions;

namespace Mockup;

public partial class RecoverPasswordPage : ContentPage
{
    public RecoverPasswordPage()
    {
        InitializeComponent();
        BindingContext = this;

        // Desactivar el botón al principio
        ContinueButton.IsEnabled = false;

        // Asignar evento al Entry para controlar la longitud del texto
        UserEntry.TextChanged += OnUserEntryTextChanged;
    }

    // Validación del usuario (permite letras, números y algunos símbolos, entre 3 y 12 caracteres)
    private void OnUserEntryTextChanged(object sender, TextChangedEventArgs e)
    {
        string username = UserEntry.Text ?? "";

        // Permitir solo letras, números y algunos caracteres especiales
        username = Regex.Replace(username, "[^a-zA-Z0-9._,@!*+-]", "");

        // Limitar a 12 caracteres
        if (username.Length > 12)
        {
            username = username.Substring(0, 12);
        }

        // Evitar la reasignación del texto si no es necesario
        if (UserEntry.Text != username)
        {
            UserEntry.Text = username;
        }

        // Si el campo está vacío, ocultar la alerta y deshabilitar el botón
        if (string.IsNullOrWhiteSpace(username))
        {
            ErrorLabel.IsVisible = false;
            ContinueButton.IsEnabled = false;
            return;
        }

        // Si el usuario tiene entre 3 y 12 caracteres, ocultar la alerta y habilitar el botón
        if (username.Length >= 3 && username.Length <= 12)
        {
            ErrorLabel.IsVisible = false; // Ocultamos la alerta si es válido
            ContinueButton.IsEnabled = true; // Habilitamos el botón
        }
        else
        {
            // Si el usuario tiene menos de 3 caracteres o más de 12, mostrar la alerta
            ErrorLabel.Text = "El usuario debe tener entre 3 y 12 caracteres.";
            ErrorLabel.IsVisible = true;
            ContinueButton.IsEnabled = false; // Deshabilitar el botón
        }
    }

    // Navegar a la página de "Ingresar Código"
    private async void OnContinueClicked(object sender, EventArgs e)
    {
        string username = UserEntry.Text ?? "";

        // Verificar si el campo está vacío o no tiene entre 3 y 12 caracteres
        if (string.IsNullOrWhiteSpace(username))
        {
            ErrorLabel.Text = "Por favor ingresa un número de usuario.";
            ErrorLabel.IsVisible = true;
            return;
        }

        // Si la validación es correcta, navegar a la siguiente página
        await Shell.Current.GoToAsync("//EnterCodePage");
    }

    // Navegar a la página de registro
    public Command GoToRegister => new Command(async () =>
    {
        await Shell.Current.GoToAsync("//RegisterPage");
    });
}
