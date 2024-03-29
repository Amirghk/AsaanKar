using System.Linq.Expressions;
using FinalProject.Application.Common.DataTransferObjects;
using FinalProject.Domain.Entities;
using FinalProject.Domain.Interfaces;

namespace FinalProject.Application.Common.Interfaces.Repositories;

public interface IServiceRepository
{
    Task<int> Add(ServiceDto model);
    Task<int> Update(ServiceDto model);
    Task<int> Remove(int id);
    Task<int> SoftDelete(int id);
    Task<ServiceDto> GetById(int id, CancellationToken cancellationToken);
    Task<IEnumerable<ServiceDto>> GetAll(CancellationToken cancellationToken, int? categoryId = null, string? expertId = null);
}