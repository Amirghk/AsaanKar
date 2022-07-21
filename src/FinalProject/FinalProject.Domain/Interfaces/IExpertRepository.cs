using System.Linq.Expressions;
using FinalProject.Domain.Dtos;
using FinalProject.Domain.Entities;
using FinalProject.Domain.Interfaces;

namespace FinalProject.Domain.Interfaces;

public interface IExpertRepository
{
    Task<int> Add(ExpertDto model);
    Task<int> Update(ExpertDto model);
    Task<int> Remove(int id);
    Task<int> SoftDelete(string expertId);
    Task<ExpertDto> GetById(int id);
    Task<ExpertDto> GetByUserId(string userId);
    Task<IEnumerable<ExpertDto>> GetAll();
}