using EvenTer.BLL.DTO.Event;
using EvenTer.BLL.Interfaces.Event.IRepositories;
using EvenTer.BLL.Interfaces.Event.IServices;
using EvenTer.DAL.Entities.Events;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvenTer.BLL.Services.Event;

public class EventCategoryService : IEventCategoryService
{
	private readonly IEventCategoryRepository _repository;
	public EventCategoryService(IEventCategoryRepository repository)
	{
		_repository = repository;
	}


	public async Task CreateCategory(EventCategoryDTO categoryDTO)
	{
		await _repository.AddCategoryAsync(MapEntity(categoryDTO));
	}

	public async Task DeleteCategory(int categoryId)
	{
		await _repository.RemoveCategoryAsync(categoryId);
	}

	public async Task<IEnumerable<EventCategory>> GetAllCategories()
	{
		return await _repository.GetAllCategoryAsync();
	}

	public async Task<EventCategory> GetCategory(int categoryId)
	{
		return await _repository.GetCategoryById(categoryId);
	}

	public async Task<IEnumerable<EventCategory>> GetCategoryByNameAsync(string categoryName)
	{
		if (categoryName == null)
			throw new ArgumentNullException(nameof(categoryName), "Event data is null!");

		return await _repository.GetCategoryByNameAsync(categoryName);
	}

	public async Task UpdateCategory(int categoryId, EventCategoryDTO categoryDTO)
	{
		await _repository.UpdateCategoryAsync(categoryId, MapEntity(categoryDTO));
	}

	private EventCategory MapEntity(EventCategoryDTO categoryDTO)
	{
		if (categoryDTO == null)
			throw new ArgumentNullException(nameof(categoryDTO), "Event data is null!");

		return new EventCategory {
			Image = categoryDTO.Image,
			Title = categoryDTO.Title,
			Description = categoryDTO.Description
		};
	}
}
