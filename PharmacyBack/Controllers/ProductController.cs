using Microsoft.AspNetCore.Mvc;
using Pharmacy.Application.Services;
using Pharmacy.Core.Models;
using PharmacyBack.Contracts;
using System;
using System.Collections.Generic;

namespace PharmacyBack.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductsService _productsService;

        public ProductController(IProductsService productsService)
        {
            _productsService = productsService;
        }

        [HttpGet("main")]
        public async Task<ActionResult<List<ProductsResponse>>> RandomProducts()
        {
            var products = await _productsService.GetFourProducts();

            List < ProductsResponse > response = new List< ProductsResponse >();

            foreach (var prod in products)
            {
                response.Add(new ProductsResponse(prod.Name, prod.Price.ToString("F2"),
                        prod.ImageUrl, prod.Description, 
                        prod.Company?.CompanyName ?? "Невідомий виробник",
                        prod.Quantity > 0 ? "В наявності" : "Немає в наявності"));
            }

            return Ok(response);

        }

        [HttpGet("search")]
        public async Task<ActionResult<List<ProductsResponse>>> SearchProducts([FromQuery] string names)
        {
            var nameList = names.Split(',')
                .Select(n => n.Trim())
                .Where(n => !string.IsNullOrWhiteSpace(n))
                .ToList();

            var products = await _productsService.SearchProducts(nameList);

            var response = products.Select(prod => new ProductsResponse(
                prod.Name,
                prod.Price.ToString("F2"),
                prod.ImageUrl,
                prod.Description,
                prod.Company?.CompanyName ?? "Невідомий виробник",
                prod.Quantity > 0 ? "В наявності" : "Немає в наявності"
            )).ToList();

            return Ok(response);
        }
    }
}
