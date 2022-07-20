using FinalProject.Domain.Dtos;

namespace FinalProject.Application.Common.Interfaces.Services;

public interface IExpertService
{
    Task<int> Set(ExpertDto dto);
    Task<IEnumerable<ExpertDto>> GetAll();
    Task<ExpertDto> GetByUserId(string userId);
    Task<ExpertDto> GetById(int id);
    Task<int> Remove(int id);
    Task<int> Update(ExpertDto dto);

}
