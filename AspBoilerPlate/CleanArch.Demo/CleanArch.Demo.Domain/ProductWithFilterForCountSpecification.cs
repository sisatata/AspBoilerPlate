using CleanArch.Demo.Domain.Models;
using CleanArch.Demo.Infra.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArch.Demo.Domain
{
   public class ProductWithFilterForCountSpecification : BaseSpecifcation<Product>
    {
        public ProductWithFilterForCountSpecification(ProductSpecParams productParams)
            : base(x =>
            (String.IsNullOrEmpty(productParams.Search) || x.Name.ToLower().Contains(productParams.Search)) &&
            (!productParams.BrandId.HasValue || x.ProductBrandId == productParams.BrandId) &&
            (!productParams.TypeId.HasValue || x.ProductTypeId == productParams.TypeId)
        )
        {

        }
    }
}
