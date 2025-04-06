using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EvenTer.DAL.Entities.Events;

namespace EvenTer.BLL.Interfaces.Event.IRepositories;

public interface IEventCategoryRepository
{
	Task AddCategoryAsync(EventCategory category);
	Task RemoveCategoryAsync(int categoryId);
	Task UpdateCategoryAsync(int categoryId, EventCategory category);
	Task<IEnumerable<EventCategory>> GetAllCategoryAsync();
	Task<EventCategory> GetCategoryById(int categoryid);
	Task<IEnumerable<EventCategory>> GetCategoryByNameAsync(string categoryName);
}
