using System;
using Xamarin.Forms;
using GestionProduit.Models;
using GestionProduit.Services;
using GestionProduit.Views;

namespace GestionProduit
{
    public partial class RegisterPage : ContentPage
    {
        private readonly DatabaseService _databaseService;

        public RegisterPage()
        {
            InitializeComponent();
            _databaseService = new DatabaseService();
        }

        private async void OnRegisterClicked(object sender, EventArgs e)
        {
            string fullName = fullNameEntry.Text;
            string email = emailEntry.Text;
            string password = passwordEntry.Text;
            string confirmPassword = confirmPasswordEntry.Text;

            if (string.IsNullOrWhiteSpace(fullName) || string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(confirmPassword))
            {
                await DisplayAlert("Error", "All fields are required.", "OK");
                return;
            }

            if (password != confirmPassword)
            {
                await DisplayAlert("Error", "Passwords do not match.", "OK");
                return;
            }

            if (await _databaseService.UserExistsAsync(email))
            {
                await DisplayAlert("Error", "An account with this email already exists.", "OK");
                return;
            }

            var user = new User
            {
                FullName = fullName,
                Email = email,
                Password = password
            };

            await _databaseService.AddUserAsync(user);
            await DisplayAlert("Success", "Account created successfully!", "OK");

            // Navigate to the login page
            await Navigation.PushAsync(new UserLoginPage());
        }
    }
}