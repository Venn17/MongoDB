using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using Microsoft.Extensions.Configuration;
using Mongo.Models.DataModels;

namespace Mongo.Models.BusinessModels
{
    public class TestDBContext
    {
        public IConfiguration Configuration;

        public TestDBContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IMongoDatabase MongoDatabase
        {
            get
            {
                var client = new MongoClient(Configuration.GetConnectionString("MongoConnection"));
                var database = client.GetDatabase(Configuration.GetConnectionString("database"));
                return database;
            }
        }
        public IMongoCollection<Department> Departments => MongoDatabase.GetCollection<Department>("departments");
        public IMongoCollection<Area> Areas => MongoDatabase.GetCollection<Area>("areas");
        public IMongoCollection<Position> Positions => MongoDatabase.GetCollection<Position>("positions");
        public IMongoCollection<Employee> Employees => MongoDatabase.GetCollection<Employee>("employees");

    }
}
