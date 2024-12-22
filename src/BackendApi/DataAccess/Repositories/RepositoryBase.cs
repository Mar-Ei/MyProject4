using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected readonly DbContext _context;

        public RepositoryBase(DbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context)); // Бд при создании не может быть null
        }

        public async Task<IEnumerable<T>> FindAll()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<IEnumerable<T>> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return await _context.Set<T>().Where(expression).ToListAsync();
        }

        public async Task Create(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public Task Update(T entity) // Не нуждается в await
        {
            _context.Set<T>().Update(entity);
            return Task.CompletedTask;
        }

        public Task Delete(T entity) // Не нуждается в await
        {
            _context.Set<T>().Remove(entity);
            return Task.CompletedTask;
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
