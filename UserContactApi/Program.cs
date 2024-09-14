using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using UserContactProject.Interfaces;
using UserContactsApi.Data;
using UserContactsApi.Dtos;
using UserContactsApi.Endpoints;
using UserContactsApi.Interfaces;
using UserContactsApi.Models;
using UserContactsApi.Repositories;
using UserContactsApi.Services;
using UserContactsApi.Validators;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("_myAllowSpecificOrigins",
                          policy =>
                          {
                              policy.AllowAnyOrigin()
                                    .AllowAnyHeader()
                                    .AllowAnyMethod();
                          });
});

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "UserContacts API", Version = "v1" });
}); // This line requires the Microsoft.OpenApi.Models namespace

// Register the in-memory database
builder.Services.AddDbContext<UserContactsDbContext>(options => options.UseInMemoryDatabase("items"));
builder.Services.AddScoped<IValidator<ContactDto>, ContactValidator>();
builder.Services.AddScoped<IUserContactRepository, UserContactRepository>();
builder.Services.AddScoped<IUserContactService, UserContactService>();

var app = builder.Build();

// Ensure the database is created and seeded
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<UserContactsDbContext>();
    if (!dbContext.Contacts.Any())
    {
        dbContext.Contacts.AddRange(
            new Contact { FirstName = "John", LastName = "Doe", Email = "john@example.com", Phone = "9999997890", Created = DateTime.Now },
            new Contact { FirstName = "Jane", LastName = "Doe", Email = "jane@example.com", Phone = "1111114321", Created = DateTime.Now }
        );
        dbContext.SaveChanges();
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("_myAllowSpecificOrigins");

// Map endpoints for the contact operations
app.MapContactEndpoints();

app.Run();

public partial class Program
{
}
