using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using wardrobe.Application.Repositories;
using wardrobe.Domain.Entities.Common;
using wardrobe.Persistence.Contexts;

namespace wardrobe.Persistence.Repositories
{
    public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity
    {
        private readonly WardrobeDBContext _context;
        public ReadRepository(WardrobeDBContext context)
        {
            _context = context;
        }

        public DbSet<T> Table => _context.Set<T>();//IQquarylbe oldugundan dolayı

        public IQueryable<T> GetAll(bool tracking = true)
        {

            var query = Table.AsQueryable();
            if (!tracking)
            {
                query = query.AsNoTracking();
            }
            return query;
        }

        public IQueryable<T> GetWhere(Expression<Func<T, bool>> method, bool tracking = true)
        {

            var query = Table.Where(method);
            if (!tracking)
            {
                query = query.AsNoTracking();
            }
            return query;
        }
        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> method, bool tracking = true)
        {
            // return Table.SingleAsync(method);

            var query = Table.AsQueryable();
            if (!tracking)
                query.AsNoTracking();

            return await query.FirstOrDefaultAsync(method);
        }


        public async Task<T> GetByIdAsync(string id, bool tracking = true)// pattern olack
        {
            // return await Table.FirstOrDefaultAsync(p => p.Id == Guid.Parse(id)); // reflection yapmaktansa baseentity yaptık

            var query = Table.AsQueryable();
            if (!tracking)
                query.AsNoTracking();

            return await query.FirstOrDefaultAsync(p => p.Id == Guid.Parse(id));
        }
    }
}
