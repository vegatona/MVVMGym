using Mockup.MVVM.ViewModel;

namespace Mockup;

public partial class EnterCodePage : ContentPage
{
    public EnterCodePage()
    {
        InitializeComponent();
        var viewModel = new EnterCodeViewModel();
        BindingContext = viewModel;
        viewModel.FocusNextRequested += OnFocusNextRequested;
        CodeEntry1.Focus();
    }

    // Establece el foco en el Entry correspondiente según el índice solicitado
    private void OnFocusNextRequested(int index)
    {
        switch (index)
        {
            case 2:
                CodeEntry2.Focus();
                break;
            case 3:
                CodeEntry3.Focus();
                break;
            case 4:
                CodeEntry4.Focus();
                break;
            case 5:
                CodeEntry5.Focus();
                break;
            case 6:
                CodeEntry6.Focus();
                break;
        }
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        // Se podría limpiar el contenido si se requiere, aunque la lógica se puede mover al ViewModel.
    }
}
