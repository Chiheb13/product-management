using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using GestionProduit.Models;
using GestionProduit.Pages;

namespace GestionProduit
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditPrduit : ContentPage
    {
        public EditPrduit(Produit produit)
        {
            InitializeComponent();
            BindingContext = produit;
        }


        // Method to handle the "Modifier Produit" button click
        private async void OnUpdateClicked(object sender, EventArgs e)
        {
            var produit = (Produit)BindingContext;
            await Navigation.PushAsync(new CreateProduitPage { BindingContext = produit });
        }
    }
}
