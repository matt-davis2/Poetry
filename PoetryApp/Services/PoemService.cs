// PoetryApp/Services/PoemApiService.cs
using PoetryApp.Controllers;
using PoetryApp.DataAccess;
using PoetryApp.Models;

namespace PoetryApp.Services;

public class PoemService(IPoemRepository poemRepository, ILogger<PoemService> logger)
{
    private const string CollectionName = "Poems";
    public async Task<List<Poem>> GetAllPoemsAsync()
    {
        try
        {
            var response = await poemRepository.GetAllAsync<Poem>(CollectionName);
            return response;
        }
        catch (HttpRequestException ex)
        {
            logger.LogError(ex, "Error fetching poems from API");
            return null;
        }
    }

    public async Task<Poem?> GetPoemByIdAsync(string id)
    {
        try
        {
            var response = await poemRepository.GetByIdAsync<Poem>(CollectionName, id);
            return response;
        }
        catch (HttpRequestException ex)
        {
            logger.LogError(ex, "Error fetching poem from API");
            return null;
        }
    }

    public async Task CreatePoemAsync(Poem poem)
    {
        try
        {
            await poemRepository.CreateAsync(CollectionName, poem);
        }
        catch (HttpRequestException ex)
        {
            logger.LogError(ex, "Error creating poem via API");
        }
    }
}