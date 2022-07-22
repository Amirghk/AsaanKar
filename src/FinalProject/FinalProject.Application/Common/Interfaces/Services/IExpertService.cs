using FinalProject.Application.Common.DataTransferObjects;

namespace FinalProject.Application.Common.Interfaces.Services;

public interface IExpertService
{
    Task<int> Set(ExpertDto dto);
    Task<IEnumerable<ExpertDto>> GetAll();
    Task<ExpertDto> GetByUserId(string userId);
    Task<ExpertDto> GetById(int id);
    Task<int> Remove(int id);
    Task<int> SoftDelete(string expertId);
    Task<int> Update(ExpertDto dto);

}
