using EvenTer.DAL.Entities.Events;
using EvenTer.DAL.Enums.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvenTer.DAL.Entities.Users;

public class User
{
	public Guid Id { get; set; }
	public string Image {  get; set; }
	public string Username { get; set; }
	public string FirstName { get; set; }
	public string LastName { get; set; }
	public string Email { get; set; }
	public string HashedPassword { get; set; }
	public string? OrganisationName {  get; set; }
	public UserRole UserRole { get; set; } = UserRole.User;
	public DateTime CreatedAt {  get; set; } = DateTime.Now;
	public bool IsActive { get; set; } = true;

	public ICollection<Event> Events { get; set; } = new List<Event>();
}
