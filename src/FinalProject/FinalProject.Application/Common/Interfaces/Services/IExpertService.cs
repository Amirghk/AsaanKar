using FinalProject.Application.Common.DataTransferObjects;

namespace FinalProject.Application.Common.Interfaces.Services;

public interface IExpertService
{
    Task<string> Set(ExpertDto dto);
    Task<IEnumerable<ExpertDto>> GetAll();
    Task<ExpertDto> GetById(string id);
    Task<string> Remove(string id);
    Task<string> SoftDelete(string id);
    Task<string> Update(ExpertDto dto);
    Task<string> GetName(string name);

}
