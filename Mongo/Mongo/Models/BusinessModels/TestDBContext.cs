using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using Mongo.Models.DataModels;
using Microsoft.Extensions.Configuration;

namespace Mongo.Models.BusinessModels
{
    public class TestDBContext
    {
        IConfiguration Configuration;

        public TestDBContext()
        {
        }

        public TestDBContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IMongoDatabase MongoDatabase
        {
            get
            {
                var client = new MongoClient("mongodb://localhost:27017");
                var database = client.GetDatabase("test");
                return database;
            }
        }
        public IMongoCollection<Department> Departments => MongoDatabase.GetCollection<Department>("departments");
        public IMongoCollection<Area> Areas => MongoDatabase.GetCollection<Area>("areas");
        public IMongoCollection<Position> Positions => MongoDatabase.GetCollection<Position>("positions");
        public IMongoCollection<Employee> Employees => MongoDatabase.GetCollection<Employee>("employees");
        public IMongoCollection<Account> Accounts => MongoDatabase.GetCollection<Account>("accounts");
        public IMongoCollection<Logined> Logined => MongoDatabase.GetCollection<Logined>("logined");

    }
}
