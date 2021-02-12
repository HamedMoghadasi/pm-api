using System;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Common;
using DataAccess.DataAccess;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.IdentityModel.Tokens;

namespace WebFramework.Configuration
{
	public static class ServiceCollectionExtentions
	{
		public static void AddjwtAuthentication(this IServiceCollection services,JwtSettings jwtSettings)
		{
			services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(options =>
			{
				var secretKey = Encoding.UTF8.GetBytes(jwtSettings.secretkey);
				var encryptionkey = Encoding.UTF8.GetBytes(jwtSettings.secretkey);
				var validationparameters=new TokenValidationParameters
				{
					ClockSkew = TimeSpan.Zero,
					RequireSignedTokens = true,
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(secretKey),
					RequireExpirationTime = true,
					ValidateLifetime = true,
					ValidateAudience = true,
					ValidAudience = jwtSettings.Audience,
					ValidateIssuer = true,
					ValidIssuer = jwtSettings.Issuer,
					TokenDecryptionKey = new SymmetricSecurityKey(secretKey)
				};
				options.RequireHttpsMetadata = false;
				options.SaveToken = true;
				options.TokenValidationParameters = validationparameters;
				options.Events=new JwtBearerEvents
				{
					OnAuthenticationFailed = context =>
					{
						return Task.CompletedTask;
					},
					OnTokenValidated = async context =>
					{
						var signInManager = context.HttpContext.RequestServices
							.GetRequiredService<SignInManager<ApplicationUser>>();

						var claimsIdentity = context.Principal.Identity as ClaimsIdentity;
						if (claimsIdentity.Claims?.Any() != true)
							context.Fail("This token has no claims.");

						//Find user and token from database and perform your custom validation
					
						var validatedUser = await signInManager.ValidateSecurityStampAsync(context.Principal);
						if (validatedUser == null)
							context.Fail("Token secuirty stamp is not valid.");
					}
				};
			});
		}
	}
}
