using CleanArch.Application.DTOs;
using CleanArch.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArch.WebUI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductsController : Controller
	{
		private readonly IProductService _productService;
		public ProductsController(IProductService productService)
		{
			_productService = productService;
		}
		[HttpGet]
		public async Task<IActionResult> GetProducts()
		{
			var products = await _productService.GetProducts();
			return Json(products);
		}

		[HttpPost]
		[Route("new")]
		public async Task<IActionResult> CreateProduct([FromBody] ProductDTO data)
		{
			await _productService.Add(data);
			return Json(data);
		}

		[HttpPut]
		[Route("{id}")]
		public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductDTO data)
		{
			data.Id = id;
			await _productService.Update(data);
			return Json(data);
		}

		[HttpDelete]
		[Route("{id}")]
		public async Task<IActionResult> DeleteProduct(int id)
		{
			var data = await _productService.GetById(id);
			await _productService.Remove(id);
			return Json(data);
		}
	}
}
