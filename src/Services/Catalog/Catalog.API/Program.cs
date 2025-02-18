using BuildingBlocks.Behaviours;
using Carter;
using FluentValidation;
using Marten;

var builder = WebApplication.CreateBuilder(args);

var assembly = typeof(Program).Assembly;
// Add services to the container.

builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehaviour<,>));
});
builder.Services.AddValidatorsFromAssembly(assembly); //add all validators that you can find in Program.cs assembly 
//basically registering all classes that implement IVal
builder.Services.AddMarten(opts =>
{
opts.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions();

var app = builder.Build();

// Setup HTTP Pipeline.
app.MapCarter();

app.Run();