using EFExample.Data;

var builder = WebApplication.CreateBuilder(args);

var connectionString = "server=127.0.0.1;port=5432;database=ef;user id=postgres;password=postgres;include error detail=true;";

builder.Services.AddSingleton(new DbConfig(connectionString));
builder.Services.AddDbContext<Database>();
builder.Services.AddControllers();

var app = builder.Build();

// Set up the database
Console.WriteLine("✨ Provisioning database..."); // 👈 Start the database
using var scope = app.Services.CreateScope();
var db = scope.ServiceProvider.GetService<Database>()!;
db.Database.EnsureCreated();

app.MapControllers();
app.Run();
