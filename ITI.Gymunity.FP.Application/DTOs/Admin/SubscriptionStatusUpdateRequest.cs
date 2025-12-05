using ITI.Gymunity.FP.Domain.Models.Enums;

namespace ITI.Gymunity.FP.Application.DTOs.Admin
{
 public class SubscriptionStatusUpdateRequest
 {
 public SubscriptionStatus NewStatus { get; set; }
 public string AdminUserId { get; set; } = null!;
 }
}
