using AutoMapper;
using CleanArch.Demo.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArch.Demo.Application.Queries.ProductTypeQuery.Model
{
    [AutoMap(typeof(ProductType))]
    public class TypeDto
    {
        public string Name { get; set; }
    }
}
