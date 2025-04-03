using EvenTer.BLL.Interfaces.Event;
using EvenTer.DAL.Entities.Events;
using EvenTer.DAL.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace EvenTer.BLL.Repository;

public class EventCategoryRepository : IEventCategoryRepository
{
	private readonly EvenTerDbContext _context;
	public EventCategoryRepository(EvenTerDbContext context)
	{
		_context = context;
	}


	public async Task AddCategoryAsync(EventCategory category)
	{
		await _context.EventsCategory.AddAsync(category);
		await _context.SaveChangesAsync();
	}

	public async Task<IEnumerable<EventCategory>> GetAllCategoryAsync()
	{
		return await _context.EventsCategory.ToListAsync();
	}

	public async Task<EventCategory> GetCategoryById(Guid categoryid)
	{
		return await _context.EventsCategory.FindAsync(categoryid);
	}

	public async Task<EventCategory> GetCategoryByNameAsync(string categoryName)
	{
		return await _context.EventsCategory
			.FirstOrDefaultAsync(e => e.Title == categoryName);
	}

	public async Task RemoveCategoryAsync(Guid categoryId)
	{
		var category = await GetCategoryById(categoryId);

		_context.EventsCategory.Remove(category);
		await _context.SaveChangesAsync();
	}

	public async Task UpdateCategoryAsync(Guid categoryId, EventCategory category)
	{
		var updCategory = await GetCategoryById(categoryId);

		updCategory.Image = category.Image;
		updCategory.Title = category.Title;
		updCategory.Description = category.Description;
		updCategory.Status = category.Status;

		await _context.SaveChangesAsync();
	}
}
