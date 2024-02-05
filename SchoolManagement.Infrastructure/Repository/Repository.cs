using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Interface.Repository;
using SchoolManagement.Domain.Common;
using SchoolManagement.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Infrastructure.Repository
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<T> _set;
        public Repository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _set = dbContext.Set<T>();
        }


        public async Task<T?> FindAsync(Guid id, params Expression<Func<T, object>>[] includes)
        {
            T? entity;
            if (includes?.Length > 0)
            {
                var set = _set.AsQueryable();
                foreach (var include in includes)
                    set = set.Include(include);

                entity = await set.FirstOrDefaultAsync(x => x.Id == id);
            }
            else
            {
                entity = await _set.FindAsync(id);
            }
            return entity;
        }
        public async Task<T?> FirstOrDefaultAsync(params Expression<Func<T, object>>[] includes)
        {
            T? entity;
            var set = _set.AsQueryable();
            if (includes?.Length > 0)
            {
                foreach (var include in includes)
                    set = set.Include(include);
            }
            entity = await set.FirstOrDefaultAsync();
            return entity;
        }

        public async Task<T?> FisrtOrDefaultAsync(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes)
        {
            T? entity;
            var set = _set.AsQueryable();
            if (includes?.Length > 0)
            {
                foreach (var include in includes)
                    set = set.Include(include);
            }
            entity = await set.FirstOrDefaultAsync(expression);
            return entity;
        }

        public async Task<IEnumerable<T>?> GetListAsync(params Expression<Func<T, object>>[] includes)
        {
            IEnumerable<T>? entities;
            var set = _set.AsQueryable();
            if (includes?.Length > 0)
            {
                foreach (var include in includes)
                    set = set.Include(include);
            }
            entities = await set.ToListAsync();
            return entities;
        }

        public async Task<IEnumerable<T>?> GetListAsync(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes)
        {
            IEnumerable<T>? entities;
            var set = _set.AsQueryable();
            if (includes?.Length > 0)
            {
                foreach (var include in includes)
                    set = set.Include(include);
            }
            entities = await set.Where(expression).ToListAsync();
            return entities;
        }
        public async Task<Guid> CreateAsync(T model)
        {
            var x = await _dbContext.AddAsync(model);
            await _dbContext.SaveChangesAsync();
            return x.Entity.Id;
        }
        public async Task<Guid> UpdateAsync(T model)
        {
            _dbContext.Entry(model).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return model.Id;
        }
        public async Task SaveChangedAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Guid> DeleteAsync(Guid id)
        {
            T? entity = await FindAsync(id);
            if (entity is not null)
            {
                entity.DeleteName = "deleted";
                _dbContext.Entry(entity).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
                return id;
            }
            return Guid.Empty;
        }

        public async Task<Guid> ToggleActiveAsync(Guid id)
        {
            T? entity = await _set.AsTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (entity is not null)
            {
                entity.IsActive = !entity.IsActive;
                await _dbContext.SaveChangesAsync();
                return id;
            }
            throw new NotFoundException(string.Format("{0} List Not Found.", typeof(T).Name));
        }
    }
}
