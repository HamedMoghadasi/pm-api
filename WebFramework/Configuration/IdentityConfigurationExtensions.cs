using System;
using System.Collections.Generic;
using System.Text;
using DataAccess;
using DataAccess.DataAccess;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace WebFramework.Configuration
{
	public static class IdentityConfigurationExtensions
	{
		public static void AddCustomIdentity(this IServiceCollection services)
		{
			services.AddIdentity<ApplicationUser, IdentityRole>(identityOptions =>
				{
					//Password Settings
					identityOptions.Password.RequiredLength = 6;
				})
				.AddEntityFrameworkStores<TmsManagementContext>()
				.AddDefaultTokenProviders();
		}
    }
}
