using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows.Input;

namespace Mockup.MVVM.ViewModel
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private string _username;
        private string _password;
        private string _errorMessage;
        private string _passwordErrorMessage;
        private bool _isErrorVisible;
        private bool _isPasswordErrorVisible;
        private bool _isLoginEnabled;

        public event PropertyChangedEventHandler PropertyChanged;

        public string Username
        {
            get => _username;
            set
            {
                _username = SanitizeUsername(value);
                OnPropertyChanged(nameof(Username));
                ValidateForm();
            }
        }
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
                ValidateForm();
            }
        }
        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }
        public string PasswordErrorMessage
        {
            get => _passwordErrorMessage;
            set
            {
                _passwordErrorMessage = value;
                OnPropertyChanged(nameof(PasswordErrorMessage));
            }
        }
        public bool IsErrorVisible
        {
            get => _isErrorVisible;
            set
            {
                _isErrorVisible = value;
                OnPropertyChanged(nameof(IsErrorVisible));
            }
        }
        public bool IsPasswordErrorVisible
        {
            get => _isPasswordErrorVisible;
            set
            {
                _isPasswordErrorVisible = value;
                OnPropertyChanged(nameof(IsPasswordErrorVisible));
            }
        }
        public bool IsLoginEnabled
        {
            get => _isLoginEnabled;
            set
            {
                _isLoginEnabled = value;
                OnPropertyChanged(nameof(IsLoginEnabled));
            }
        }

        // Comandos
        public ICommand LoginCommand { get; }
        public ICommand GoToRegisterCommand { get; }
        public ICommand GoToRecoverPasswordCommand { get; }

        public LoginViewModel()
        {
            LoginCommand = new Command(ExecuteLogin);
            GoToRegisterCommand = new Command(async () => await Shell.Current.GoToAsync("//RegisterPage"));
            GoToRecoverPasswordCommand = new Command(async () => await Shell.Current.GoToAsync("//RecoverPasswordPage"));
        }
        private string SanitizeUsername(string input)
        {
            string sanitized = Regex.Replace(input ?? "", "[^a-zA-Z0-9._,@!*+-]", "");
            return sanitized.Length > 12 ? sanitized.Substring(0, 12) : sanitized;
        }
        private void ValidateForm()
        {
            // Validación del usuario
            if (string.IsNullOrWhiteSpace(Username) || Username.Length < 3)
            {
                ErrorMessage = "El usuario debe tener al menos 3 caracteres.";
                IsErrorVisible = true;
            }
            else
            {
                ErrorMessage = "";
                IsErrorVisible = false;
            }
            // Validación de la contraseña
            if (!IsValidPassword(Password))
            {
                PasswordErrorMessage = "La contraseña debe tener al menos 8 caracteres, una mayúscula, un número y un símbolo.";
                IsPasswordErrorVisible = true;
            }
            else
            {
                PasswordErrorMessage = "";
                IsPasswordErrorVisible = false;
            }
            // Activar botón de login si todo es válido
            IsLoginEnabled = !IsErrorVisible && !IsPasswordErrorVisible;
        }
        private bool IsValidPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password)) return false;
            var regex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%_\-*.?&])[A-Za-z\d@$!%_\-*.?&]{8,}$");
            return regex.IsMatch(password);
        }
        private async void ExecuteLogin()
        {
            if (IsLoginEnabled)
            {
                await Shell.Current.GoToAsync($"//UserDashboardPage?username=Ruben González&userNumber={Username}");
            }
        }
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}