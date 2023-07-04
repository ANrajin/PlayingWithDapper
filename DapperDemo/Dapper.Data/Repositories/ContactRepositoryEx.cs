using Dapper.Data.Entities;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Dapper.Data.Repositories
{
    public class ContactRepositoryEx
    {
        private readonly IDbConnection _dbConnection;

        public ContactRepositoryEx(string connectionString)
        {
            _dbConnection = new SqlConnection(connectionString);
        }

        public IList<Contact> GetContactsById(params int[] ids)
        {
            return _dbConnection.Query<Contact>("Select * from contacts where id in @Id", new {Id = ids}).ToList();
        }

        public IList<dynamic> GetDynamicContactsById(params int[] ids)
        {
            return _dbConnection.Query("Select * from contacts where id in @Id", new { Id = ids }).ToList();
        }

        public int BulkInsert(List<Contact> contacts)
        {
            var sql = new StringBuilder();
            sql.Append("Insert Into Contacts (FullName, Email, Company, Title) Values (@FullName, @Email, @Company, @Title)");
            sql.Append("Select CAST(SCOPE_IDENTITY() as int)");

            return _dbConnection.Execute(sql.ToString(), contacts);
        }

        public IList<Contact> GetContactsWithAddresses()
        {
            var sql = new StringBuilder();
            sql.Append("Select * from contacts as c Inner join addresses as a on a.ContactId = c.Id");

            var contactDicts = new Dictionary<int, Contact>();

            var contacts = _dbConnection.Query<Contact, Address, Contact>(sql.ToString(), (contact, address) =>
            {
                if(!contactDicts.TryGetValue(contact.Id, out var currentContact))
                {
                    currentContact = contact;
                    contactDicts.Add(contact.Id, currentContact);
                }

                currentContact.Addresses.Add(address);
                return currentContact;
            });

            return contacts.Distinct().ToList();
        }
    }
}
