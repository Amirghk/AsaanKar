using System.Linq.Expressions;
using FinalProject.Domain.Dtos;
using FinalProject.Domain.Entities;
using FinalProject.Domain.Interfaces;

namespace FinalProject.Domain.Interfaces;

public interface IServiceRepository
{
    Task<int> Add(ServiceDto model);
    Task<int> Update(ServiceDto model);
    Task<int> Remove(int id);
    Task<ServiceDto> GetById(int id);
    Task<IEnumerable<ServiceDto>> GetAll();
}