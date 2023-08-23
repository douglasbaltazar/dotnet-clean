﻿using CleanArch.Application.Interfaces;
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
	}
}
