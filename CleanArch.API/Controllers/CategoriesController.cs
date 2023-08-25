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
	public class CategoriesController : ControllerBase
	{
		private readonly ICategoryService _categorySerive;
		public CategoriesController(ICategoryService categorySerive)
		{
			_categorySerive = categorySerive;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<CategoryDTO>>> List()
		{
			var categories = await _categorySerive.GetCategories();
			if(categories == null)
			{
				return NotFound("Categories not found");
			}
			return Ok(categories);
		}
		
		[HttpGet]
		[Route("{id}", Name = "/GetCategory")]
		public async Task<ActionResult<CategoryDTO>> Get(int id)
		{
			var category = await _categorySerive.GetById(id);
			if(category == null)
			{
				return NotFound("Category not found");
			}
			return Ok(category);
		}

		[HttpPost]
		public async Task<ActionResult> Create([FromBody] CategoryDTO categoryDto)
		{
			if(categoryDto == null)
			{
				return BadRequest("Invalid data");
			}
			await _categorySerive.Add(categoryDto);

			return new CreatedAtRouteResult("/GetCategory", new { id = categoryDto.Id }, categoryDto);
		}

		[HttpPut]
		public async Task<ActionResult> Update(int id, [FromBody] CategoryDTO categoryDto)
		{
			if(id != categoryDto.Id)
			{
				return BadRequest();
			}

			if (categoryDto == null)
			{
				return BadRequest();
			}

			await _categorySerive.Update(categoryDto);

			return Ok(categoryDto);
		}

		[HttpDelete("{id:int}")]
		public async Task<ActionResult<CategoryDTO>> Delete(int id)
		{
			var category = await _categorySerive.GetById(id);
			if (category == null)
			{
				return NotFound("Category not found");
			}
			await _categorySerive.Remove(id);
			return Ok(category);
		}
	}
}
