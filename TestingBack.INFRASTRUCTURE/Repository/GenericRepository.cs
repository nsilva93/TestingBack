using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TestingBack.CORE.Interfaces;
using TestingBack.INFRASTRUCTURE.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TestingBack.INFRASTRUCTURE.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected ApplicationDbContext _context;
        protected DbSet<T> dbSet;
        protected readonly ILogger _logger;
        public GenericRepository(
            ApplicationDbContext context,
            ILogger logger)
        {
            _context = context;
            _logger = logger;
            dbSet = _context.Set<T>();
        }

        public async Task<bool> Add(T entity)
        {
            await dbSet.AddAsync(entity);
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            var t = await dbSet.FindAsync(id);

            if (t != null)
            {
                dbSet.Remove(t);
                return true;
            }
            else
                return false;
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> expression)
        {
            return dbSet.Where(expression);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await dbSet.ToListAsync();
        }

        public async Task<T?> GetById(int id)
        {
            return await dbSet.FindAsync(id);
        }

        public Task<bool> Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
