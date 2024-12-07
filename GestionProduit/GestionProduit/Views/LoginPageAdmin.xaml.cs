using System;
using Xamarin.Forms;

namespace GestionProduit
{
    public partial class LoginPageAdmin : ContentPage
    {
        public LoginPageAdmin()
        {
            InitializeComponent();
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            // Credentials for admin
            string adminEmail = "admin@gmail.com";
            string adminPassword = "admin123";

            // Get user input from Entry fields
            string enteredEmail = emailEntry.Text;
            string enteredPassword = passwordEntry.Text;

            // Validate email and password
            if (string.IsNullOrEmpty(enteredEmail) || string.IsNullOrEmpty(enteredPassword))
            {
                await DisplayAlert("Erreur", "Veuillez remplir tous les champs.", "OK");
                return;
            }

            if (enteredEmail == adminEmail && enteredPassword == adminPassword)
            {
                // Navigate to HomePage if login is successful
                await Navigation.PushAsync(new HomePage());
            }
            else
            {
                // Show error message if login fails
                await DisplayAlert("Échec de connexion", "Email ou mot de passe incorrect. Veuillez réessayer.", "OK");
            }
        }
    }
}