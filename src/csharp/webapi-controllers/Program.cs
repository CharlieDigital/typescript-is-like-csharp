var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<AppService>();
// builder.Services.SetupAppServices();
builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();
app.Run();
