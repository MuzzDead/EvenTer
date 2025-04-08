using EvenTer.BLL.DTO.Event;
using EvenTer.BLL.Interfaces.Event.IServices;
using EvenTer.BLL.Interfaces.User.IServices;
using EvenTer.BLL.Services.Event;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EvenTer.WebAPI.Controllers.Event;

[Route("api/[controller]")]
[ApiController]
public class EventController : ControllerBase
{
	private readonly IEventService _service;
	private readonly ICurrentUserService _currentUserService;
	public EventController(IEventService service, ICurrentUserService currentUserService)
	{
		_service = service;
		_currentUserService = currentUserService;
	}

	[Authorize]
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

	[Authorize]
	[HttpDelete("{eventId:guid}")]
	public async Task<IActionResult> DeleteEvent([FromRoute] Guid eventId)
	{
		if (await IsAuthor(eventId))
		{
			await _service.DeleteEvent(eventId);
			return NoContent();
		}

		return Forbid("You have not permission!");
	}

	[Authorize]
	[HttpPut("{eventId:guid}")]
	public async Task<IActionResult> UpdateEvent([FromRoute] Guid eventId, [FromBody] EventDTO eventDTO)
	{
		if (await IsAuthor(eventId))
		{
			await _service.UpdateEvent(eventId, eventDTO);
			return Ok("Event updated successful!");
		}

		return Forbid("You have not permission!");
	}


	[HttpGet("{eventId:guid}")]
	public async Task<IActionResult> GetEvent([FromRoute] Guid eventId)
	{
		return Ok(await _service.GetEvent(eventId));
	}

	[Authorize(Roles = "Admin")]
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

	private async Task<bool> IsAuthor(Guid eventId)
	{
		var userId = (await _service.GetEvent(eventId)).OrganizerId;
		var currentUserId = _currentUserService.GetUserId(User);
		if (currentUserId == userId)
		{
			return true;
		}

		return false;
	}
}