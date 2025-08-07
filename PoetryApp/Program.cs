using PoetryApp.Components;
using PoetryApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Add API services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add HttpClient for internal API calls
builder.Services.AddHttpClient<WeatherService>(client =>
{
    // This will be set to the same host as the app
    client.BaseAddress = new Uri("https://localhost:5171/"); // Adjust port as needed
});

builder.Services.AddHttpClient<PoemService>(client =>
{
    // This will be set to the same host as the app
    client.BaseAddress = new Uri("https://localhost:5171/"); // Adjust port as needed
});

// Add our services
//builder.Services.AddScoped<WeatherService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}
else
{
    // Enable Swagger in development
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAntiforgery();

// Map API controllers
app.MapControllers();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();