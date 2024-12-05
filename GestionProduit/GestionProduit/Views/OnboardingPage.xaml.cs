using System;
using Xamarin.Forms;

namespace GestionProduit
{
    public partial class OnboardingPage : ContentPage
    {
        public OnboardingPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false); // Désactiver l'AppBar
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            // Navigation ou logique lors du clic sur le bouton "Sign In"
            await Navigation.PushAsync(new LoginPage());
        }
    }
}
