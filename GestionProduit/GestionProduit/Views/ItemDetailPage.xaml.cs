using System.ComponentModel;
using GestionProduit.ViewModels;
using Xamarin.Forms;

namespace GestionProduit.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}