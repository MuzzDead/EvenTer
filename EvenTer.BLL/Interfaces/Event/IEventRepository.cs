using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventEntity = EvenTer.DAL.Entities.Events.Event;

namespace EvenTer.BLL.Interfaces.Event;

public interface IEventRepository
{
	Task AddEventAsync(EventEntity events);
	Task RemoveEventAsync(Guid eventId);
	Task UpdateEventAsync(Guid eventId, EventEntity events);
	Task<IEnumerable<EventEntity>> GetAllEventsAsync();
	Task<EventEntity> GetEventById (Guid eventid);
}
