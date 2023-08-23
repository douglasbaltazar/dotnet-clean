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
	public class CategoriesController : Controller
	{
		private readonly ICategoryService _categoryService;
		public CategoriesController(ICategoryService categoryService)
		{
			_categoryService = categoryService;
		}

		[HttpGet]
		public async Task<IActionResult> GetCategories()
		{
			var categories = await _categoryService.GetCategories();
			return Json(categories);
		}

		[HttpPost]
		[Route("new")]
		public async Task<IActionResult> CreateCategory([FromBody] CategoryDTO data)
		{
			await _categoryService.Add(data);
			return Json(data);
		}

		[HttpDelete]
		[Route("{id}")]
		public async Task<IActionResult> DeleteCategory(int id)
		{
			var data = await _categoryService.GetById(id);
			await _categoryService.Remove(id);
			return Json(data);
		}

		[HttpPut]
		[Route("{id}")]
		public async Task<IActionResult> UpdateCategory(int id, [FromBody] CategoryDTO data)
		{
			data.Id = id;
			await _categoryService.Update(data);
			return Json(data);
		}
	}
}
