using CleanArch.Application.DTOs;
using CleanArch.Application.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArch.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	public class ProductsController : ControllerBase
	{
		private readonly IProductService _productSerive;
		public ProductsController(IProductService productSerive)
		{
			_productSerive = productSerive;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<ProductDTO>>> List()
		{
			var products = await _productSerive.GetProducts();
			if (products == null)
			{
				return NotFound("Products not found");
			}
			return Ok(products);
		}

		[HttpGet]
		[Route("{id}", Name = "/GetProduct")]
		public async Task<ActionResult<ProductDTO>> Get(int id)
		{
			var product = await _productSerive.GetById(id);
			if (product == null)
			{
				return NotFound("Product not found");
			}
			return Ok(product);
		}

		[HttpPost]
		public async Task<ActionResult> Create([FromBody] ProductDTO productDTO)
		{
			if (productDTO == null)
			{
				return BadRequest("Invalid data");
			}
			await _productSerive.Add(productDTO);

			return new CreatedAtRouteResult("/GetProduct", new { id = productDTO.Id }, productDTO);
		}

		[HttpPut]
		public async Task<ActionResult> Update(int id, [FromBody] ProductDTO productDTO)
		{
			if (id != productDTO.Id)
			{
				return BadRequest();
			}

			if (productDTO == null)
			{
				return BadRequest();
			}

			await _productSerive.Update(productDTO);

			return Ok(productDTO);
		}

		[HttpDelete("{id:int}")]
		public async Task<ActionResult<ProductDTO>> Delete(int id)
		{
			var product = await _productSerive.GetById(id);
			if (product == null)
			{
				return NotFound("Product not found");
			}
			await _productSerive.Remove(id);
			return Ok(product);
		}
	}
}
