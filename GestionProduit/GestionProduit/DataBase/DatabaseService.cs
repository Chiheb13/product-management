using System;
using System.IO;
using SQLite;
using GestionProduit.Models;
using System.Threading.Tasks;

namespace GestionProduit.Services
{
    public class DatabaseService
    {
        private readonly SQLiteAsyncConnection _database;

        public DatabaseService()
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "users.db3");
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<User>().Wait();
        }

        public Task<int> AddUserAsync(User user)
        {
            return _database.InsertAsync(user);
        }

        public Task<User> GetUserAsync(string email, string password)
        {
            return _database.Table<User>()
                .Where(u => u.Email == email && u.Password == password)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> UserExistsAsync(string email)
        {
            int count = await _database.Table<User>()
                                       .Where(u => u.Email == email)
                                       .CountAsync();
            return count > 0;
        }

    }
}
