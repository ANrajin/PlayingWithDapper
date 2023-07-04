using Dapper.Data.Entities;
using System.Diagnostics;

namespace Dapper.Console;

public static class Test
{
    public static void GettAll_Should_Return_6_Result()
    {
        //Arrange
        var repository = AppConfiguration.CreateRepository();

        //Act
        var contacts = repository.GetAll();

        // assert
        System.Console.WriteLine($"Count: {contacts.Count}");
        Debug.Assert(contacts.Count == 6);
        contacts.Output();
    }

    public static int Create_Should_Assign_Identity_ToNewEntity()
    {
        //Arrange
        var repository = AppConfiguration.CreateRepository();

        var contact = new Contact
        {
            FullName = "John Snow",
            Email = "snow@email.com",
            Company = "FakeBook",
            Title = "CEO"
        };

        //Act
        repository.Create(contact);

        //Assert
        Debug.Assert(contact.Id != 0);
        System.Console.WriteLine("Contact Inserted!");
        System.Console.WriteLine($"New Id = {contact.Id}");
        return contact.Id;
    }

    public static int Create_Should_Insert_Addresses_And_Assign_Identity_ToNewEntity()
    {
        //Arrange
        var repository = AppConfiguration.CreateRepository();

        var contact = new Contact
        {
            FullName = "Sansa Stark",
            Email = "sansa@email.com",
            Company = "FakeBook",
            Title = "COO"
        };

        var address = new Address
        {
            AddressType = "Home",
            StreetAddress = "Green Road, Dhaka",
            City = "Dhaka",
            PostalCode = "1207",
            StateId = 8
        };

        contact.Addresses.Add(address);

        //Act
        repository.Create(contact);

        //Assert
        Debug.Assert(contact.Id != 0);
        System.Console.WriteLine("Contact Inserted!");
        System.Console.WriteLine($"New Id = {contact.Id}");
        contact.Output();
        return contact.Id;
    }

    public static void Find_Should_Retrive_Existing_Entity(int id)
    {
        //Arrange
        var repository = AppConfiguration.CreateRepository();

        //Act
        var contact = repository.Find(id);

        //Assert
        System.Console.WriteLine("*** Get Contact ***");
        contact.Output();
        Debug.Assert(contact.FullName == "Jane");
        Debug.Assert(contact.Email == "jane@email.com");
    }

    public static void Find_SP_Should_Retrive_Existing_Entity(int id)
    {
        //Arrange
        var repository = AppConfiguration.CreateRepository();

        //Act
        var contact = repository.Find(id);

        //Assert
        System.Console.WriteLine("*** Get Contact Using Stored Proccedure ***");
        contact.Output();
    }

    public static void Update_Should_Modify_Existing_Contact(int id)
    {
        //Arrange
        var repository = AppConfiguration.CreateRepository();

        //Act
        var contact = repository.Find(id);
        contact.FullName = "Test Name Updated";
        contact.Email = "test_updated@email.com";
        repository.Update(contact);

        //Assert
        System.Console.WriteLine("*** Contact Modified! ***");
        var modifiedContact = AppConfiguration.CreateRepository().Find(id);
        modifiedContact.Output();
        Debug.Assert(modifiedContact.FullName == "Test Name Updated");
        Debug.Assert(modifiedContact.Email == "test_updated@email.com");
    }

    public static void Delete_Should_Remove_A_Contact(int id)
    {
        //Arrange
        var repository = AppConfiguration.CreateRepository();

        //Act
        repository.Delete(id);

        //Assert
        var newRepo = AppConfiguration.CreateRepository();
        var contact = newRepo.Find(id);

        Debug.Assert(contact == null);
        System.Console.WriteLine("***Contact Deleted***");
    }

    public static void GetFullContact_Should_Retrive_FullContact_Info(int id)
    {
        //Arrange
        var repository = AppConfiguration.CreateRepository();

        //Act
        var contact = repository.GetFullContact(id);

        //Assert
        System.Console.WriteLine("*** Get Contact With Addresses! ***");
        contact.Output();
    }

    public static void GetFullContact_SP_Should_Retrive_FullContact_Info(int id)
    {
        //Arrange
        var repository = AppConfiguration.CreateRepository();

        //Act
        var contact = repository.GetFullContact(id);

        //Assert
        System.Console.WriteLine("*** Get Contact With Addresses Using Stored Proccedure! ***");
        contact.Output();
    }

    public static int Create_SP_Should_Insert_Addresses_And_Assign_Identity_ToNewEntity()
    {
        //Arrange
        var repository = AppConfiguration.CreateRepository();

        var contact = new Contact
        {
            FullName = "Super Man",
            Email = "superman@email.com",
            Company = "FakeBook",
            Title = "Developer"
        };

        var address = new Address
        {
            AddressType = "Home",
            StreetAddress = "Green Road, Dhaka",
            City = "Dhaka",
            PostalCode = "1207",
            StateId = 8
        };

        contact.Addresses.Add(address);

        //Act
        repository.Create(contact);

        //Assert
        Debug.Assert(contact.Id != 0);
        System.Console.WriteLine("Contact Inserted Using Stored Procedure");
        System.Console.WriteLine($"New Id = {contact.Id}");
        contact.Output();
        return contact.Id;
    }

    public static void Delete_SP_Should_Remove_A_Contact(int id)
    {
        //Arrange
        var repository = AppConfiguration.CreateRepository();

        //Act
        repository.Delete(id);

        //Assert
        var newRepo = AppConfiguration.CreateRepository();
        var contact = newRepo.Find(id);

        Debug.Assert(contact == null);
        System.Console.WriteLine("***Contact Deleted Using Stored Procedure***");
    }

    public static void GetContactsById_Should_Return_List()
    {
        //Arrange
        var repository = AppConfiguration.ContactRepositoryEx();

        //Act
        var contacts = repository.GetContactsById(1, 2, 5);

        //Assert
        contacts.Output();
    }

    public static void GetDynamicContactById_Should_Return_List()
    {
        //Arrange
        var repository = AppConfiguration.ContactRepositoryEx();

        //Act
        var contacts = repository.GetDynamicContactsById(1, 2, 5);

        //Assert
        contacts.Output();
    }

    public static void BulkInsert_Should_Save_Multiple_Contacts()
    {
        //Arrange
        var repository = AppConfiguration.ContactRepositoryEx();

        var contacts = new List<Contact>
        {
            new Contact{ FullName = "Contact One", Email="contact.one@gmail.com", Company = "Company One", Title = "CFO"},
            new Contact{ FullName = "Contact Two", Email="contact.two@gmail.com", Company = "Company Two", Title = "CFO"},
            new Contact{FullName = "Contact Three", Email = "contact.three@gmail.com", Company = "Company Three", Title = "CFO"},
            new Contact{FullName = "Contact Four", Email = "contact.four@gmail.com", Company = "Company Four", Title = "CFO"},

        };

        //Act
        var rowsAffected = repository.BulkInsert(contacts);

        //Assert
        System.Console.WriteLine($"Rows Inserted = {rowsAffected}");
        Debug.Assert(rowsAffected == contacts.Count);
    }
}