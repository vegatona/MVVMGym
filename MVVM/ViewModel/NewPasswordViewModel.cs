using System;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace Mockup.MVVM.ViewModel
{
    public class NewPasswordViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _newPassword;
        private string _confirmPassword;
        private string _passwordError;
        private string _confirmPasswordError;
        private bool _canChangePassword;
        private bool _hasPasswordError;
        private bool _hasConfirmPasswordError;

        // Propiedad para la nueva contraseña
        public string NewPassword
        {
            get => _newPassword;
            set
            {
                _newPassword = value;
                OnPropertyChanged(nameof(NewPassword));
                ValidatePasswords();
            }
        }

        // Confirmación de la contraseña
        public string ConfirmPassword
        {
            get => _confirmPassword;
            set
            {
                _confirmPassword = value;
                OnPropertyChanged(nameof(ConfirmPassword));
                ValidatePasswords();
            }
        }

        // Mensaje de error para la nueva contraseña
        public string PasswordError
        {
            get => _passwordError;
            set
            {
                _passwordError = value;
                OnPropertyChanged(nameof(PasswordError));
            }
        }

        // Mensaje de error si las contraseñas no coinciden
        public string ConfirmPasswordError
        {
            get => _confirmPasswordError;
            set
            {
                _confirmPasswordError = value;
                OnPropertyChanged(nameof(ConfirmPasswordError));
            }
        }

        // Indica si se debe mostrar el mensaje de error de contraseña
        public bool HasPasswordError
        {
            get => _hasPasswordError;
            set
            {
                _hasPasswordError = value;
                OnPropertyChanged(nameof(HasPasswordError));
            }
        }

        // Indica si se debe mostrar el mensaje de error de confirmación
        public bool HasConfirmPasswordError
        {
            get => _hasConfirmPasswordError;
            set
            {
                _hasConfirmPasswordError = value;
                OnPropertyChanged(nameof(HasConfirmPasswordError));
            }
        }

        // Habilita o deshabilita el botón de cambiar contraseña
        public bool CanChangePassword
        {
            get => _canChangePassword;
            set
            {
                _canChangePassword = value;
                OnPropertyChanged(nameof(CanChangePassword));
                ((Command)ChangePasswordCommand).ChangeCanExecute();
            }
        }

        // Command que se ejecuta al cambiar la contraseña
        public ICommand ChangePasswordCommand { get; }

        public NewPasswordViewModel()
        {
            ChangePasswordCommand = new Command(async () => await ChangePassword(), () => CanChangePassword);
        }

        // Valida ambas contraseñas y actualiza los mensajes y la habilitación del botón
        private void ValidatePasswords()
        {
            bool isValid = IsValidPassword(NewPassword);
            if (!isValid)
            {
                PasswordError = "La contraseña debe tener al menos 8 caracteres,\nuna mayúscula,\nuna minúscula,\nun número y un carácter especial.";
                HasPasswordError = true;
            }
            else
            {
                PasswordError = "";
                HasPasswordError = false;
            }

            if (isValid)
            {
                if (string.IsNullOrWhiteSpace(ConfirmPassword) || NewPassword != ConfirmPassword)
                {
                    ConfirmPasswordError = "Las contraseñas no coinciden.";
                    HasConfirmPasswordError = true;
                }
                else
                {
                    ConfirmPasswordError = "";
                    HasConfirmPasswordError = false;
                }
            }
            else
            {
                ConfirmPasswordError = "";
                HasConfirmPasswordError = false;
            }

            // El botón se habilita solo si la contraseña es válida y coincide con la confirmación
            CanChangePassword = isValid && (!string.IsNullOrWhiteSpace(NewPassword)) && (NewPassword == ConfirmPassword);
        }

        // Lógica para cambiar la contraseña:
        // Se limpian las credenciales almacenadas, se muestra un mensaje y se navega a la página de Login
        private async Task ChangePassword()
        {
            try
            {
                // Eliminar las credenciales almacenadas
                SecureStorage.Remove("username");
                SecureStorage.Remove("password");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error al borrar credenciales: " + ex.Message);
            }

            await Shell.Current.DisplayAlert("Éxito", "Tu contraseña ha sido cambiada exitosamente.", "Iniciar sesión");
            await Shell.Current.GoToAsync("//LoginPage");
        }

        // Valida la contraseña utilizando una expresión regular
        private bool IsValidPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password)) return false;
            var regex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%-_*?&])[A-Za-z\d@$!%-_*?&]{8,}$");
            return regex.IsMatch(password);
        }

        // Método para notificar cambios en las propiedades
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}