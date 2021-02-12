using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DataAccess.DataAccess;
using Microsoft.AspNetCore.Identity;

namespace Services.Services
{
	public interface IJwtService
	{
		 Task<string> GenerateAsync(ApplicationUser user);
	}
}
