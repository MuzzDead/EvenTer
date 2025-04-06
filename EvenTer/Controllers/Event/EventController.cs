using EvenTer.BLL.DTO.Event;
using EvenTer.BLL.Interfaces.Event.IServices;
using EvenTer.BLL.Services.Event;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EvenTer.WebAPI.Controllers.Event;

[Route("api/[controller]")]
[ApiController]
public class EventController : ControllerBase
{
	private readonly IEventService _service;
	public EventController(IEventService service)
	{
		_service = service;
	}

	[HttpPost]
	public async Task<IActionResult> AddEvent([FromBody] EventDTO eventDTO)
	{
		if (eventDTO == null)
		{
			return BadRequest();
		}

		await _service.AddEvent(eventDTO);
		return Ok("Event added successful!");
	}

	[HttpDelete("{eventId:guid}")]
	public async Task<IActionResult> DeleteEvent([FromRoute] Guid eventId)
	{
		await _service.DeleteEvent(eventId);
		return NoContent();
	}

	[HttpPut("{eventId:guid}")]
	public async Task<IActionResult> UpdateEvent([FromRoute] Guid eventId, [FromBody] EventDTO eventDTO)
	{
		await _service.UpdateEvent(eventId, eventDTO);
		return Ok("Event updated successful!");
	}

	[HttpGet("{eventId:guid}")]
	public async Task<IActionResult> GetEvent([FromRoute] Guid eventId)
	{
		return Ok(await _service.GetEvent(eventId));
	}

	[HttpGet]
	public async Task<IActionResult> GetEvents()
	{
		return Ok(await _service.GetEvents());
	}

	[HttpGet("{categoryId:int}")]
	public async Task<IActionResult> GetEventsByCategory([FromRoute] int categoryId)
	{
		if (categoryId == null || categoryId <= 0)
		{
			return BadRequest();
		}

		var events = await _service.GetEventByCategory(categoryId);
		if (events == null)
		{
			return NotFound("No events in this category were found");
		}

		return Ok(events);
	}

	[HttpGet("search")]
	public async Task<ActionResult> SearchEventsByTitle([FromQuery] string title)
	{
		if (string.IsNullOrWhiteSpace(title))
			return BadRequest("Title cannot be empty.");

		var events = await _service.GetEventByTitle(title);

		if (events == null || !events.Any())
			return NotFound($"No events found with title containing '{title}'.");

		return Ok(events);
	}
}