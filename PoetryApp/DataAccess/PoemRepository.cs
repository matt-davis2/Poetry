using MongoDB.Bson;
using MongoDB.Driver;

namespace PoetryApp.DataAccess;

public class PoemRepository(IMongoDatabase database) : IPoemRepository
{
    public async Task<List<T>> GetAllAsync<T>(string collectionName)
    {
        var collection = database.GetCollection<T>(collectionName);
        return await collection.Find(_ => true).ToListAsync();
    }

    public async Task<T> GetByIdAsync<T>(string collectionName, string id)
    {
        var collection = database.GetCollection<T>(collectionName);
        var filter = Builders<T>.Filter.Eq("_id", ObjectId.Parse(id));
        return await collection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task CreateAsync<T>(string collectionName, T item)
    {
        var collection = database.GetCollection<T>(collectionName);
        await collection.InsertOneAsync(item);
    }

    public async Task UpdateAsync<T>(string collectionName, string id, T item)
    {
        var collection = database.GetCollection<T>(collectionName);
        var filter = Builders<T>.Filter.Eq("_id", ObjectId.Parse(id));
        await collection.ReplaceOneAsync(filter, item);
    }

    public async Task DeleteAsync(string collectionName, string id)
    {
        var collection = database.GetCollection<BsonDocument>(collectionName);
        var filter = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(id));
        await collection.DeleteOneAsync(filter);
    }

}