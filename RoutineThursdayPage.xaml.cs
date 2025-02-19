using Microsoft.Maui.Controls;
using System;

namespace Mockup
{
    [QueryProperty(nameof(Username), "username")]
    [QueryProperty(nameof(UserNumber), "userNumber")]
    [QueryProperty(nameof(UserImage), "userImage")]
    public partial class RoutineThursdayPage : ContentPage
    {
        private string _username;
        private string _userNumber;
        private string _userImage;

        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        public string UserNumber
        {
            get => _userNumber;
            set
            {
                _userNumber = value;
                OnPropertyChanged(nameof(UserNumber));
            }
        }

        public string UserImage
        {
            get => _userImage;
            set
            {
                _userImage = value;
                OnPropertyChanged(nameof(UserImage));
            }
        }

        public RoutineThursdayPage()
        {
            InitializeComponent();
            BindingContext = this;
        }
        private async void OnInstructionsTapped(object sender, EventArgs e)
        {
            if (sender is Label label && label.Text != null)
            {
                await DisplayAlert("Ejercicio Seleccionado", label.Text, "OK");
            }
        }
    }
}
