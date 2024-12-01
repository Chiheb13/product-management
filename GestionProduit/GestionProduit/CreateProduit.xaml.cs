using Xamarin.Essentials;
using Xamarin.Forms;
using GestionProduit.Models;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace GestionProduit.Pages
{
    public partial class CreateProduitPage : ContentPage
    {
        public CreateProduitPage()
        {
            InitializeComponent();
        }

        async void OnChooseImageClicked(object sender, EventArgs e)
        {
            try
            {
                if (DeviceInfo.Platform == DevicePlatform.Android)
                {
                    // For Android 13 (API 33) and above
                    if (DeviceInfo.Version.Major >= 13)
                    {
                        var mediaStatus = await Permissions.RequestAsync<Permissions.Media>();
                        var photosStatus = await Permissions.RequestAsync<Permissions.Photos>();

                        if (mediaStatus != PermissionStatus.Granted || photosStatus != PermissionStatus.Granted)
                        {
                            var result = await DisplayAlert("Permission Required",
                                "This app needs access to your media and photos. Would you like to open settings to grant permissions?",
                                "Open Settings", "Cancel");

                            if (result)
                            {
                                AppInfo.ShowSettingsUI();
                            }
                            return;
                        }
                    }
                    // For Android 12 and below
                    else
                    {
                        var storageWrite = await Permissions.RequestAsync<Permissions.StorageWrite>();
                        var storageRead = await Permissions.RequestAsync<Permissions.StorageRead>();

                        if (storageWrite != PermissionStatus.Granted || storageRead != PermissionStatus.Granted)
                        {
                            var result = await DisplayAlert("Permission Required",
                                "This app needs access to your storage. Would you like to open settings to grant permissions?",
                                "Open Settings", "Cancel");

                            if (result)
                            {
                                AppInfo.ShowSettingsUI();
                            }
                            return;
                        }
                    }
                }

                // Use MediaPicker instead of FilePicker
                var photo = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
                {
                    Title = "Sélectionnez une image"
                });

                if (photo != null)
                {
                    // Load the photo as a stream
                    var stream = await photo.OpenReadAsync();

                    // Display the image
                    SelectedImage.Source = ImageSource.FromStream(() => stream);
                    SelectedImage.IsVisible = true;

                    // Store the image path
                    var produit = (Produit)BindingContext;
                    produit.ImagePath = photo.FullPath;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Full Exception: {ex}");
                await DisplayAlert("Error",
                    "Unable to access photos. Please ensure you have granted the necessary permissions in your device settings.",
                    "OK");
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
