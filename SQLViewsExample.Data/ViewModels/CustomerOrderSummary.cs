using Microsoft.EntityFrameworkCore;

namespace SQLViewsExample.Domain.ViewModels
{
    [Keyless]
    public class CustomerOrderSummary
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string Email { get; set; }
        public int OrderCount { get; set; }
        public decimal TotalSpent { get; set; }
        public DateTime? LastOrderDate { get; set; }
    }
}
