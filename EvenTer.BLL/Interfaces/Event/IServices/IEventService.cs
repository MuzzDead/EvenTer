using EvenTer.BLL.DTO.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventEntity = EvenTer.DAL.Entities.Events.Event;

namespace EvenTer.BLL.Interfaces.Event.IServices;

public interface IEventService
{
	Task AddEvent(EventDTO eventDTO);
	Task DeleteEvent(Guid eventId);
	Task<IEnumerable<EventEntity>> GetEvents();
	Task<EventEntity> GetEvent(Guid eventId);
	Task UpdateEvent(Guid eventId, EventDTO eventDTO);
}
