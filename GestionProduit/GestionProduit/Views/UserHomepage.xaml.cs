using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using GestionProduit.Models;
using GestionProduit.DataBase;

namespace GestionProduit.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserHomepage : ContentPage
    {
        private readonly ProduitDatabase _produitDatabase;
        public ObservableCollection<Produit> Products { get; set; }

        public UserHomepage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false); // Désactiver l'AppBar

            Products = new ObservableCollection<Produit>();
            BindingContext = this;
            _produitDatabase = App.Database; // Shared instance
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadProducts();
        }

        private async Task LoadProducts()
        {
            try
            {
                var allProduits = await _produitDatabase.GetProduitAsync();
                Products.Clear();

                foreach (var product in allProduits)
                {
                    Products.Add(product);
                }

                if (Products.Count == 0)
                {
                    await DisplayAlert("Info", "No products available", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }

        private async void OnViewDetailsClicked(object sender, EventArgs e)
        {
            if (sender is Button button && button.CommandParameter is Produit selectedProduct)
            {
                await Navigation.PushAsync(new ProductDetail(selectedProduct));
            }
        }
        private async void OnViewOrdersClicked(object sender, EventArgs e)
        {
         
            await Navigation.PushAsync(new UserOrderPage(App.CurrentUser.Id));
        }
    }
}
