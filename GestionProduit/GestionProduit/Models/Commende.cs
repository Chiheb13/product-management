using SQLite;
using System;

namespace GestionProduit.Models
{
    public class Commende
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        [Indexed]
        public int ProduitID { get; set; } 

        public string ProduitTitle { get; set; } 

        public int UserID { get; set; } 
        public string UserName { get; set; } // Nom de l'utilisateur pour affichage

        // New property to hold the status with default value
        public string Status { get; set; } = "en attente";  // Default status is "en attente"

        // New property to store the date (current date)
        public DateTime Date { get; set; } = DateTime.Now;  // Default date is the current date
    }
}
