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

    // Validar que solo se ingresen exactamente 6 dígitos
    private void OnUserEntryTextChanged(object sender, TextChangedEventArgs e)
    {
        string username = UserEntry.Text ?? "";

        // Limitar a 6 dígitos
        if (username.Length > 6)
        {
            UserEntry.Text = username.Substring(0, 6); // Limitar a 6 dígitos
        }

        // Validación del usuario
        if (username.Length != 6)
        {
            ErrorLabel.Text = "El usuario debe tener 6 dígitos.";
            ErrorLabel.IsVisible = true;

            // Desactivar el botón si no tiene 6 dígitos
            ContinueButton.IsEnabled = false;
        }
        else
        {
            ErrorLabel.IsVisible = false;

            // Habilitar el botón cuando tiene 6 dígitos
            ContinueButton.IsEnabled = true;
        }
    }

    // Navegar a la página de "Ingresar Código"
    private async void OnContinueClicked(object sender, EventArgs e)
    {
        string username = UserEntry.Text ?? "";

        // Verificar si el campo está vacío o no tiene 6 dígitos
        if (string.IsNullOrWhiteSpace(username))
        {
            ErrorLabel.Text = "Por favor ingresa un número de usuario.";
            ErrorLabel.IsVisible = true;
            return;
        }

        if (username.Length != 6)
        {
            ErrorLabel.Text = "El número de usuario debe tener 6 dígitos.";
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
