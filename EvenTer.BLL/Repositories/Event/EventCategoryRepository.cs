using EvenTer.BLL.Interfaces.Event.IRepositories;
using EvenTer.DAL.Entities.Events;
using EvenTer.DAL.Enums.Events;
using EvenTer.DAL.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace EvenTer.BLL.Repositories.Event;

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

	public async Task<EventCategory> GetCategoryById(int categoryid)
	{
		return await _context.EventsCategory.FindAsync(categoryid);
	}

	public async Task<IEnumerable<EventCategory>> GetCategoryByNameAsync(string categoryName)
	{
		return await _context.EventsCategory
			.Where(e => EF.Functions.ILike(e.Title, $"%{categoryName}%"))
			.OrderBy(e => e.Title)
			.Take(5)
			.ToListAsync();
	}

	public async Task RemoveCategoryAsync(int categoryId)
	{
		var category = await GetCategoryById(categoryId);

		_context.EventsCategory.Remove(category);
		await _context.SaveChangesAsync();
	}

	public async Task UpdateCategoryAsync(int categoryId, EventCategory category)
	{
		var updCategory = await GetCategoryById(categoryId);

		updCategory.Image = category.Image;
		updCategory.Title = category.Title;
		updCategory.Description = category.Description;
		updCategory.Status = category.Status;

		await _context.SaveChangesAsync();
	}
}
