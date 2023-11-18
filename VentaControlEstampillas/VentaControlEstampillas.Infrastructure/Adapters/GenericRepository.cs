using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Linq;
using VentaControlEstampillas.Domain.Entities;
using VentaControlEstampillas.Infrastructure.DataSource;
using VentaControlEstampillas.Infrastructure.Ports;

namespace VentaControlEstampillas.Infrastructure.Adapters
{
    public class GenericRepository<T> : IRepository<T> where T : DomainEntity
    {
        
        private readonly DbSet<T> _dataset;

        public GenericRepository(DataContext context)
        {
            // Usamos una variable local para inicializar '_dataset'
            var localContext = context ?? throw new ArgumentNullException(nameof(context));
            _dataset = localContext.Set<T>();
        }

        public async Task<T> GetOneAsync(Guid id)
        {
            return await _dataset.FindAsync(id) ?? throw new ArgumentNullException(nameof(id));
        }

        public async Task<IEnumerable<T>> GetManyAsync()
        {
            return await _dataset.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetManyAsync(Expression<Func<T, bool>> filter)
        {
            return await _dataset.Where(filter).ToListAsync();
        }

        public async Task<IEnumerable<T>> GetManyAsync(Expression<Func<T, bool>> filter, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy)
        {
            var query = _dataset.Where(filter);
            return await orderBy(query).ToListAsync();
        }

        public async Task<IEnumerable<T>> GetManyAsync(Expression<Func<T, bool>> filter, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy, string includeStringProperties)
        {
            var query = _dataset.Where(filter).Include(includeStringProperties);
            return await orderBy(query).ToListAsync();
        }

        public async Task<IEnumerable<T>> GetManyAsync(Expression<Func<T, bool>> filter, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy, string includeStringProperties, bool isTracking)
        {
            var query = isTracking ? _dataset.Where(filter).Include(includeStringProperties) : _dataset.AsNoTracking().Where(filter).Include(includeStringProperties);
            return await orderBy(query).ToListAsync();
        }

        public async Task<T> AddAsync(T entity)
        {
            _ = entity ?? throw new ArgumentNullException(nameof(entity), "Entity can not be null");
            await _dataset.AddAsync(entity);
            return entity;
        }

        public void Delete(T entity) 
        {
            _ = entity ?? throw new ArgumentNullException(nameof(entity), "Entity can not be null");
            _dataset.Remove(entity);
        }

        public void Update(T entity)
        {
            _dataset.Update(entity);
        }
    }
}
