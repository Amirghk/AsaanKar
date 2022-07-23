using System.Linq.Expressions;
using FinalProject.Application.Common.DataTransferObjects;
using FinalProject.Domain.Entities;
using FinalProject.Domain.Interfaces;

namespace FinalProject.Application.Common.Interfaces.Repositories;

public interface IExpertRepository
{
    Task<string> Add(ExpertDto model);
    Task<string> Update(ExpertDto model);
    Task<string> Remove(string id);
    Task<string> SoftDelete(string id);
    Task<ExpertDto> GetById(string id);
    Task<IEnumerable<ExpertDto>> GetAll();
}