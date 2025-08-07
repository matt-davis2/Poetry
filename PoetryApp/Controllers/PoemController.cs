// PoetryApp/Controllers/PoemController.cs
using Microsoft.AspNetCore.Mvc;
using PoetryApp.Models;

namespace PoetryApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PoemController : ControllerBase
{
    private static readonly List<Poem> Poems = new()
    {
        new Poem { Id = 1, Title = "The Road Not Taken", Author = "Robert Frost", Content = "Two roads diverged in a yellow wood..." },
        new Poem { Id = 2, Title = "Hope", Author = "Emily Dickinson", Content = "Hope is the thing with feathers..." },
        new Poem { Id = 3, Title = "If", Author = "Rudyard Kipling", Content = "If you can keep your head when all about you..." },
        new Poem { Id = 4, Title = "The Raven", Author = "Edgar Allan Poe", Content = "Once upon a midnight dreary..." },
        new Poem { Id = 5, Title = "Stopping by Woods", Author = "Robert Frost", Content = "Whose woods these are I think I know..." }
    };

    [HttpGet]
    public async Task<IEnumerable<Poem>> GetAll()
    {
        await Task.Delay(200); // Simulate API delay
        return Poems;
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Poem>> GetById(int id)
    {
        await Task.Delay(100);
        var poem = Poems.FirstOrDefault(p => p.Id == id);
        return poem == null ? NotFound() : Ok(poem);
    }

    [HttpPost]
    public async Task<ActionResult<Poem>> Create(CreatePoemRequest request)
    {
        await Task.Delay(150);
        
        var poem = new Poem
        {
            Id = Poems.Max(p => p.Id) + 1,
            Title = request.Title,
            Author = request.Author,
            Content = request.Content
        };
        
        Poems.Add(poem);
        return CreatedAtAction(nameof(GetById), new { id = poem.Id }, poem);
    }
}