using EvenTer.BLL.DTO.Event;
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
		public async Task<IActionResult> AddEvent([FromBody] EventCategoryDTO categoryDTO)
		{
			if (categoryDTO == null)
			{
				return BadRequest();
			}

			await _service.CreateCategory(categoryDTO);
			return Ok("Event added successful!");
		}


		[HttpGet]
		public async Task<IActionResult> GetEvents()
		{
			return Ok(await _service.GetAllCategories());
		}
	}
}
