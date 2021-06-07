using CleanArch.Demo.Domain.Interfaces;
using CleanArch.Demo.Infra.Core.Interfaces;
using CleanArch.Demo.Infra.Core.Specifications;
using CleanArch.Demo.Infra.Data.Context;
using CleanArch.Demo.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Demo.Infra.Data.Repository
{
   public class EfRepository<T, TId> : IAsyncRepository<T, TId> where T : BaseEntity<TId>
    {
        protected readonly UniversityDBContext _universityDBContext;

        public EfRepository(UniversityDBContext universityDBContext)
        {
            _universityDBContext = universityDBContext;
        }
        public async Task<T> AddAsync(T entity)
        {
            await _universityDBContext.Set<T>().AddAsync(entity);
            await _universityDBContext.SaveChangesAsync();
            return entity;
        }

        public async Task<T> GetByIdAsync(TId id)
        {
            return await _universityDBContext.Set<T>().FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> GetAll()
        {
            return await _universityDBContext.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            
            await _universityDBContext.SaveChangesAsync();
        }
        public async  Task DeleteAsync(T entity)
        {
            _universityDBContext.Set<T>().Remove(entity);
            await _universityDBContext.SaveChangesAsync();
        }
        public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec)
        {
            var query = ApplySpecification(spec);
            return await query.ToListAsync();
        }
        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(_universityDBContext.Set<T>().AsQueryable(), spec);
        }

        
    }
}
