using Xamarin.Essentials;
using Xamarin.Forms;
using GestionProduit.Models;
using System;
using System.Threading.Tasks;
using System.IO;
using Plugin.Media;
using Plugin.Media.Abstractions;
using GestionProduit.ViewModels;

namespace GestionProduit.Pages
{
    public partial class CreateProduitPage : ContentPage
    {
        public CreateProduitPage()
        {
            InitializeComponent();
            BindingContext = new CreateProduitViewModel();
        }


        private async Task CheckPermissions()
        {
            try
            {
                await CrossMedia.Current.Initialize();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Media plugin initialization error: {ex}");
            }
        }

        async void OnChooseImageClicked(object sender, EventArgs e)
        {
            try
            {
                if (!CrossMedia.Current.IsPickPhotoSupported)
                {
                    await DisplayAlert("Not supported", "Your device doesn't support photo picking", "OK");
                    return;
                }

                var file = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions
                {
                    PhotoSize = PhotoSize.Medium,
                    CompressionQuality = 80
                });

                if (file == null)
                    return;

                // Create a local copy in the app's storage
                var localPath = Path.Combine(FileSystem.AppDataDirectory,
                    $"product_image_{DateTime.Now.Ticks}{Path.GetExtension(file.Path)}");

                using (var sourceStream = File.OpenRead(file.Path))
                using (var destinationStream = File.Create(localPath))
                {
                    await sourceStream.CopyToAsync(destinationStream);
                }

                // Ensure UI updates happen on the main thread
                Device.BeginInvokeOnMainThread(() =>
                {
                    SelectedImage.Source = ImageSource.FromFile(localPath);
                    SelectedImage.IsVisible = true;
                });

                // Update the binding context
                var produit = (Produit)BindingContext;
                produit.ImagePath = localPath;

                // Clean up the temporary file
                file.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Image picking error: {ex}");
                await DisplayAlert("Error", "An error occurred while selecting the image. Please try again.", "OK");
            }
        }

        async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            var produit = (Produit)BindingContext;
            produit.DateCreation = DateTime.UtcNow;
            await App.Database.SaveProduitAsync(produit);
            await Navigation.PopAsync();
        }

        async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            var produit = (Produit)BindingContext;
            bool confirm = await DisplayAlert("Confirmation", "Voulez-vous vraiment supprimer ce produit ?", "Oui", "Non");
            if (confirm)
            {
                await App.Database.DeleteProduitAsync(produit);
                await Navigation.PopAsync();
            }
        }
    }
}
