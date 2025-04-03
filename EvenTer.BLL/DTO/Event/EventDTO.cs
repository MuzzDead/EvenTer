using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvenTer.BLL.DTO.Event;

public class EventDTO
{
	public string Image { get; set; }
	public string EventName { get; set; }
	public string Description { get; set; }
	public DateOnly Date { get; set; }
	public TimeOnly StartTime { get; set; }
	public TimeOnly? EndTime { get; set; }
	public decimal Price { get; set; } = 0;
	public string Location { get; set; }
	public string? MapUrl { get; set; }
	public int? Capacity { get; set; }
	public Guid OrganizerId { get; set; }
	public int CategoryId { get; set; }
}
