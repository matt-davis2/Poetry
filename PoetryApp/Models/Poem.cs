using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PoetryApp.Models;

public class Poem
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = string.Empty;
    
    [BsonElement("title")]
    public string Title { get; set; } = string.Empty;
    
    [BsonElement("author")]
    public string Author { get; set; } = string.Empty;
    
    [BsonElement("content")]
    public string Content { get; set; } = string.Empty;
    
    [BsonElement("dateCreated")]
    public DateTime DateCreated { get; set; } = DateTime.Now;
}