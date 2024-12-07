using System;
using System.Collections.Generic;
using Xamarin.Forms;
using GestionProduit.Models;

namespace GestionProduit.Views
{
    public partial class AdminOrdersPage : ContentPage
    {
        private List<Commende> allOrders;

        public AdminOrdersPage()
        {
            InitializeComponent();
            NavigationPage.SetHasBackButton(this, false);
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // Charger les commandes depuis la base de données
            allOrders = await App.Databasecommende.GetAllCommendesAsync();
            PopulateOrdersGrid(allOrders);
        }

        private void PopulateOrdersGrid(List<Commende> orders)
        {
            OrdersGrid.Children.Clear();

            // Add headers
            OrdersGrid.Children.Add(new Label { Text = "Produit", FontAttributes = FontAttributes.Bold, BackgroundColor = Color.FromHex("#69dbff"), TextColor = Color.White, HorizontalTextAlignment = TextAlignment.Center }, 0, 0);
            OrdersGrid.Children.Add(new Label { Text = "Utilisateur", FontAttributes = FontAttributes.Bold, BackgroundColor = Color.FromHex("#69dbff"), TextColor = Color.White, HorizontalTextAlignment = TextAlignment.Center }, 1, 0);
            OrdersGrid.Children.Add(new Label { Text = "Statut", FontAttributes = FontAttributes.Bold, BackgroundColor = Color.FromHex("#69dbff"), TextColor = Color.White, HorizontalTextAlignment = TextAlignment.Center }, 2, 0);
            OrdersGrid.Children.Add(new Label { Text = "Date de Commande", FontAttributes = FontAttributes.Bold, BackgroundColor = Color.FromHex("#69dbff"), TextColor = Color.White, HorizontalTextAlignment = TextAlignment.Center }, 3, 0);
            OrdersGrid.Children.Add(new Label { Text = "Actions", FontAttributes = FontAttributes.Bold, BackgroundColor = Color.FromHex("#69dbff"), TextColor = Color.White, HorizontalTextAlignment = TextAlignment.Center }, 4, 0);

            // Add orders
            for (int i = 0; i < orders.Count; i++)
            {
                var order = orders[i];
                int rowIndex = i + 1;

                OrdersGrid.Children.Add(new Label { Text = order.ProduitTitle, VerticalTextAlignment = TextAlignment.Center }, 0, rowIndex);
                OrdersGrid.Children.Add(new Label { Text = order.UserName, VerticalTextAlignment = TextAlignment.Center }, 1, rowIndex);
                OrdersGrid.Children.Add(new Label { Text = order.Status, VerticalTextAlignment = TextAlignment.Center }, 2, rowIndex);
                OrdersGrid.Children.Add(new Label { Text = order.Date.ToString("dd MMM yyyy"), VerticalTextAlignment = TextAlignment.Center }, 3, rowIndex);

                var actionLayout = new StackLayout { Orientation = StackOrientation.Horizontal, Spacing = 5 };
                var detailsButton = new ImageButton { Source = "https://cdn-icons-png.flaticon.com/128/8771/8771493.png", BackgroundColor = Color.Transparent, HeightRequest = 30, WidthRequest = 30 };
                var deleteButton = new ImageButton { Source = "https://cdn-icons-png.flaticon.com/128/6861/6861362.png", BackgroundColor = Color.Transparent, HeightRequest = 30, WidthRequest = 30 };

                detailsButton.Clicked += (s, e) => OnDetailsClicked(order);
                deleteButton.Clicked += (s, e) => OnDeleteClicked(order);

                actionLayout.Children.Add(detailsButton);
                actionLayout.Children.Add(deleteButton);

                OrdersGrid.Children.Add(actionLayout, 4, rowIndex);
            }
        }


        private async void OnDeleteClicked(Commende order)
        {
            bool confirm = await DisplayAlert("Confirmation", $"Voulez-vous supprimer cette commande?", "Oui", "Non");
            if (confirm)
            {
                await App.Databasecommende.DeleteCommendeAsync(order);
                allOrders.Remove(order);
                PopulateOrdersGrid(allOrders);
            }
        }

        private async void OnDetailsClicked(Commende order)
        {
          
        }
    }
}
