namespace ITI.Gymunity.FP.Application.DTOs.Admin
{
 public class UserAdminGetAllResponse
 {
 public string UserId { get; set; } = null!;
 public string UserName { get; set; } = null!;
 public string Email { get; set; } = null!;
 public string Role { get; set; } = null!;
 public bool IsVerified { get; set; }
 }
}
