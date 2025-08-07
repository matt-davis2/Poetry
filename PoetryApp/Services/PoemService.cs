// PoetryApp/Services/PoemApiService.cs
using PoetryApp.Controllers;

namespace PoetryApp.Services;

public class PoemService(HttpClient httpClient, ILogger<PoemService> logger)
{
    public async Task<Poem[]?> GetAllPoemsAsync()
    {
        try
        {
            var response = await httpClient.GetFromJsonAsync<Poem[]>("api/poem");
            return response;
        }
        catch (HttpRequestException ex)
        {
            logger.LogError(ex, "Error fetching poems from API");
            return null;
        }
    }

    public async Task<Poem?> GetPoemByIdAsync(int id)
    {
        try
        {
            var response = await httpClient.GetFromJsonAsync<Poem>($"api/poem/{id}");
            return response;
        }
        catch (HttpRequestException ex)
        {
            logger.LogError(ex, "Error fetching poem from API");
            return null;
        }
    }

    public async Task<Poem?> CreatePoemAsync(CreatePoemRequest request)
    {
        try
        {
            var response = await httpClient.PostAsJsonAsync("api/poem", request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Poem>();
        }
        catch (HttpRequestException ex)
        {
            logger.LogError(ex, "Error creating poem via API");
            return null;
        }
    }
}