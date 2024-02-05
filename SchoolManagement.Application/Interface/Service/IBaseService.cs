using SchoolManagement.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Interface.Service
{
    public interface IBaseService<T> where T : BaseEntity
    {
        Task<T> GetAsync(Guid id, params Expression<Func<T, object>>[] includes);
        //Task<T> GetAsync(params Expression<Func<T, object>>[] includes);
        Task<T> GetAsync(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes);
        Task<IEnumerable<T>> GetListAsync(params Expression<Func<T, object>>[] includes);
        Task<IEnumerable<T>> GetListAsync(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes);
        Task<Guid> CreateAsync(T model);
        Task<Guid> UpdateAsync(T model);
        Task<Guid> DeleteAsync(Guid id);
        Task<Guid> ToggleActive(Guid id);
    }
}
