using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using GestionProduit.Models;

namespace GestionProduit.DataBase
{
    public class ProduitDatabase
    {
        readonly SQLiteAsyncConnection _database;

        public ProduitDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
              // Drop the existing table (be cautious, this deletes all data)
            _database.CreateTableAsync<Produit>().Wait(); // Recreate the table with the updated schema
        }


        public Task<List<Produit>> GetProduitAsync()
        {
            return _database.Table<Produit>().ToListAsync();
        }

        public Task<Produit> GetPrduitAsync(int id)
        {
            return _database.Table<Produit>()
                            .Where(i => i.ID == id)
                            .FirstOrDefaultAsync();
        }

        public Task<int> SaveProduitAsync(Produit produit)
        {
            if (produit.ID != 0)
            {
                return _database.UpdateAsync(produit);
            }
            else
            {
                return _database.InsertAsync(produit);
            }
        }

        public Task<int> DeleteProduitAsync(Produit produit)
        {
            return _database.DeleteAsync(produit);
        }
    }
}
