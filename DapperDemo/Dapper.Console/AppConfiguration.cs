#nullable disable

using Dapper.Data.Repositories;
using Microsoft.Extensions.Configuration;

namespace Dapper.Console;

public static class AppConfiguration
{
    private static IConfigurationRoot _configurationRoot;
    public static void Initialize()
    {
        _configurationRoot = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json")
           .Build();
    }

    public static IContactRepository CreateRepository()
    {
        //return new ContactRepository(_configurationRoot.GetConnectionString("DefaultConnection"));
        //return new ContactRepositoryForDapperContrib(_configurationRoot.GetConnectionString("DefaultConnection"));
        return new ContactRepositorySP(_configurationRoot.GetConnectionString("DefaultConnection"));
    }

    public static ContactRepositoryEx ContactRepositoryEx()
    {
        return new ContactRepositoryEx(_configurationRoot.GetConnectionString("DefaultConnection"));
    }

    public static ContactRepositoryMySql ContactRepositoryMySql()
    {
        return new ContactRepositoryMySql(_configurationRoot.GetConnectionString("MySqlConnection"));
    }
}