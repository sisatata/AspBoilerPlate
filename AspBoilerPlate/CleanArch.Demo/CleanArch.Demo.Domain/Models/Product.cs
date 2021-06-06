using CleanArch.Demo.Shared;
using CleanArch.Demo.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArch.Demo.Domain.Models
{
   public class Product : BaseEntity<Guid>, IAuditable
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string PictureUrl { get; set; }
        public ICollection<ProductType> ProductType { get; set; }
      //  public ProductType ProductType { get; set; }
        public int ProductTypeId { get; set; }
     //   public ProductBrand ProductBrand { get; set; }

        public ICollection<ProductBrand> ProductBrand { get; set; }
        public int ProductBrandId { get; set; }

    }
}
