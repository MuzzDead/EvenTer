using EvenTer.BLL.DTO.Event;
using EvenTer.DAL.Entities.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvenTer.BLL.Interfaces.Event.IServices;

public interface IEventCategoryService
{
	Task CreateCategory(EventCategoryDTO categoryDTO);
	Task DeleteCategory(int categoryId);
	Task UpdateCategory(int categoryId, EventCategoryDTO categoryDTO);
	Task<EventCategory> GetCategory(int categoryId);
	Task<IEnumerable<EventCategory>> GetAllCategories();
	Task<IEnumerable<EventCategory>> GetCategoryByNameAsync(string categoryName);
}
