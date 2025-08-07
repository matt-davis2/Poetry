using MongoDB.Driver;
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

// Configure MongoDB
var connectionString = builder.Configuration.GetConnectionString("MongoDB");
builder.Services.AddSingleton<IMongoClient>(serviceProvider =>
{
    return new MongoClient(connectionString);
});

builder.Services.AddScoped(serviceProvider =>
{
    var client = serviceProvider.GetRequiredService<IMongoClient>();
    var databaseName = "your-database-name"; // Replace with your actual database name
    return client.GetDatabase(databaseName);
});


// Add HttpClient for internal API calls
builder.Services.AddHttpClient<WeatherService>(client =>
{
    // This will be set to the same host as the app
    client.BaseAddress = new Uri("http://localhost:5171/"); // Adjust port as needed
});

builder.Services.AddHttpClient<PoemService>(client =>
{
    // This will be set to the same host as the app
    client.BaseAddress = new Uri("http://localhost:5171/"); // Adjust port as needed
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