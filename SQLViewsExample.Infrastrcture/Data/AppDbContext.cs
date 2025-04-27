using Microsoft.EntityFrameworkCore;
using SQLViewsExample.Domain.Entities;
using SQLViewsExample.Domain.ViewModels;

namespace SQLViewsExample.Infrastrcture.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<CustomerOrderSummary> CustomerOrderSummaries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomerOrderSummary>(entity =>
            {
                entity.HasNoKey();
                entity.ToView("vw_CustomerOrderSummary");
            });

            modelBuilder.Entity<Customer>().HasData(
            new Customer { Id = 1, Name = "John Smith", Email = "john.smith@example.com", RegistrationDate = new DateTime(2023, 1, 15) },
            new Customer { Id = 2, Name = "Emily Johnson", Email = "emily.j@example.com", RegistrationDate = new DateTime(2023, 2, 20) },
            new Customer { Id = 3, Name = "Michael Brown", Email = "michael.b@example.com", RegistrationDate = new DateTime(2023, 3, 10) },
            new Customer { Id = 4, Name = "Sarah Davis", Email = "sarah.d@example.com", RegistrationDate = new DateTime(2023, 4, 5) },
            new Customer { Id = 5, Name = "Robert Wilson", Email = "robert.w@example.com", RegistrationDate = new DateTime(2023, 5, 12) },
            new Customer { Id = 6, Name = "Olivia Martinez", Email = "olivia.m@example.com", RegistrationDate = new DateTime(2023, 6, 1) },
new Customer { Id = 7, Name = "William Anderson", Email = "william.a@example.com", RegistrationDate = new DateTime(2023, 6, 3) },
new Customer { Id = 8, Name = "Sophia Thomas", Email = "sophia.t@example.com", RegistrationDate = new DateTime(2023, 6, 8) },
new Customer { Id = 9, Name = "James Taylor", Email = "james.t@example.com", RegistrationDate = new DateTime(2023, 6, 12) },
new Customer { Id = 10, Name = "Isabella Moore", Email = "isabella.m@example.com", RegistrationDate = new DateTime(2023, 6, 18) }
            );

            modelBuilder.Entity<Order>().HasData(
               new Order { Id = 25, CustomerId = 8, OrderDate = new DateTime(2023, 6, 25), TotalAmount = 199.90m, Status = "Pending" },
new Order { Id = 26, CustomerId = 9, OrderDate = new DateTime(2023, 6, 22), TotalAmount = 332.10m, Status = "Completed" },
new Order { Id = 27, CustomerId = 10, OrderDate = new DateTime(2023, 6, 30), TotalAmount = 289.30m, Status = "Completed" },
new Order { Id = 28, CustomerId = 3, OrderDate = new DateTime(2023, 6, 18), TotalAmount = 122.75m, Status = "Completed" },
new Order { Id = 29, CustomerId = 5, OrderDate = new DateTime(2023, 6, 27), TotalAmount = 185.00m, Status = "Completed" },
new Order { Id = 30, CustomerId = 6, OrderDate = new DateTime(2023, 7, 1), TotalAmount = 205.80m, Status = "Pending" },
new Order { Id = 31, CustomerId = 2, OrderDate = new DateTime(2023, 6, 29), TotalAmount = 412.60m, Status = "Completed" },
new Order { Id = 32, CustomerId = 7, OrderDate = new DateTime(2023, 6, 14), TotalAmount = 333.33m, Status = "Cancelled" },
new Order { Id = 33, CustomerId = 1, OrderDate = new DateTime(2023, 6, 16), TotalAmount = 178.22m, Status = "Completed" },
new Order { Id = 34, CustomerId = 10, OrderDate = new DateTime(2023, 7, 3), TotalAmount = 267.85m, Status = "Pending" },
new Order { Id = 35, CustomerId = 4, OrderDate = new DateTime(2023, 6, 28), TotalAmount = 354.10m, Status = "Completed" },
new Order { Id = 36, CustomerId = 2, OrderDate = new DateTime(2023, 6, 23), TotalAmount = 221.45m, Status = "Completed" },
new Order { Id = 37, CustomerId = 6, OrderDate = new DateTime(2023, 6, 12), TotalAmount = 108.70m, Status = "Completed" },
new Order { Id = 38, CustomerId = 8, OrderDate = new DateTime(2023, 6, 19), TotalAmount = 159.60m, Status = "Completed" },
new Order { Id = 39, CustomerId = 3, OrderDate = new DateTime(2023, 7, 2), TotalAmount = 490.00m, Status = "Pending" },
new Order { Id = 40, CustomerId = 9, OrderDate = new DateTime(2023, 6, 20), TotalAmount = 139.99m, Status = "Cancelled" },
new Order { Id = 41, CustomerId = 1, OrderDate = new DateTime(2023, 6, 30), TotalAmount = 325.55m, Status = "Completed" },
new Order { Id = 42, CustomerId = 7, OrderDate = new DateTime(2023, 6, 17), TotalAmount = 245.60m, Status = "Completed" },
new Order { Id = 43, CustomerId = 10, OrderDate = new DateTime(2023, 7, 4), TotalAmount = 312.00m, Status = "Pending" },
new Order { Id = 44, CustomerId = 5, OrderDate = new DateTime(2023, 6, 15), TotalAmount = 164.25m, Status = "Completed" },
new Order { Id = 45, CustomerId = 6, OrderDate = new DateTime(2023, 6, 21), TotalAmount = 199.99m, Status = "Completed" },
new Order { Id = 46, CustomerId = 3, OrderDate = new DateTime(2023, 6, 9), TotalAmount = 279.95m, Status = "Cancelled" },
new Order { Id = 47, CustomerId = 8, OrderDate = new DateTime(2023, 7, 5), TotalAmount = 388.88m, Status = "Completed" },
new Order { Id = 48, CustomerId = 2, OrderDate = new DateTime(2023, 6, 13), TotalAmount = 105.45m, Status = "Completed" },
new Order { Id = 49, CustomerId = 4, OrderDate = new DateTime(2023, 6, 26), TotalAmount = 244.75m, Status = "Pending" },
new Order { Id = 50, CustomerId = 7, OrderDate = new DateTime(2023, 6, 24), TotalAmount = 390.10m, Status = "Completed" }


            );
        }

        public void EnsureDatabaseCreated()
        {
            Database.EnsureCreated();
            EnsureViewsCreated();
        }

        private void EnsureViewsCreated()
        {
            var viewSql = @"
        IF NOT EXISTS (SELECT * FROM sys.views WHERE name = 'vw_CustomerOrderSummary')
        EXEC('CREATE VIEW vw_CustomerOrderSummary AS SELECT 1 AS dummy');
        
        EXEC('
        ALTER VIEW vw_CustomerOrderSummary AS
        SELECT 
            c.Id AS CustomerId,
            c.Name AS CustomerName,
            c.Email,
            COUNT(o.Id) AS OrderCount,
            SUM(o.TotalAmount) AS TotalSpent,
            MAX(o.OrderDate) AS LastOrderDate
        FROM Customers c
        LEFT JOIN Orders o ON c.Id = o.CustomerId
        GROUP BY c.Id, c.Name, c.Email
        ')";

            Database.ExecuteSqlRaw(viewSql);
        }
    }
}
