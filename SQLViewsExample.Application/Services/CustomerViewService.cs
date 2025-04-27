using Microsoft.EntityFrameworkCore;
using SQLViewsExample.Application.Repositories;
using SQLViewsExample.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLViewsExample.Application.Services
{
    public class CustomerViewService
    {
        private readonly ViewRepository<CustomerOrderSummary> _viewRepo;

        public CustomerViewService(ViewRepository<CustomerOrderSummary> viewRepo)
        {
            _viewRepo = viewRepo;
        }

        public IQueryable<CustomerOrderSummary> GetQueryable()
        {
            return _viewRepo.AsQueryable();
        }

        public IQueryable<CustomerOrderSummary> GetHighValueCustomers(decimal minAmount)
        {
            return _viewRepo.AsQueryable()
                .Where(c => c.TotalSpent > minAmount)
                .OrderByDescending(c => c.TotalSpent);
        }

        public async Task<List<CustomerOrderSummary>> GetTopSpendersAsync(int count)
        {
            var a =  _viewRepo.AsQueryable()
                .OrderByDescending(c => c.TotalSpent);

            return await a.Take(count)
                .ToListAsync();
        }
    }
}
