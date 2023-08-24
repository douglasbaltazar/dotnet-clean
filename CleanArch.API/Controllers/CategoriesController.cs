using CleanArch.Application.DTOs;
using CleanArch.Application.Interfaces;
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
	}
}
