using GestionProduit.Models;
using GestionProduit.Pages;
using GestionProduit.Views;
using System.Collections.Generic;
using System;
using Xamarin.Forms;

namespace GestionProduit
{
    public partial class HomePage : ContentPage
    {
        private List<Produit> allProduits;

        public HomePage()
        {
            InitializeComponent();
            NavigationPage.SetHasBackButton(this, false);
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            // Charger les Produits depuis la base de données
            allProduits = await App.Database.GetProduitAsync();
            PopulateProduitsGrid(allProduits);
        }

        private void PopulateProduitsGrid(List<Produit> produits)
        {
            ProduitsGrid.Children.Clear();

            // Ajouter les en-têtes
            ProduitsGrid.Children.Add(new Label { Text = "Titre", FontAttributes = FontAttributes.Bold, BackgroundColor = Color.FromHex("#69dbff"), TextColor = Color.White, HorizontalTextAlignment = TextAlignment.Center }, 0, 0);
            ProduitsGrid.Children.Add(new Label { Text = "Statut", FontAttributes = FontAttributes.Bold, BackgroundColor = Color.FromHex("#69dbff"), TextColor = Color.White, HorizontalTextAlignment = TextAlignment.Center }, 1, 0);
            ProduitsGrid.Children.Add(new Label { Text = "Actions", FontAttributes = FontAttributes.Bold, BackgroundColor = Color.FromHex("#69dbff"), TextColor = Color.White, HorizontalTextAlignment = TextAlignment.Center }, 2, 0);

            // Ajouter les produits
            for (int i = 0; i < produits.Count; i++)
            {
                var produit = produits[i];
                int rowIndex = i + 1;

                // Calculer le statut et ses couleurs
                string status;
                Color statusColor;
                if (produit.Quentiter == 0)
                {
                    status = "Hors Stock";
                    statusColor = Color.Red;
                }
                else if (produit.Quentiter <= 20)
                {
                    status = "Faible";
                    statusColor = Color.Red;
                }
                else if (produit.Quentiter <= 40)
                {
                    status = "Moyen";
                    statusColor = Color.Orange;
                }
                else
                {
                    status = "Élevé";
                    statusColor = Color.Green;
                }

                // Ajouter le titre
                ProduitsGrid.Children.Add(new Label { Text = produit.Title, VerticalTextAlignment = TextAlignment.Center }, 0, rowIndex);

                // Ajouter le statut
                ProduitsGrid.Children.Add(new Label { Text = status, TextColor = statusColor, FontAttributes = FontAttributes.Bold, VerticalTextAlignment = TextAlignment.Center }, 1, rowIndex);

                // Ajouter les boutons d'action
                var actionLayout = new StackLayout { Orientation = StackOrientation.Horizontal, Spacing = 5 };

                var updateButton = new ImageButton
                {
                    Source = "https://cdn-icons-png.flaticon.com/128/8771/8771493.png",
                    BackgroundColor = Color.Transparent,
                    HeightRequest = 30,
                    WidthRequest = 30
                };
                updateButton.Clicked += (s, e) => OnUpdateClicked(produit);

                var deleteButton = new ImageButton
                {
                    Source = "https://cdn-icons-png.flaticon.com/128/6861/6861362.png",
                    BackgroundColor = Color.Transparent,
                    HeightRequest = 30,
                    WidthRequest = 30
                };
                deleteButton.Clicked += (s, e) => OnDeleteClicked(produit);

                actionLayout.Children.Add(updateButton);
                actionLayout.Children.Add(deleteButton);

                ProduitsGrid.Children.Add(actionLayout, 2, rowIndex);
            }
        }

        private async void OnUpdateClicked(Produit produit)
        {
            await Navigation.PushAsync(new EditPrduit(produit));
        }

        private async void OnDeleteClicked(Produit produit)
        {
            bool confirm = await DisplayAlert("Confirmation", $"Voulez-vous supprimer {produit.Title} ?", "Oui", "Non");
            if (confirm)
            {
                await App.Database.DeleteProduitAsync(produit);
                allProduits.Remove(produit);
                PopulateProduitsGrid(allProduits);
            }
        }

        async void OnProduitAddedClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CreateProduitPage
            {
                BindingContext = new Produit()
            });
        }

        private async void OnDetailsClicked(Produit produit)
        {
            await Navigation.PushAsync(new ProduitDetailsPage(produit));
        }

        private void OnSearchBarTextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = e.NewTextValue?.ToLower() ?? string.Empty;

            var filteredProduits = string.IsNullOrWhiteSpace(searchText)
                ? allProduits
                : allProduits.FindAll(p => p.Title.ToLower().Contains(searchText) || p.Description.ToLower().Contains(searchText));

            PopulateProduitsGrid(filteredProduits);
        }

        // Event handler for the "Voir les Commandes" button click
        private async void OnViewOrdersClicked(object sender, EventArgs e)
        {
            // Navigate to the AdminOrdersPage
            await Navigation.PushAsync(new AdminOrdersPage());
        }
    }
}
