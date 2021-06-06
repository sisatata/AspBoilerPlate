using CleanArch.Demo.Shared;
using CleanArch.Demo.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CleanArch.Demo.Domain.Models
{
   public class ProductType : BaseEntity<Guid>, IAuditable
    {
       
        public string Name { get; set; }
        public Product Product { get; set; }
    }
}
