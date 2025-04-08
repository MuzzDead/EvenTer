using EvenTer.BLL.Interfaces.User.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EvenTer.BLL.Services.User;

public class CurrentUserService : ICurrentUserService
{
	public Guid? GetUserId(ClaimsPrincipal user)
	{
		var currentUser = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
		if (Guid.TryParse(currentUser, out var userId))
		{
			return userId;
		}

		return null;
	}
}
