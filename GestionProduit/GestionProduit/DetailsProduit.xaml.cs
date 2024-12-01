using Xamarin.Forms;
using GestionProduit.Models;

namespace GestionProduit.Pages
{
    public partial class ProduitDetailsPage : ContentPage
    {
        public ProduitDetailsPage(Produit produit)
        {
            InitializeComponent();
            BindingContext = produit; // Lier les données du produit à la page
        }
    }
}
