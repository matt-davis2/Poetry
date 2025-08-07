using MongoDB.Bson;
using MongoDB.Driver;
using PoetryApp.Components;
using PoetryApp.DataAccess;
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

var settings = MongoClientSettings.FromConnectionString(connectionString);
// Set the ServerApi field of the settings object to set the version of the Stable API on the client
settings.ServerApi = new ServerApi(ServerApiVersion.V1);
// Create a new client and connect to the server
var client = new MongoClient(settings);
// Send a ping to confirm a successful connection
try {
    var result = client.GetDatabase("admin").RunCommand<BsonDocument>(new BsonDocument("ping", 1));
    Console.WriteLine("Pinged your deployment. You successfully connected to MongoDB!");
} catch (Exception ex) {
    Console.WriteLine(ex);
}

builder.Services.AddSingleton<IMongoClient>(serviceProvider =>
{
    return new MongoClient(connectionString);
});

builder.Services.AddScoped(serviceProvider =>
{
    var client = serviceProvider.GetRequiredService<IMongoClient>();
    var databaseName = "PoetryAppDb"; // Replace with your actual database name
    return client.GetDatabase(databaseName);
});


// Add HttpClient for internal API calls
builder.Services.AddHttpClient<WeatherService>(client =>
{
    // This will be set to the same host as the app
    client.BaseAddress = new Uri("http://localhost:5171/"); // Adjust port as needed
});

// builder.Services.AddHttpClient<PoemService>(client =>
// {
//     // This will be set to the same host as the app
//     client.BaseAddress = new Uri("http://localhost:5171/"); // Adjust port as needed
// });

// Add our services
//builder.Services.AddScoped<WeatherService>();
builder.Services.AddScoped<IPoemRepository, PoemRepository>();
builder.Services.AddScoped<PoemService>();

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