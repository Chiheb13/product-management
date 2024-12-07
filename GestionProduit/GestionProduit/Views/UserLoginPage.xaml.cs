using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using GestionProduit.Services;

namespace GestionProduit.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserLoginPage : ContentPage
    {
        private readonly DatabaseService _databaseService;

        public UserLoginPage()
        {
            InitializeComponent();
            _databaseService = new DatabaseService(); // Initialize the database service here
        }

        private async void OnSignUpClicked(object sender, EventArgs e)
        {
            // Navigate to the Register Page
            await Navigation.PushAsync(new RegisterPage());
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            string email = emailEntry?.Text; // Use null-conditional operator to avoid potential null reference issues
            string password = passwordEntry?.Text;

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                await DisplayAlert("Error", "Email and password are required.", "OK");
                return;
            }

            var user = await _databaseService.GetUserAsync(email, password);
            if (user != null)
            {
                // Set the logged-in user globally in App.CurrentUser
                App.CurrentUser = user;

                await DisplayAlert("Success", $"Welcome back, {user.FullName}!", "OK");

                // Navigate to the main page (UserHomepage) and pass the user details if needed
                await Navigation.PushAsync(new UserHomepage()); // UserHomepage can access App.CurrentUser now
            }
            else
            {
                await DisplayAlert("Error", "Invalid email or password.", "OK");
            }
        }

    }
}
