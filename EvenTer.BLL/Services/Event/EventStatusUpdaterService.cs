using EvenTer.DAL.Enums.Events;
using EvenTer.DAL.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvenTer.BLL.Services.Event
{
	public class EventStatusUpdaterService : BackgroundService
	{
		private readonly IServiceProvider _serviceProvider;
		private readonly TimeSpan _interval = TimeSpan.FromHours(2);

		public EventStatusUpdaterService(IServiceProvider serviceProvider)
		{
			_serviceProvider = serviceProvider;
		}

		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			while (!stoppingToken.IsCancellationRequested)
			{
				await UpdateEventStatusesAsync();

				await Task.Delay(_interval, stoppingToken);
			}
		}

		private async Task UpdateEventStatusesAsync()
		{
			using var scope = _serviceProvider.CreateScope();
			var context = scope.ServiceProvider.GetRequiredService<EvenTerDbContext>();

			var dateTimeNow = DateTime.Now;
			var today = DateOnly.FromDateTime(dateTimeNow);
			var now = TimeOnly.FromDateTime(dateTimeNow);

			var eventsToUpdate = await context.Events
				.Where(e => e.Status == EventStatus.Planned
				&& e.Date == today &&
				(
					(e.EndTime.HasValue && e.EndTime.Value <= now) || 
					(!e.EndTime.HasValue && e.StartTime <= now.AddHours(-2))
				))
				.ToListAsync();

			if (eventsToUpdate.Any())
			{
				foreach (var e in eventsToUpdate)
				{
					e.Status = EventStatus.Completed;
				}

				await context.SaveChangesAsync();
			}
		}
	}
}
