using SchoolManagement.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Interface.Repository
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<T?> FindAsync(Guid id, params Expression<Func<T, object>>[] includes);
        Task<T?> FirstOrDefaultAsync(params Expression<Func<T, object>>[] includes);
        Task<T?> FisrtOrDefaultAsync(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes);
        Task<IEnumerable<T>?> GetListAsync(params Expression<Func<T, object>>[] includes);
        Task<IEnumerable<T>?> GetListAsync(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes);
        Task<Guid> CreateAsync(T model);
        Task<Guid> UpdateAsync(T model);
        Task<Guid> DeleteAsync(Guid id);
        Task<Guid> ToggleActiveAsync(Guid id);
        Task SaveChangedAsync();
    }
}
