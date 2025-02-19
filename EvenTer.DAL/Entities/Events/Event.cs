using EvenTer.DAL.Entities.Users;
using EvenTer.DAL.Enums.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvenTer.DAL.Entities.Events;

public class Event
{
	public Guid Id { get; set; }
	public string Image {  get; set; }
	public string EventName { get; set; }
	public string Description { get; set; }
	public DateOnly Date { get; set; }
	public TimeOnly StartTime { get; set; }
	public TimeOnly? EndTime { get; set; }
	public decimal Price { get; set; } = 0;
	public string Location { get; set; }
	public string? MapUrl { get; set; }
	public EventStatus Status { get; set; } = EventStatus.Planned;
	public EventCategory Category { get; set; } // event category
	public int? Capacity { get; set; }
	// public int? RegisteredCount { get; set; }
	public User Organizer { get; set; } // connection with organizer
}
