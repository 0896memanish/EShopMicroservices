var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var app = builder.Build();

// Setup HTTP Pipeline.

app.Run();