﻿using VentaControlEstampillas.Domain.Entities;
using System.Linq.Expressions;

namespace VentaControlEstampillas.Infrastructure.Ports
{
    public interface IRepository<T> where T : DomainEntity
    {
        Task<T> GetOneAsync(Guid id);

        Task<IEnumerable<T>> GetManyAsync();
        Task<IEnumerable<T>> GetManyAsync(Expression<Func<T, bool>> filter);
        Task<IEnumerable<T>> GetManyAsync(Expression<Func<T, bool>> filter, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy);
        Task<IEnumerable<T>> GetManyAsync(Expression<Func<T, bool>> filter, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy, string includeStringProperties);
        Task<IEnumerable<T>> GetManyAsync(Expression<Func<T, bool>> filter, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy, string includeStringProperties, bool isTracking);

        Task<T> AddAsync(T entity);
        
        void Update(T entity);
        void Delete(T entity);
    }
}
