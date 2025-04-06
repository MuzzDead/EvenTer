﻿using EvenTer.BLL.DTO.Event;
using EvenTer.BLL.Interfaces.Event.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EvenTer.WebAPI.Controllers.Event
{
	[Route("api/[controller]")]
	[ApiController]
	public class EventCategoryController : ControllerBase
	{
		private readonly IEventCategoryService _service;
		public EventCategoryController(IEventCategoryService service)
		{
			_service = service;
		}

		[HttpPost]
		public async Task<IActionResult> CreateCategory([FromBody] EventCategoryDTO categoryDTO)
		{
			if (categoryDTO == null)
			{
				return BadRequest();
			}

			await _service.CreateCategory(categoryDTO);
			return Ok("Category added successful!");
		}

		[HttpGet]
		public async Task<IActionResult> GetCategories()
		{
			return Ok(await _service.GetAllCategories());
		}

		[HttpGet("{categoryId:int}")]
		public async Task<IActionResult> GetCategory([FromRoute] int categoryId)
		{
			var category = await _service.GetCategory(categoryId);
			if (category == null)
			{
				return NotFound();
			}

			return Ok(category);
		}

		[HttpPut("{categoryId:int}")]
		public async Task<IActionResult> UpdateCategories([FromRoute] int categoryId, [FromBody] EventCategoryDTO categoryDTO)
		{
			if (categoryDTO == null || categoryId == null)
			{
				return BadRequest();
			}

			await _service.UpdateCategory(categoryId, categoryDTO);
			return Ok("Category updated successful!");
		}

		[HttpDelete("{categoryId:int}")]
		public async Task<IActionResult> DeleteCategory([FromRoute] int categoryId)
		{
			await _service.DeleteCategory(categoryId);
			return Ok("Category deleted successful!");
		}
	}
}
