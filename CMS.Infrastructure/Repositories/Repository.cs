using CMS.Core.Repositories;
using CMS.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        public CMSDbContext _cmsDbContext { get; }
        DbSet<T> _dbSet;
        public Repository(CMSDbContext cmsDbContext)
        {
            _cmsDbContext = cmsDbContext;
            _dbSet = _cmsDbContext.Set<T>();
        }


        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> Get(Expression<Func<T, bool>> filters)
        {
            return await _dbSet.Where(filters).FirstOrDefaultAsync();
        }

        public Task<IEnumerable<T>> Add(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
