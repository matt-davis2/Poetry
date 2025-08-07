namespace PoetryApp.DataAccess;

public interface IPoemRepository
{
    Task<List<T>> GetAllAsync<T>(string collectionName);
    Task<T> GetByIdAsync<T>(string collectionName, string id);
    Task CreateAsync<T>(string collectionName, T item);
    Task UpdateAsync<T>(string collectionName, string id, T item);
    Task DeleteAsync(string collectionName, string id);
}
