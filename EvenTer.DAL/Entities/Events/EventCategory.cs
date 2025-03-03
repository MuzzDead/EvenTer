using EvenTer.DAL.Enums.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvenTer.DAL.Entities.Events
{
	public class EventCategory
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Image { get; set; } // how it to save in db?
		public string Description { get; set; }
		public CategoryStatus Status { get; set; } = CategoryStatus.Active;

		public ICollection<Event> Events { get; set; } = new List<Event>();
	}
}
