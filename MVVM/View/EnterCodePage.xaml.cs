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

            ConfirmButton.IsEnabled = false;
            Entry1.Focus();
        };
    }

    private async void OnConfirmClicked(object sender, EventArgs e)
    {
        string code = string.Concat(entries.Select(entry => entry.Text ?? ""));

        if (code.Length != 6 || !code.All(char.IsDigit)) return;

        await Task.Delay(2000);
        await Shell.Current.GoToAsync("//NewPasswordPage");
    }

    private void OnEntryTextChanged(object sender, TextChangedEventArgs e)
    {
        if (entries == null) return;

        if (sender is Entry currentEntry)
        {
            int currentIndex = entries.IndexOf(currentEntry);

            if (!string.IsNullOrEmpty(e.NewTextValue) && e.NewTextValue.Length > 1)
            {
                currentEntry.Text = e.OldTextValue;
                return;
            }

            if (!string.IsNullOrEmpty(e.NewTextValue) && !char.IsDigit(e.NewTextValue.Last()))
            {
                currentEntry.Text = "";
                return;
            }

            if (!string.IsNullOrEmpty(e.NewTextValue) && currentIndex < entries.Count - 1)
            {
                entries[currentIndex + 1].Focus();
            }
            else if (string.IsNullOrEmpty(e.NewTextValue))
            {
                entries[currentIndex].Focus();
            }

            ValidateCode();
        }
    }

    private void ValidateCode()
    {
        string code = string.Concat(entries.Select(entry => entry.Text ?? ""));
        ConfirmButton.IsEnabled = code.Length == 6;
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();

        foreach (var entry in entries)
        {
            entry.Text = string.Empty;
        }

        ConfirmButton.IsEnabled = false;
    }
}
