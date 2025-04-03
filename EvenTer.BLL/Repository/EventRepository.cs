using EvenTer.BLL.Interfaces.Event;
using EvenTer.DAL.Entities.Events;
using EvenTer.DAL.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvenTer.BLL.Repository;

public class EventRepository : IEventRepository
{
	private readonly EvenTerDbContext _context;
	public EventRepository(EvenTerDbContext context)
	{
		_context = context;
	}

	public async Task AddEventAsync(Event events)
	{
		await _context.Events.AddAsync(events);
		await _context.SaveChangesAsync();
	}

	public async Task<IEnumerable<Event>> GetAllEventsAsync()
	{
		return await _context.Events.ToListAsync();
	}

	public async Task<Event> GetEventById(Guid eventid)
	{
		return await _context.Events.FindAsync(eventid);
	}

	public async Task RemoveEventAsync(Guid eventId)
	{
		var specialEvent = GetEventById(eventId);

		_context.Remove(specialEvent);
		await _context.SaveChangesAsync();
	}

	public async Task UpdateEventAsync(Guid eventId, Event events)
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
