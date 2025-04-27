using Microsoft.EntityFrameworkCore;
using SQLViewsExample.Infrastrcture.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLViewsExample.Application.Repositories
{
    public class ViewRepository<TView> where TView : class
    {
        private readonly AppDbContext _context;

        public ViewRepository(AppDbContext context)
        {
            _context = context;
        }

        public IQueryable<TView> AsQueryable()
        {
            return _context.Set<TView>().AsNoTracking();
        }
    }
}
