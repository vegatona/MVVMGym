using System.Text.RegularExpressions;
using System.Windows.Input;

namespace Mockup.MVVM.ViewModel
{
    public class RegisterViewModel : BindableObject
    {
        private string _username;
        private string _password;
        private string _confirmPassword;
        private string _usernameError;
        private string _passwordError;
        private string _confirmPasswordError;
        private bool _isRegisterEnabled;

        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged();
                ValidateUsername();
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged();
                ValidatePassword();
                ValidateConfirmPassword();
            }
        }

        public string ConfirmPassword
        {
            get => _confirmPassword;
            set
            {
                _confirmPassword = value;
                OnPropertyChanged();
                ValidateConfirmPassword();
            }
        }

        public string UsernameError
        {
            get => _usernameError;
            set { _usernameError = value; OnPropertyChanged(); }
        }

        public string PasswordError
        {
            get => _passwordError;
            set { _passwordError = value; OnPropertyChanged(); }
        }

        public string ConfirmPasswordError
        {
            get => _confirmPasswordError;
            set { _confirmPasswordError = value; OnPropertyChanged(); }
        }

        public bool IsUsernameErrorVisible => !string.IsNullOrEmpty(UsernameError);
        public bool IsPasswordErrorVisible => !string.IsNullOrEmpty(PasswordError);
        public bool IsConfirmPasswordErrorVisible => !string.IsNullOrEmpty(ConfirmPasswordError);

        public bool IsRegisterEnabled
        {
            get => _isRegisterEnabled;
            set { _isRegisterEnabled = value; OnPropertyChanged(); }
        }

        public ICommand RegisterCommand => new Command(Register);
        public ICommand GoToLoginCommand => new Command(async () => await Shell.Current.GoToAsync("//LoginPage"));

        private void ValidateUsername()
        {
            if (string.IsNullOrWhiteSpace(Username) || Username.Length < 3 || Username.Length > 12)
            {
                UsernameError = "El usuario debe tener entre 3 y 12 caracteres.";
            }
            else
            {
                UsernameError = null;
            }
            UpdateRegisterButtonState();
        }

        private void ValidatePassword()
        {
            if (!IsValidPassword(Password))
            {
                PasswordError = "Debe tener al menos 8 caracteres, una mayúscula, una minúscula, un número y un carácter especial.";
            }
            else
            {
                PasswordError = null;
            }
            UpdateRegisterButtonState();
        }

        private void ValidateConfirmPassword()
        {
            if (Password != ConfirmPassword)
            {
                ConfirmPasswordError = "Las contraseñas no coinciden.";
            }
            else
            {
                ConfirmPasswordError = null;
            }
            UpdateRegisterButtonState();
        }

        private void UpdateRegisterButtonState()
        {
            IsRegisterEnabled = string.IsNullOrEmpty(UsernameError) &&
                                string.IsNullOrEmpty(PasswordError) &&
                                string.IsNullOrEmpty(ConfirmPasswordError);
        }

        private async void Register()
        {
            if (!IsRegisterEnabled) return;

            await Application.Current.MainPage.DisplayAlert("Éxito", "Tu registro ha sido exitoso.", "Iniciar sesión");
            await Shell.Current.GoToAsync("//LoginPage");
        }

        private bool IsValidPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password)) return false;
            var regex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%-_*?&])[A-Za-z\d@$!%-_*?&]{8,}$");
            return regex.IsMatch(password);
        }
    }
}