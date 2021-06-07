using CleanArch.Demo.Domain.Interfaces;
using CleanArch.Demo.Domain.Models;
using CleanArch.Demo.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Demo.Infra.Data.Repository.Product
{
   public class ProductRepository : EfRepository<Domain.Models.Product, Guid>, IProductRepository
    {

        public ProductRepository(UniversityDBContext universityDBContext) : base(universityDBContext)
        { }

        public async Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync()
        {
            return await _universityDBContext.ProductBrands.ToListAsync();
        }

        public async Task<Domain.Models.Product> GetProductByIdAsync(Guid id)
        {
            return await _universityDBContext.Products
                .Include(p => p.ProductType)
                .Include(p => p.ProductBrand)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IReadOnlyList<Domain.Models.Product>> GetProductsAsync()
        {
            return await _universityDBContext.Products
                .Include(p => p.ProductType)
                .Include(p => p.ProductBrand)
                .ToListAsync();
        }

        public async Task<IReadOnlyList<ProductType>> GetProductTypesAsync()
        {
            return await _universityDBContext.ProductTypes.ToListAsync();
        }

        Task<IReadOnlyList<ProductBrand>> IProductRepository.GetProductBrandsAsync()
        {
            throw new NotImplementedException();
        }

        Task<Domain.Models.Product> IProductRepository.GetProductByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        Task<IReadOnlyList<Domain.Models.Product>> IProductRepository.GetProductsAsync()
        {
            throw new NotImplementedException();
        }

        Task<IReadOnlyList<ProductType>> IProductRepository.GetProductTypesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
