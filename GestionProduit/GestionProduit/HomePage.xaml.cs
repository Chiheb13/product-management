using System;
using System.Collections.Generic;
using System.Linq;
using GestionProduit.Models;
using GestionProduit.Pages;
using Xamarin.Forms;

namespace GestionProduit
{
    public partial class HomePage : ContentPage
    {
        private List<Produit> allProduits;

        public HomePage()
        {
            InitializeComponent();
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

            // Add headers
            ProduitsGrid.Children.Add(new Label { Text = "Titre", FontAttributes = FontAttributes.Bold }, 0, 0);
            ProduitsGrid.Children.Add(new Label { Text = "Status", FontAttributes = FontAttributes.Bold }, 1, 0);
            ProduitsGrid.Children.Add(new Label { Text = "Actions", FontAttributes = FontAttributes.Bold }, 2, 0);

            // Add products
            for (int i = 0; i < produits.Count; i++)
            {
                var produit = produits[i];
                int rowIndex = i + 1;

                // Calculate status based on quantity using if-else
                string status;
                if (produit.Quentiter <= 20)
                {
                    status = "faible";
                }
                else if (produit.Quentiter <= 40)
                {
                    status = "moyen";
                }
                else
                {
                    status = "élevé";
                }

                // Wrap product details in a Frame to make it look like a card
                var frame = new Frame
                {
                    BorderColor = Color.Gray,
                    Padding = 10,
                    Margin = new Thickness(5),
                    CornerRadius = 10,
                    HasShadow = true
                };

                var productLayout = new Grid
                {
                    ColumnSpacing = 10,
                    RowSpacing = 10,
                    RowDefinitions =
            {
                new RowDefinition { Height = GridLength.Auto }
            }
                };

                // Add title
                productLayout.Children.Add(new Label { Text = produit.Title }, 0, 0);

                // Add status with color coding
                var statusLabel = new Label { Text = status, FontAttributes = FontAttributes.Bold };
                if (status == "faible")
                {
                    statusLabel.TextColor = Color.Red;
                }
                else if (status == "moyen")
                {
                    statusLabel.TextColor = Color.Orange;
                }
                else
                {
                    statusLabel.TextColor = Color.Green;
                }
                productLayout.Children.Add(statusLabel, 1, 0);

                // Add action buttons
                var actionLayout = new StackLayout { Orientation = StackOrientation.Horizontal, Spacing = 5 };

                // Create update button with icon
                var updateButton = new ImageButton
                {
                    Source = "https://cdn-icons-png.flaticon.com/128/8771/8771493.png",
                    BackgroundColor = Color.FromHex("#48d6fa"),
                    CornerRadius = 10,
                    HeightRequest = 40,
                    WidthRequest = 40,
                    Padding = new Thickness(5),
                    Aspect = Aspect.AspectFit
                };
                updateButton.Clicked += (sender, e) => OnUpdateClicked(produit);

                // Create delete button with icon
                var deleteButton = new ImageButton
                {
                    Source = "https://cdn-icons-png.flaticon.com/128/6861/6861362.png",
                    BackgroundColor = Color.White,
                    CornerRadius = 10,
                    HeightRequest = 40,
                    WidthRequest = 40,
                    Padding = new Thickness(5),
                    Aspect = Aspect.AspectFit
                };
                deleteButton.Clicked += (sender, e) => OnDeleteClicked(produit);

                actionLayout.Children.Add(updateButton);
                actionLayout.Children.Add(deleteButton);

                // Add the action buttons layout to the product layout
                productLayout.Children.Add(actionLayout, 2, 0);

                // Add the product layout to the frame
                frame.Content = productLayout;

                // Add the frame to the grid
                ProduitsGrid.Children.Add(frame, 0, rowIndex);
                Grid.SetColumnSpan(frame, 3);
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
    }
}