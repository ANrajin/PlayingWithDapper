using Dapper.Contrib.Extensions;
using Dapper.Data.Entities;
using System.Data;
using System.Data.SqlClient;

namespace Dapper.Data.Repositories
{
    public class ContactRepositoryForDapperContrib : IContactRepository
    {
        private readonly IDbConnection _dbConnection;

        public ContactRepositoryForDapperContrib(string connectionString)
        {
            _dbConnection = new SqlConnection(connectionString);
        }

        public Contact Create(Contact contact)
        {
            var id = _dbConnection.Insert(contact);
            contact.Id = (int)id;
            
            return contact;
        }

        public void Delete(int id)
        {
            _dbConnection.Delete(new Contact { Id = id});
        }

        public Contact Find(int id)
        {
            return _dbConnection.Get<Contact>(id);
        }

        public IList<Contact> GetAll()
        {
            return _dbConnection.GetAll<Contact>().ToList();
        }

        public Contact GetFullContact(int id)
        {
            throw new NotImplementedException();
        }

        public Contact Update(Contact contact)
        {
            _dbConnection.Update<Contact>(contact);
            return contact;
        }
    }
}
