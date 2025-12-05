using ITI.Gymunity.FP.Domain.Models.Enums;
using System;

namespace ITI.Gymunity.FP.Application.DTOs.Admin
{
 public class SubscriptionAdminGetResponse
 {
 public int Id { get; set; }
 public string ClientId { get; set; } = null!;
 public int PackageId { get; set; }
 public SubscriptionStatus Status { get; set; }
 public DateTime StartDate { get; set; }
 public DateTime CurrentPeriodEnd { get; set; }
 public decimal AmountPaid { get; set; }
 }
}
