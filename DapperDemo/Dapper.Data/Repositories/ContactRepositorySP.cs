using Dapper.Data.Entities;
using System.Data.SqlClient;
using System.Data;
using System.Transactions;

namespace Dapper.Data.Repositories
{
    public class ContactRepositorySP : IContactRepository
    {
        private readonly IDbConnection _dbConnection;

        public ContactRepositorySP(string connectionString)
        {
            _dbConnection = new SqlConnection(connectionString);
        }

        public Contact Create(Contact contact)
        {
            using var transactionScope = new TransactionScope();

            var parameters = new DynamicParameters();
            parameters.Add("@Id", value: contact.Id, dbType: DbType.Int32, ParameterDirection.InputOutput);
            parameters.Add("@FullName", value: contact.FullName);
            parameters.Add("@Email", value: contact.Email);
            parameters.Add("@Company", value: contact.Company);
            parameters.Add("@Title", value: contact.Title);

            _dbConnection.Execute("SaveContact", parameters, commandType: CommandType.StoredProcedure);
            contact.Id = parameters.Get<int>("@Id");

            foreach(var address in contact.Addresses.Where(c => !c.IsDeleted))
            {
                address.ContactId = contact.Id;
                Create(address);
            }

            transactionScope.Complete();

            return contact;
        }

        public Address Create(Address address)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Id", value: address.Id, dbType: DbType.Int32, ParameterDirection.InputOutput);
            parameters.Add("@ContactId", value: address.ContactId);
            parameters.Add("@AddressType", value: address.AddressType);
            parameters.Add("@StreetAddress", value: address.StreetAddress);
            parameters.Add("@City", value: address.City);
            parameters.Add("@StateId", value: address.StateId);
            parameters.Add("@PostalCode", value: address.PostalCode);

            _dbConnection.Execute("SaveAddress", parameters, commandType: CommandType.StoredProcedure);
            address.Id = parameters.Get<int>("@Id");

            return address;
        }

        public void Delete(int id)
        {
            _dbConnection.Execute("DeleteContact", new {Id = id}, commandType: CommandType.StoredProcedure);
        }

        public Contact? Find(int id)
        {
            return _dbConnection.Query<Contact>("GetContact", new { Id = id }, commandType: CommandType.StoredProcedure)
                .SingleOrDefault();
        }

        public IList<Contact> GetAll()
        {
            throw new NotImplementedException();
        }

        public Contact GetFullContact(int id)
        {
            using var multipleResult = _dbConnection
                .QueryMultiple("GetContact", new { Id = id }, commandType: CommandType.StoredProcedure);
            var contact = multipleResult.ReadFirstOrDefault<Contact>();
            var addresses = multipleResult.Read<Address>().ToList();
            if (contact is not null && addresses is not null)
                contact.Addresses.AddRange(addresses);
            return contact!;
        }

        public Contact Update(Contact contact)
        {
            throw new NotImplementedException();
        }
    }
}
