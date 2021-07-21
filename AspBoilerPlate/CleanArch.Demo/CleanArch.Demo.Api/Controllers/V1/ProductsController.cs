using CleanArch.Demo.Application.ViewModels;
using CleanArch.Demo.Domain;
using CleanArch.Demo.Infra.Core.Specifications;
using CleanArch.Demo.Shared;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArch.Demo.Api.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ProductsController : BaseController<ProductsController>
    {
        public ProductsController()
        {

        }

        [HttpGet("products")]
        public async Task<ActionResult> GetProducts(
             [FromQuery] ProductSpecParams productParams)
        {
            var products = await _mediator.Send(new Application.Queries.ProductQuery.GetAllProductWithPaginationQueryV1{ ProductParams = productParams});
            return Ok(products);
            
            
        }
        [HttpGet("brands")]
        public async Task<IActionResult> GetBrands()
        {
            var brands = await _mediator.Send(new Application.Queries.ProductQuery.GetAllBrandQueryV1());
            return Ok(brands);
        }

        [HttpGet("product-types")]
        public async Task<IActionResult> GetProductTypes()
        {
            var types = await _mediator.Send(new Application.Queries.ProductTypeQuery.GetAllTypeQueryV1());
            return Ok(new { success = true, data= types});
        }
    }
}
