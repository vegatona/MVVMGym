namespace Mockup;

public partial class EnterCodePage : ContentPage
{
    private List<Entry> entries;

    public EnterCodePage()
    {
        InitializeComponent();

        Loaded += (s, e) =>
        {
            entries = new List<Entry> { Entry1, Entry2, Entry3, Entry4, Entry5, Entry6 };

            foreach (var entry in entries)
            {
                entry.TextChanged += OnEntryTextChanged;
            }

            ConfirmButton.IsEnabled = false; // Desactivar el botón al principio
            Entry1.Focus(); // Poner el foco en el primer campo
        };
    }

    // Función que se ejecuta cuando se hace clic en "Confirmar"
    private async void OnConfirmClicked(object sender, EventArgs e)
    {
        string code = string.Concat(entries.Select(entry => entry.Text ?? ""));

        // Validar longitud y solo números
        if (code.Length != 6 || !code.All(char.IsDigit))
        {
            errorLabel.Text = "Debes ingresar exactamente 6 dígitos numéricos.";
            errorLabel.IsVisible = true;
            return;
        }

        // Si el código es válido, ocultar el mensaje de error
        errorLabel.IsVisible = false;

        // Simular proceso de validación de código (como un delay de 2 segundos)
        await Task.Delay(2000); // Simular validación, como si fuese una llamada a un servicio

        // Navegar a la página de nueva contraseña
        await Shell.Current.GoToAsync("//NewPasswordPage");
    }

    // Función que maneja los cambios en los campos de entrada de código
    private void OnEntryTextChanged(object sender, TextChangedEventArgs e)
    {
        if (entries == null) return;

        if (sender is Entry currentEntry)
        {
            int currentIndex = entries.IndexOf(currentEntry);

            // Evitar más de un carácter por casilla
            if (!string.IsNullOrEmpty(e.NewTextValue) && e.NewTextValue.Length > 1)
            {
                currentEntry.Text = e.OldTextValue;
                return;
            }

            // Permitir solo números
            if (!string.IsNullOrEmpty(e.NewTextValue) && !char.IsDigit(e.NewTextValue.Last()))
            {
                currentEntry.Text = "";
                return;
            }

            // Mover el foco a la siguiente casilla si tiene texto
            if (!string.IsNullOrEmpty(e.NewTextValue) && currentIndex < entries.Count - 1)
            {
                entries[currentIndex + 1].Focus();
            }

            // Si la casilla está vacía, mantener el foco en la misma casilla
            if (string.IsNullOrEmpty(e.NewTextValue) && currentIndex < entries.Count)
            {
                entries[currentIndex].Focus();
            }

            ValidateCode(); // Validar el código en cada cambio
        }
    }

    // Función para validar el código
    private void ValidateCode()
    {
        string code = string.Concat(entries.Select(entry => entry.Text ?? ""));

        // Si el código tiene exactamente 6 dígitos, habilitar el botón
        if (code.Length == 6)
        {
            ConfirmButton.IsEnabled = true;
            errorLabel.IsVisible = false;
        }
        else
        {
            ConfirmButton.IsEnabled = false;
            errorLabel.IsVisible = true;
            errorLabel.Text = "Ingresar exactamente 6 dígitos.";
        }
    }

    // Limpiar los campos al navegar a otra página
    protected override void OnDisappearing()
    {
        base.OnDisappearing();

        // Limpiar los campos y restablecer el estado del formulario
        foreach (var entry in entries)
        {
            entry.Text = string.Empty;
        }

        errorLabel.IsVisible = false;
        ConfirmButton.IsEnabled = false;
    }
}
