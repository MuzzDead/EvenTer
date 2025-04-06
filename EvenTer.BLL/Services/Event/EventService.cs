using EvenTer.BLL.DTO.Event;
using EvenTer.BLL.Interfaces.Event.IRepositories;
using EvenTer.BLL.Interfaces.Event.IServices;
using EvenTer.DAL.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventEntity = EvenTer.DAL.Entities.Events.Event;

namespace EvenTer.BLL.Services.Event;

public class EventService : IEventService
{
	private readonly IEventRepository _repository;
	private readonly EvenTerDbContext _context;
	public EventService(IEventRepository repository, EvenTerDbContext context)
	{
		_repository = repository;
		_context = context;
	}


	public async Task AddEvent(EventDTO eventDTO)
	{
		if (eventDTO.OrganizerId == Guid.Empty)
			throw new ArgumentException("OrganizerId must be valid", nameof(eventDTO));

		if (eventDTO.CategoryId <= 0)
			throw new ArgumentException("CategoryId must be valid", nameof(eventDTO));

		await _repository.AddEventAsync(await MapEntity(eventDTO));
	}

	public async Task DeleteEvent(Guid eventId)
	{
		await _repository.RemoveEventAsync(eventId);
	}

	public async Task<EventEntity> GetEvent(Guid eventId)
	{
		var events = await _repository.GetEventById(eventId);
		if (events == null)
		{
			throw new ArgumentNullException(nameof(eventId), "Event data is null!");
		}

		return events;
	}

	public async Task<IEnumerable<EventEntity>> GetEvents()
	{
		return await _repository.GetAllEventsAsync();
	}

	private async Task<EventEntity> MapEntity(EventDTO eventDTO)
	{
		if (eventDTO == null)
			throw new ArgumentNullException(nameof(eventDTO), "Event data is null!");

		var category = await _context.EventsCategory.FindAsync(eventDTO.CategoryId)
			?? throw new ArgumentException($"Category with ID {eventDTO.CategoryId} not found.");

		var organizer = await _context.Users
			.FirstOrDefaultAsync(u => u.Id == eventDTO.OrganizerId)
			?? throw new ArgumentException($"Organizer with ID {eventDTO.OrganizerId} not found.");

		if (!DateOnly.TryParse(eventDTO.Date.ToString(), out var eventDate))
			throw new ArgumentException("Invalid Date format.");

		return new EventEntity
		{
			Image = eventDTO.Image,
			EventName = eventDTO.EventName,
			Description = eventDTO.Description,
			Date = eventDate,
			StartTime = eventDTO.StartTime,
			EndTime = eventDTO.EndTime ?? null,
			Price = eventDTO.Price,
			Location = eventDTO.Location,
			MapUrl = eventDTO.MapUrl,
			Capacity = eventDTO.Capacity,
			CategoryId = eventDTO.CategoryId,
			Category = category,
			OrganizerId = eventDTO.OrganizerId,
			Organizer = organizer
		};
	}


	public async Task UpdateEvent(Guid eventId, EventDTO eventDTO)
	{
		await _repository.UpdateEventAsync(eventId, await MapEntity(eventDTO));
	}

	public async Task<IEnumerable<EventEntity>> GetEventByCategory(int categoryId)
	{
		var events = await _repository.GetEventsByCategoryAsync(categoryId);
		if (events == null) { throw new Exception("No events in this category were found!"); }

		return events;
	}

	public async Task<IEnumerable<EventEntity>> GetEventByTitle(string title)
	{
		var events = await _repository.GetEventsByTitle(title);
		if (events == null) { throw new Exception("No events with this title were found!"); }

		return events;
	}
}
