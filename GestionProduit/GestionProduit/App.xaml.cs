﻿using System;
using System.IO;
using GestionProduit.DataBase;
using GestionProduit.Models;
using GestionProduit.Views;
using Xamarin.Forms;

namespace GestionProduit
{
    public partial class App : Application
    {
        public static User CurrentUser { get; set; }
        private static ProduitDatabase _database;
        private static CommendeDatabase _databasee;

        // Propriété statique pour accéder à la base de données
        public static ProduitDatabase Database
        {
            get
            {
                if (_database == null)
                {
                    var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Produits.db3");
                    _database = new ProduitDatabase(dbPath);
                }
                return _database;
            }
        }
        public static CommendeDatabase Databasecommende
        {
            get
            {
                if (_databasee == null)
                {
                    var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Produits.db3");
                    _databasee = new CommendeDatabase(dbPath);
                }
                return _databasee;
            }
        }

        public App()
        {
            InitializeComponent();
            Application.Current.UserAppTheme = OSAppTheme.Light;

            // Définir la page principale de navigation
            MainPage = new NavigationPage(new OnboardingPage());
        }

        protected override void OnStart()
        {
            // Logique d'initialisation lors du démarrage de l'application
        }

        protected override void OnSleep()
        {
            // Logique à exécuter lorsque l'application entre en veille
        }

        protected override void OnResume()
        {
            // Logique à exécuter lorsque l'application revient en avant-plan
        }
    }
}
