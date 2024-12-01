using SQLite;
using System;

namespace GestionProduit.Models
{
    public class Produit
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double Prix { get; set; }
        public int Quentiter { get; set; }
        public DateTime DateCreation { get; set; }
        public string ImagePath { get; set; } // Chemin de l'image
    }
}
