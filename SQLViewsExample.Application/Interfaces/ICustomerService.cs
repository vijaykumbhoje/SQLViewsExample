using SQLViewsExample.Domain.Entities;
using SQLViewsExample.Domain.ViewModels;

namespace SQLViewsExample.Application.Interfaces
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerOrderSummary>> GetCustomerSummariesAsync();
        Task<CustomerOrderSummary> GetCustomerSummaryAsync(int customerId);
        Task AddCustomerAsync(Customer customer);
        Task AddOrderAsync(Order order);
    }
}
