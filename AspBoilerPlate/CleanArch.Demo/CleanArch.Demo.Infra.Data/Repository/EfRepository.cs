using CleanArch.Demo.Domain.Interfaces;
using CleanArch.Demo.Infra.Core.Interfaces;
using CleanArch.Demo.Infra.Data.Context;
using CleanArch.Demo.Shared;
using System;
using System.Collections.Generic;
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

       

        public Task UpdateAsync(T entity)
        {
            throw new NotImplementedException();
        }


    }
}
