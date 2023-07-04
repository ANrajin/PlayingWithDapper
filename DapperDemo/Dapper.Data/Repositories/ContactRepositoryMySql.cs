using Dapper.Data.Entities;
using MySql.Data.MySqlClient;
using System.Data;

namespace Dapper.Data.Repositories
{
    public class ContactRepositoryMySql
    {
        private readonly IDbConnection _dbConnection;

        public ContactRepositoryMySql(string connectionString)
        {
            _dbConnection = new MySqlConnection(connectionString);
        }

        public async Task<IList<Contact>> GetAllAsync()
        {
            var contacts = await _dbConnection.QueryAsync<Contact>("Select * from contacts");
            return contacts.ToList();
        }
    }
}
