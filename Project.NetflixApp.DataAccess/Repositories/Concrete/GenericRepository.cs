using Microsoft.EntityFrameworkCore;
using Project.NetflixApp.DataAccess.Contexts.EntityFramework;
using Project.NetflixApp.DataAccess.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Project.NetflixApp.DataAccess.Repositories.Concrete
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly NetflixAppContext _netflixAppContext;

        public GenericRepository(NetflixAppContext netflixAppContext)
        {
            _netflixAppContext = netflixAppContext;
        }

        public async Task DeleteAsync(T entity)
        {
            _netflixAppContext.Set<T>().Remove(entity);
            await _netflixAppContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
           return await _netflixAppContext.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllFilterAsync(Expression<Func<T, bool>> filter)
        {
            return await _netflixAppContext.Set<T>().Where(filter).AsNoTracking().ToListAsync();
        }

        public async Task<T> GetByFilterAsync(Expression<Func<T, bool>> filter)
        {
            return await _netflixAppContext.Set<T>().Where(filter).SingleOrDefaultAsync(filter);
        }
        public async Task<T> AsNoTrackingGetByFilterAsync(Expression<Func<T, bool>> filter)
        {
            return await _netflixAppContext.Set<T>().Where(filter).AsNoTracking().SingleOrDefaultAsync(filter);
        }
        public async Task<T> GetByIdAsync(int id)
        {
            return await _netflixAppContext.Set<T>().FindAsync(id);
        }

        public IQueryable<T> GetQuery()
        {
            return _netflixAppContext.Set<T>().AsQueryable();
        }

        public async Task InsertAsync(T entity)
        {
            await _netflixAppContext.Set<T>().AddAsync(entity);
            await _netflixAppContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            var addedEntity = _netflixAppContext.Entry(entity);
            addedEntity.State = EntityState.Modified;
            await _netflixAppContext.SaveChangesAsync();
        }
    }
}
