using EvenTer.BLL.DTO.Event;
using EvenTer.BLL.Interfaces.Event.IRepositories;
using EvenTer.BLL.Interfaces.Event.IServices;
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
	public EventService(IEventRepository repository)
	{
		_repository = repository;
	}


	public async Task AddEvent(EventDTO eventDTO)
	{
		await _repository.AddEventAsync(MapEntity(eventDTO));
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

	private EventEntity MapEntity(EventDTO eventDTO)
	{
		if (eventDTO == null)
			throw new ArgumentNullException(nameof(eventDTO), "Event data is null!");

		return new EventEntity
		{
			Image = eventDTO.Image,
			EventName = eventDTO.EventName,
			Description = eventDTO.Description,
			Date = eventDTO.Date,
			StartTime = eventDTO.StartTime,
			EndTime = eventDTO.EndTime,
			Price = eventDTO.Price,
			Location = eventDTO.Location,
			MapUrl = eventDTO.MapUrl,
			Capacity = eventDTO.Capacity,
			CategoryId = eventDTO.CategoryId
		};
	}

	public async Task UpdateEvent(Guid eventId, EventDTO eventDTO)
	{
		await _repository.UpdateEventAsync(eventId, MapEntity(eventDTO));
	}
}
