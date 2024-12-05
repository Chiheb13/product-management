using Xamarin.Forms;
using GestionProduit.Models;

namespace GestionProduit.Pages
{
    public partial class ProduitDetailsPage : ContentPage
    {
        public ProduitDetailsPage(Produit produit)
        {
            InitializeComponent();
            BindingContext = produit;
        }
    }
}
