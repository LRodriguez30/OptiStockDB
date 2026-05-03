using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace OptiStock.AppContext
{
    public class MongoDbContext
    {
        public IMongoDatabase Database { get; }

        public MongoDbContext(
            IMongoClient mongoClient,
            IOptions<MongoDBSettings> options)
        {
            var settings = options.Value;
            Database = mongoClient.GetDatabase(settings.DatabaseName);

            Console.WriteLine($"Servidor Mongo: {settings.ConnectionString}");
            Console.WriteLine($"Base de datos activa: {settings.DatabaseName}");
        }
    }
}
