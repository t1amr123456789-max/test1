using System;

namespace ITI.Gymunity.FP.Application.DTOs.Admin
{
 public class ProgramAdminGetResponse
 {
 public int Id { get; set; }
 public string Title { get; set; } = null!;
 public string TrainerId { get; set; } = null!;
 public string? TrainerUserName { get; set; }
 public DateTime CreatedAt { get; set; }
 public bool IsPublic { get; set; }
 public bool IsDeleted { get; set; }
 }
}
