using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EvenTer.DAL.Entities.Events;

namespace EvenTer.BLL.Interfaces.Event;

public interface IEventCategoryRepository
{
	Task AddCategoryAsync(EventCategory category);
	Task RemoveCategoryAsync(Guid categoryId);
	Task UpdateCategoryAsync(Guid categoryId, EventCategory category);
	Task<IEnumerable<EventCategory>> GetAllCategoryAsync();
	Task<EventCategory> GetCategoryById(Guid categoryid);
	Task<EventCategory> GetCategoryByNameAsync(string categoryName);
}
