
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Common;
using DataAccess.DataAccess;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Services.Services
{
	public class JwtService : IJwtService
	{
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly SiteSettings _siteSettings;
		public JwtService(IOptionsSnapshot<SiteSettings> setting,SignInManager<ApplicationUser> signInManager)
		{
			_signInManager = signInManager;
			_siteSettings = setting.Value ;
		}
		public async Task<string> GenerateAsync(ApplicationUser user)
		{
			var secretKey = Encoding.UTF8.GetBytes(_siteSettings.JwtSettings.secretkey); // secret key on server to ensure security (encrypt+decrypt)
			var signinCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature);

			var encryptionkey = Encoding.UTF8.GetBytes(_siteSettings.JwtSettings.secretkey); //must be 16 character
			var encryptingCredentials = new EncryptingCredentials(new SymmetricSecurityKey(encryptionkey), SecurityAlgorithms.Aes128KW, SecurityAlgorithms.Aes128CbcHmacSha256);

			var claims =await _getClaimsAsync(user);
			var descriptor = new SecurityTokenDescriptor()
			{
				Issuer = _siteSettings.JwtSettings.Issuer,
				Audience = _siteSettings.JwtSettings.Audience,
				IssuedAt = DateTime.Now,
				Expires = DateTime.Now.AddMinutes(30),
				SigningCredentials = signinCredentials,
				EncryptingCredentials = encryptingCredentials,
				Subject = new ClaimsIdentity(claims)
			
			};
			var tokenHandler = new JwtSecurityTokenHandler();
			var securityToken = tokenHandler.CreateToken(descriptor);
			var jwt = tokenHandler.WriteToken(securityToken);
			return jwt;
		}

		private async Task<IEnumerable<Claim>> _getClaimsAsync(ApplicationUser user)
		{
			var result = await _signInManager.ClaimsFactory.CreateAsync(user);
			var list=new List<Claim>(result.Claims);
			list.Add(new Claim(ClaimTypes.MobilePhone,"09191677925"));
			//var list = new List<Claim>()
			//{
			//	new Claim(ClaimTypes.Name,user.Username),
			//	new Claim(ClaimTypes.NameIdentifier,user.UserId.ToString())
			//};
			//var roles = new Role[]
			//{
			//	new Role {Name = "Admin"}
			//};
			//foreach (var role in roles)
			//{
			//	list.Add(new Claim(ClaimTypes.Role,role.Name));
			//}
			//we can add roles too
			return list;
		}
	}
}
