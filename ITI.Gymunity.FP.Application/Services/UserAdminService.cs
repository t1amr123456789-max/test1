using AutoMapper;
using ITI.Gymunity.FP.Application.DTOs.Admin;
using ITI.Gymunity.FP.Domain.RepositoiesContracts;
using ITI.Gymunity.FP.Domain.Models.Identity;
using ITI.Gymunity.FP.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITI.Gymunity.FP.Application.Services
{
 public interface IUserAdminService
 {
 Task<IReadOnlyList<UserAdminGetAllResponse>> GetAllUsersAsync();
 Task<UserAdminGetByIdResponse?> GetByIdAsync(string userId);
 Task<bool> UpdateUserRoleAsync(string userId, string newRole);
 Task<bool> DeleteUserAsync(string userId);
 }

 public class UserAdminService : IUserAdminService
 {
 private readonly UserManager<AppUser> _userManager;
 private readonly IMapper _mapper;
 private readonly RoleManager<IdentityRole> _roleManager;

 public UserAdminService(UserManager<AppUser> userManager, IMapper mapper, RoleManager<IdentityRole> roleManager)
 {
 _userManager = userManager;
 _mapper = mapper;
 _roleManager = roleManager;
 }

 public async Task<IReadOnlyList<UserAdminGetAllResponse>> GetAllUsersAsync()
 {
 var users = await _userManager.Users.ToListAsync();
 return users.Select(u => new UserAdminGetAllResponse { UserId = u.Id, UserName = u.UserName ?? string.Empty, Email = u.Email ?? string.Empty, Role = u.Role.ToString(), IsVerified = u.IsVerified }).ToList();
 }

 public async Task<UserAdminGetByIdResponse?> GetByIdAsync(string userId)
 {
 var u = await _userManager.FindByIdAsync(userId);
 if (u == null) return null;
 return new UserAdminGetByIdResponse { UserId = u.Id, UserName = u.UserName ?? string.Empty, Email = u.Email ?? string.Empty, Role = u.Role.ToString(), IsVerified = u.IsVerified, LastLoginAt = u.LastLoginAt };
 }

 public async Task<bool> UpdateUserRoleAsync(string userId, string newRole)
 {
 var u = await _userManager.FindByIdAsync(userId);
 if (u == null) return false;
 if (!await _roleManager.RoleExistsAsync(newRole))
 {
 await _roleManager.CreateAsync(new IdentityRole(newRole));
 }
 // remove current role(s)
 var userRoles = await _userManager.GetRolesAsync(u);
 if (userRoles.Any()) await _userManager.RemoveFromRolesAsync(u, userRoles);
 await _userManager.AddToRoleAsync(u, newRole);
 u.Role = Enum.Parse<ITI.Gymunity.FP.Domain.Models.Enums.UserRole>(newRole);
 await _userManager.UpdateAsync(u);
 return true;
 }

 public async Task<bool> DeleteUserAsync(string userId)
 {
 var u = await _userManager.FindByIdAsync(userId);
 if (u == null) return false;
 await _userManager.DeleteAsync(u);
 return true;
 }
 }
}
