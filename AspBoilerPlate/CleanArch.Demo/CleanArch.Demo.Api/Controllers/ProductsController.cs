using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArch.Demo.Api.Controllers
{
    public class ProductsController : BaseController<ProductsController>
    {
        public ProductsController()
        {

        }

      
        [HttpGet("brands")]
        public async Task<IActionResult> GetBrands()
        {
            var brands = await _mediator.Send(new Application.Queries.ProductQuery.GetAllBrandQuery());
            return Ok(brands);
        }
    }
}
