using System;
using GestionProduit.Views;
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
            
            await Navigation.PushAsync(new LoginPageAdmin());
        }
        private async void OnUserLoginClicked(object sender, EventArgs e)
        {
           
            await Navigation.PushAsync(new UserLoginPage());
        }
    }
}
