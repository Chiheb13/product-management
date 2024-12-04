using System;
using Xamarin.Forms;

namespace GestionProduit
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            string adminEmail = "admin@gmail.com";
            string adminPassword = "admin123";

            if (true) // Replace with your logic
            {
                await Navigation.PushAsync(new HomePage());
            }
            else
            {
                await DisplayAlert("Login Failed", "Invalid email or password. Please try again.", "OK");
            }
        }
    }
}
