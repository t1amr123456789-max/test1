using System;

namespace ITI.Gymunity.FP.Application.DTOs.Admin
{
 public class ExerciseAdminGetResponse
 {
 public int Id { get; set; }
 public string Name { get; set; } = null!;
 public string? TrainerId { get; set; }
 public bool IsCustom { get; set; }
 public bool IsDeleted { get; set; }
 public DateTime CreatedAt { get; set; }
 }
}
