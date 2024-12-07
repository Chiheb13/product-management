using System;
using GestionProduit.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GestionProduit.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductDetail : ContentPage
    {
        private Produit _selectedProduct;

        public ProductDetail(Produit selectedProduct)
        {
            InitializeComponent();
            _selectedProduct = selectedProduct;
            BindingContext = _selectedProduct;
        }

        private async void OnCommanderClicked(object sender, EventArgs e)
        {
            if (App.CurrentUser == null)
            {
                await DisplayAlert("Erreur", "Utilisateur non connecté.", "OK");
                return;
            }

            var newCommande = new Commende
            {
                ProduitID = _selectedProduct.ID,
                ProduitTitle = _selectedProduct.Title,
                UserID = App.CurrentUser.Id, // L'ID de l'utilisateur connecté
                UserName = App.CurrentUser.FullName,
                // Set the status and date explicitly here
                Status = "en attente",  // Ensure the status is set to "en attente"
                Date = DateTime.Now      // Ensure the date is set to the current date and time
            };

            // Save the new commande to the database
            await App.Databasecommende.SaveCommendeAsync(newCommande);

            await DisplayAlert("Succès", "Votre commande a été enregistrée.", "OK");
        }
    }
}
