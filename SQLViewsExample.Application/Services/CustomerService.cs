using Microsoft.EntityFrameworkCore;
using SQLViewsExample.Application.Interfaces;
using SQLViewsExample.Domain.Entities;
using SQLViewsExample.Domain.ViewModels;
using SQLViewsExample.Infrastrcture.Data;

namespace SQLViewsExample.Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IRepository<Customer> _customerRepository;
        private readonly IRepository<Order> _orderRepository;
        private readonly AppDbContext _dbContext;

        public CustomerService(
            IRepository<Customer> customerRepository,
            IRepository<Order> orderRepository,
            AppDbContext dbContext)
        {
            _customerRepository = customerRepository;
            _orderRepository = orderRepository;
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<CustomerOrderSummary>> GetCustomerSummariesAsync()
        {
            return await _dbContext.CustomerOrderSummaries
                .OrderByDescending(c => c.TotalSpent)
                .ToListAsync();
        }

        public async Task<CustomerOrderSummary> GetCustomerSummaryAsync(int customerId)
        {
            return await _dbContext.CustomerOrderSummaries
                .FirstOrDefaultAsync(c => c.CustomerId == customerId);
        }

        public async Task AddCustomerAsync(Customer customer)
        {
            await _customerRepository.AddAsync(customer);
        }

        public async Task AddOrderAsync(Order order)
        {
            await _orderRepository.AddAsync(order);
        }
    }
}
