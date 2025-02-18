using Carter;
using Marten;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCarter();
builder.Services.AddMediatR(config => 
    config.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.AddMarten(opts =>
{
opts.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions();

var app = builder.Build();

// Setup HTTP Pipeline.
app.MapCarter();

app.Run();