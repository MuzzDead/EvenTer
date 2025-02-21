using EvenTer.DAL.Entities.Events;
using EvenTer.DAL.Entities.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvenTer.DAL.Persistence
{
	public class EvenTerDbContext : DbContext
	{
		public EvenTerDbContext()
		{
			Database.EnsureCreated();
		}

		public EvenTerDbContext(DbContextOptions<EvenTerDbContext> options) : base(options)
		{

			Database.EnsureCreated();
		}

		public DbSet<User> Users { get; set; }
		public DbSet<Event> Events { get; set; }
		public DbSet<EventCategory> EventsCategory { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.ApplyConfigurationsFromAssembly(typeof(EvenTerDbContext).Assembly);
		}
	}
}
