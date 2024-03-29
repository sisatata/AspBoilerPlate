﻿using CleanArch.Demo.Infra.Core.Specifications;
using CleanArch.Demo.Shared;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Demo.Infra.Core.Interfaces
{
    public interface IAsyncRepository<T, TId> where T : BaseEntity<TId>
    {
        Task<T> GetByIdAsync(TId id);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<int> CountAsync(ISpecification<T> spec);
        Task<IList<T>> GetAll();
    
       
      //  Task<T> GetEntityWithSpec(ISpecification<T> spec);
        Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);
      //  Task<int> CountAsync(ISpecification<T> spec);
       

    }
}
