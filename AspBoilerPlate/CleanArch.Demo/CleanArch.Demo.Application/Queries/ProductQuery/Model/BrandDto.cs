using AutoMapper;
using CleanArch.Demo.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArch.Demo.Application.Queries.ProductQuery.Model
{
    [AutoMap(typeof(ProductBrand))]
    public class BrandDto
    {
        public string Name { get; set; }
    }
}
