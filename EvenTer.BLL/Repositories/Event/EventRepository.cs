﻿using EvenTer.BLL.Interfaces.Event.IRepositories;
using EvenTer.DAL.Entities.Events;
using EvenTer.DAL.Enums.Events;
using EvenTer.DAL.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventEntity = EvenTer.DAL.Entities.Events.Event;

namespace EvenTer.BLL.Repositories.Event;

public class EventRepository : IEventRepository
{
	private readonly EvenTerDbContext _context;
	public EventRepository(EvenTerDbContext context)
	{
		_context = context;
	}

	public async Task AddEventAsync(EventEntity events)
	{
		await _context.Events.AddAsync(events);
		await _context.SaveChangesAsync();
	}

	public async Task<IEnumerable<EventEntity>> GetAllEventsAsync()
	{
		return await _context.Events
			.Where(e => e.Status == EventStatus.Planned)
			.ToListAsync();
	}

	public async Task<EventEntity> GetEventById(Guid eventid)
	{
		return await _context.Events.FindAsync(eventid);
	}

	public async Task<IEnumerable<EventEntity>> GetEventsByCategoryAsync(int categoryId)
	{
		return await _context.Events.Where(e => e.CategoryId == categoryId 
			&& e.Status == EventStatus.Planned).ToListAsync();
	}

	public async Task<IEnumerable<EventEntity>> GetEventsByTitle(string title)
	{
		return await _context.Events
			.Where(e => EF.Functions.ILike(e.EventName, $"%{title}%") && e.Status == EventStatus.Planned)
			.OrderBy(e => e.EventName)
			.Take(8)
			.ToListAsync();
	}

	public async Task RemoveEventAsync(Guid eventId)
	{
		var specialEvent = GetEventById(eventId);

		_context.Remove(specialEvent);
		await _context.SaveChangesAsync();
	}

	public async Task UpdateEventAsync(Guid eventId, EventEntity events)
	{
		var specialEvent = await GetEventById(eventId);

		specialEvent.Image = events.Image;
		specialEvent.EventName = events.EventName;
		specialEvent.Description = events.Description;
		specialEvent.Date = events.Date;
		specialEvent.StartTime = events.StartTime;
		specialEvent.EndTime = events.EndTime;
		specialEvent.Price = events.Price;
		specialEvent.Location = events.Location;
		specialEvent.MapUrl = events.MapUrl;
		specialEvent.Status = events.Status;
		specialEvent.Capacity = events.Capacity;
		specialEvent.CategoryId = events.CategoryId;

		await _context.SaveChangesAsync();
	}
}
