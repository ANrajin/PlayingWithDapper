using Dapper.Data.Entities;

namespace Dapper.Data.Repositories
{
    public interface IContactRepository
    {
        Contact Find(int id);

        IList<Contact> GetAll();

        Contact Create(Contact contact);

        Contact Update(Contact contact);

        void Delete(int id);

        Contact GetFullContact(int id);
    }
}
