using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Common.Exceptions;
using Common.Utilities;
using DataAccess.DataAccess;
using DataAccess.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using WebFramework.Api;

namespace PosManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IJwtService _jwtService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public UserController(IJwtService jwtService, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<ApplicationUser> signInManager)
        {
            this._jwtService = jwtService;
            this._userManager = userManager;
            this._roleManager = roleManager;
            this._signInManager = signInManager;
        }
        /// <summary>
        /// Logins the specified user.
        /// </summary>
        /// <param name="userDto">The user dto.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("~/api/user/Login")]
        [AllowAnonymous]
        public async Task<ApiResult<UserDto>> Login(UserDto userDto, CancellationToken cancellationToken)
        {
            var passwordHash = SecurityHelper.GetSha256Hash(userDto.Password.ToLower().Trim());
            var user = _userManager.Users.FirstOrDefault(u =>
                u.UserName == userDto.Username &&
                u.PasswordHash == passwordHash &&
                u.Status && !u.IsDeleted);
            if (user == null)
            {
                return NotFound();
            }
            var jwt = await _jwtService.GenerateAsync(user);
            var currentUser = new UserDto()
            {
                id = user.Id,
                fullname = user.FullName,
                Jwt = jwt,
                Username = user.UserName,
                MenuPermissionList = user.FullName,
                StatusCode = true
            };
            return Ok(currentUser);
            //return new ApiResult()
            //{
            //	Data = currentUser,
            //	IsSuccess = true,
            //	Message = "عملیات با موفقیت انجام شد",
            //	StatusCode = ApiResultStatusCode.Success
            //};
        }

        /// <summary>
        /// Creates the specified user.
        /// </summary>
        /// <param name="userDto">The user dto.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("~/api/user/create")]
        //	[Authorize(Roles = "Admin")]
        public async Task<ApiResult<ApplicationUser>> Create(UserDto userDto, CancellationToken cancellationToken)
        {
            var user = new ApplicationUser()
            {
                Id = Guid.NewGuid().ToString(),
                PasswordHash = SecurityHelper.GetSha256Hash(userDto.Password),
                UserName = userDto.Username,
                FullName = "1",
                DateTimeCreated = DateTime.Now,
                DateTimeModified = DateTime.Now,
                Status = userDto.Status == 1 ? true : false
            };
            await _userManager.CreateAsync(user);
            //var result2 = await _roleManager.CreateAsync(new IdentityRole()
            //{
            //	Name = "NormalUser",
            //	Id = Guid.NewGuid().ToString()
            //});
            //var result3 = await _userManager.AddToRoleAsync(user, "NormalUser");
            return user;
        }
        /// <summary>
		/// Deletes the specified user dto.
		/// </summary>
		/// <param name="userDto">The user dto.</param>
		/// <param name="cancellationToken">The cancellation token.</param>
		/// <returns></returns>
		/// <exception cref="BadRequestException">کاربر مورد نظر یافت نشد!</exception>
		[HttpPost]
        [Route("~/api/user/delete")]
        public async Task<ApiResult> Delete(UserDto userDto, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(userDto.Username);
            if (user == null)
            {
                throw new BadRequestException("کاربر مورد نظر یافت نشد!");
            }
            else
            {
                //await _userManager.DeleteAsync(user);
                var updateUser = await _userManager.FindByNameAsync(userDto.Username);
                updateUser.IsDeleted = true;
                await _userManager.UpdateAsync(updateUser);
                return Ok();
            }
        }

        [HttpPost]
        [Route("~/api/user/edit")]
        [AllowAnonymous]
        public async Task<ApiResult> Edit(UserDto userDto, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(userDto.id);
            if (user == null)
            {
                throw new BadRequestException("کاربر مورد نظر یافت نشد!");
            }
            else
            {
                //await _userManager.DeleteAsync(user);
                user.UserName = userDto.Username;
                user.Status = userDto.StatusCode;
                await _userManager.UpdateAsync(user);
                return Ok();
            }
        }
        /// <summary>
        /// Updates the status.
        /// </summary>
        /// <param name="userDto">The user dto.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        /// <exception cref="BadRequestException">کاربر مورد نظر یافت نشد!</exception>
        [HttpPost]
        [Route("~/api/user/update")]
        public async Task<ApiResult> UpdateStatus(UserDto userDto, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(userDto.Username);
            if (user == null)
            {
                throw new BadRequestException("کاربر مورد نظر یافت نشد!");
            }
            else
            {
                var updateUser = await _userManager.FindByNameAsync(userDto.Username);
                if (userDto.Status != null)
                {
                    if (userDto.Status == 1)
                    {
                        updateUser.Status = true;
                    }
                    else
                    {
                        updateUser.Status = false;
                    }
                }
                await _userManager.UpdateAsync(updateUser);
                return Ok();
            }
        }
    }
}
