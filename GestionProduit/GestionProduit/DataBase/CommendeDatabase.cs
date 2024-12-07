using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GestionProduit.Models;
using SQLite;

namespace GestionProduit.DataBase
{
    public class CommendeDatabase
    {
        private readonly SQLiteAsyncConnection _database;

        public CommendeDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Commende>().Wait(); // Crée la table "Commende" si elle n'existe pas
        }

        // Méthode pour insérer une commande
        public Task<int> SaveCommendeAsync(Commende commende)
        {
            if (commende.ID != 0)
            {
                return _database.UpdateAsync(commende); // Mise à jour si l'ID existe
            }
            else
            {
                return _database.InsertAsync(commende); // Insertion sinon
            }
        }

        // Méthode pour obtenir toutes les commandes
        public Task<List<Commende>> GetAllCommendesAsync()
        {
            return _database.Table<Commende>().ToListAsync();
        }

        // Méthode pour supprimer une commande
        public Task<int> DeleteCommendeAsync(Commende commende)
        {
            return _database.DeleteAsync(commende);
        }

        // Méthode pour obtenir une commande par ID
        public Task<Commende> GetCommendeByIdAsync(int id)
        {
            return _database.Table<Commende>().Where(c => c.ID == id).FirstOrDefaultAsync();
        }
    }
}
