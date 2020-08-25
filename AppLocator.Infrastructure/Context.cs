using AppLocator.Infrastructure.Entities;
using MongoDB.Driver;

namespace AppLocator.Infrastructure
{
    public class Context
    {
        private readonly MongoClient mongoClient;
        private readonly IMongoDatabase database;

        public Context(string connectionString, string databaseName)
        {
            mongoClient = new MongoClient(connectionString);
            database = mongoClient.GetDatabase(databaseName);
        }

        public IMongoCollection<ApplicationDocument> Application
        {
            get
            {
                return database.GetCollection<ApplicationDocument>("Application");
            }
        }
    }
}
