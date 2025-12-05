using AutoMapper;
using ITI.Gymunity.FP.Application.DTOs.Admin;
using ITI.Gymunity.FP.Domain.RepositoiesContracts;
using ITI.Gymunity.FP.Domain.Models;
using ITI.Gymunity.FP.Domain.Models.Enums;
using ITI.Gymunity.FP.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITI.Gymunity.FP.Application.Services
{
 public interface ISubscriptionAdminService
 {
 Task<IReadOnlyList<SubscriptionAdminGetResponse>> GetAllSubscriptionsAsync();
 Task<bool> UpdateSubscriptionStatusAsync(int subscriptionId, SubscriptionStatus newStatus);
 }

 public class SubscriptionAdminService : ISubscriptionAdminService
 {
 private readonly IUnitOfWork _unitOfWork;
 private readonly IMapper _mapper;

 public SubscriptionAdminService(IUnitOfWork unitOfWork, IMapper mapper)
 {
 _unitOfWork = unitOfWork;
 _mapper = mapper;
 }

 public async Task<IReadOnlyList<SubscriptionAdminGetResponse>> GetAllSubscriptionsAsync()
 {
 var list = (await _unitOfWork.Repository<Subscription>().GetAllAsync()).ToList();
 return list.Select(s => new SubscriptionAdminGetResponse { Id = s.Id, ClientId = s.ClientId, PackageId = s.PackageId, Status = s.Status, StartDate = s.StartDate, CurrentPeriodEnd = s.CurrentPeriodEnd, AmountPaid = s.AmountPaid }).ToList();
 }

 public async Task<bool> UpdateSubscriptionStatusAsync(int subscriptionId, SubscriptionStatus newStatus)
 {
 var s = await _unitOfWork.Repository<Subscription>().GetByIdAsync(subscriptionId);
 if (s == null) return false;
 s.Status = newStatus;
 _unitOfWork.Repository<Subscription>().Update(s);
 await _unitOfWork.CompleteAsync();
 return true;
 }
 }
}
