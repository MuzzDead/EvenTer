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
	public class EventConfiguration : IEntityTypeConfiguration<Entities.Events.Event>
	{
		public void Configure(EntityTypeBuilder<Entities.Events.Event> builder)
		{
			builder.Property(e => e.EventName)
				.HasMaxLength(50)
				.IsRequired();

			builder.Property(e => e.Description)
				.HasMaxLength(450);

			builder.Property(e => e.Location)
				.HasMaxLength(80);
		}
	}
}
