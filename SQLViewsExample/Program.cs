using Microsoft.EntityFrameworkCore;
using SQLViewsExample.Application.Interfaces;
using SQLViewsExample.Application.Repositories;
using SQLViewsExample.Application.Services;
using SQLViewsExample.Domain.Entities;
using SQLViewsExample.Infrastrcture.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register repositories
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

// Register services
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped(typeof(ViewRepository<>));
builder.Services.AddScoped<CustomerViewService>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Initialize database
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.EnsureDatabaseCreated();

    // Seed test data if needed
    if (!dbContext.Customers.Any())
    {
        dbContext.Customers.Add(new Customer { Name = "John Doe", Email = "john@example.com" });
        dbContext.SaveChanges();
    }
}

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
