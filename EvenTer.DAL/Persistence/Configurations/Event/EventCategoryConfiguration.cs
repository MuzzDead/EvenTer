using EvenTer.DAL.Entities.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvenTer.DAL.Persistence.Configurations.Event
{
	public class EventCategoryConfiguration : IEntityTypeConfiguration<EventCategory>
	{
		public void Configure(EntityTypeBuilder<EventCategory> builder)
		{
			builder.Property(e => e.Title)
				.HasMaxLength(20)
				.IsRequired();

			builder.Property(e => e.Description)
				.HasMaxLength(100);
		}
	}
}
