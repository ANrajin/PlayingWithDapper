using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Transactions;
using Dapper.Data.Entities;

namespace Dapper.Data.Repositories
{
    public class ContactRepository : IContactRepository
    {
        private readonly IDbConnection _dbConnection;

        public ContactRepository(string connectionString)
        {
            _dbConnection = new SqlConnection(connectionString);
        }
        
        public Contact Find(int id)
        {
            return _dbConnection.Query<Contact>("Select * From Contacts Where Id = @Id", new { Id = id }).FirstOrDefault()!;
        }

        public IList<Contact> GetAll()
        {
            return _dbConnection.Query<Contact>("Select * from contacts").ToList();
        }

        public Contact Create(Contact contact)
        {
            var sql = new StringBuilder();

            using var transactionScope = new TransactionScope();

            sql.Append("Insert Into Contacts (FullName, Email, Company, Title) Values (@FullName, @Email, @Company, @Title)");
            sql.Append("Select CAST(SCOPE_IDENTITY() as int)");

            var id = _dbConnection.Query<int>(sql.ToString(), contact).Single();
            contact.Id = id;

            foreach(var address in contact.Addresses)
            {
                address.ContactId = contact.Id;
                Create(address);
            }

            transactionScope.Complete();

            return contact;
        }

        public Address Create(Address address)
        {
            var sql = new StringBuilder();
            sql.Append("Insert Into Addresses (ContactId, AddressType, StreetAddress, City, StateId, PostalCode) Values (@ContactId, @AddressType, @StreetAddress, @City, @StateId, @PostalCode)");
            sql.Append("Select CAST(SCOPE_IDENTITY() as int)");

            var id = _dbConnection.Query<int>(sql.ToString(), address).Single();
            address.Id = id;
            return address;
        }

        public Contact Update(Contact contact)
        {
            var sql = new StringBuilder();
            sql.Append("Update Contacts Set ");
            sql.Append("FullName = @FullName, Email = @Email, Company = @Company, Title = @Title ");
            sql.Append("Where Id = @Id");

            _dbConnection.Execute(sql.ToString(), contact);

            return contact;
        }

        public void Delete(int id)
        {
            _dbConnection.Execute("Delete From Contacts Where Id = @Id", new { id });
        }

        public Contact GetFullContact(int id)
        {
            var sql = new StringBuilder();
            sql.Append("Select * From Contacts Where Id = @Id; ");
            sql.Append("Select * From Addresses Where ContactId = @Id");

            using var multipleResults = _dbConnection.QueryMultiple(sql.ToString(), new { Id = id });
            var contact = multipleResults.Read<Contact>().FirstOrDefault();
            var addresses = multipleResults.Read<Address>().ToList();
            if (contact is not null && addresses is not null)
                contact.Addresses.AddRange(addresses);

            return contact!;
        }
    }
}
