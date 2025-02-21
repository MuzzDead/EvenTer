using EvenTer.DAL.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvenTer.DAL.Persistence.Configurations.User
{
	public class UserConfiguration : IEntityTypeConfiguration<Entities.Users.User>
	{
		public void Configure(EntityTypeBuilder<Entities.Users.User> builder)
		{
			builder
				.Property(x => x.Username)
				.HasMaxLength(20)
				.IsRequired();

			
		}
	}
}
