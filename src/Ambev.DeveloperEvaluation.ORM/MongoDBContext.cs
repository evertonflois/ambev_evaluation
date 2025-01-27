using Ambev.DeveloperEvaluation.Domain.Entities;

using Microsoft.Extensions.Configuration;

using MongoDB.Bson;
using MongoDB.Driver;

namespace Ambev.DeveloperEvaluation.ORM;

public class MongoDBContext
{
    private readonly IMongoDatabase _database;

    public MongoDBContext(IConfiguration configuration)
    {
        // Connect to MongoDB
        var client = new MongoClient(configuration["MongoDbSettings:ConnectionString"]);
        _database = client.GetDatabase(configuration["MongoDbSettings:DatabaseName"]);
    }

    public IMongoCollection<Order> Orders => _database.GetCollection<Order>("Orders");
    public IMongoCollection<BsonDocument> Counters => _database.GetCollection<BsonDocument>("Counters");

    // Get next sequence value for auto-increment
    public async Task<int> GetNextSequenceValue(string counterName)
    {
        var filter = Builders<BsonDocument>.Filter.Eq("_id", counterName);
        var update = Builders<BsonDocument>.Update.Inc("seq", 1);
        var options = new FindOneAndUpdateOptions<BsonDocument>
        {
            IsUpsert = true,  // Create the counter if it doesn't exist
            ReturnDocument = ReturnDocument.After  // Return the updated document
        };

        var result = await Counters.FindOneAndUpdateAsync(filter, update, options);
        return result["seq"].ToInt32();
    }


    public IMongoCollection<T> GetCollection<T>(string name)
    {
        return _database.GetCollection<T>(name);
    }
}
