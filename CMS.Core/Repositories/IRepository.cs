using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Core.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<T> Get(Expression<Func<T, bool>> filters);
        Task<IEnumerable<T>> GetAllAsync();
        void Delete(T entity);
        T Add(T entity);
    }
}
