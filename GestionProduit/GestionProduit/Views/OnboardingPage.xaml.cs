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
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            // Navigate to the login page
            await Navigation.PushAsync(new LoginPage());
        }
    }
}
