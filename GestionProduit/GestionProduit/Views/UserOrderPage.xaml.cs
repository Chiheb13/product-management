using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using GestionProduit.Models;

namespace GestionProduit.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserOrderPage : ContentPage
    {
        private List<Commende> userOrders;

        public UserOrderPage(int userId)
        {
            InitializeComponent();
            LoadUserOrders(userId);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (userOrders != null)
            {
                OrdersListView.ItemsSource = userOrders;
            }
        }

        private async void LoadUserOrders(int userId)
        {
            try
            {
                var allOrders = await App.Databasecommende.GetAllCommendesAsync();
                userOrders = allOrders.Where(order => order.UserID == userId).ToList();
                OrdersListView.ItemsSource = userOrders;

                if (!userOrders.Any())
                {
                    await DisplayAlert("Info", "Aucune commande trouvée pour cet utilisateur.", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erreur", $"Impossible de charger les commandes : {ex.Message}", "OK");
            }
        }
    }
}
