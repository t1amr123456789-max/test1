using System;

namespace ITI.Gymunity.FP.Application.DTOs.Admin
{
 public class TrainerAdminGetResponse
 {
 public int Id { get; set; }
 public string UserId { get; set; } = null!;
 public string UserName { get; set; } = null!;
 public string Handle { get; set; } = null!;
 public bool IsVerified { get; set; }
 public DateTime CreatedAt { get; set; }
 }
}
