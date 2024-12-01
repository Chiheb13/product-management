using System;
using System.Collections.Generic;
using System.ComponentModel;
using GestionProduit.Models;
using GestionProduit.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GestionProduit.Views
{
    public partial class NewItemPage : ContentPage
    {
        public Item Item { get; set; }

        public NewItemPage()
        {
            InitializeComponent();
            BindingContext = new NewItemViewModel();
        }
    }
}