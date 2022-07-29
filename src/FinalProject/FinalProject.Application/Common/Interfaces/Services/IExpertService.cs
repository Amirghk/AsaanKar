using FinalProject.Application.Common.DataTransferObjects;

namespace FinalProject.Application.Common.Interfaces.Services;

public interface IExpertService
{
    Task<string> Set(ExpertDto dto, CancellationToken cancellationToken);
    Task<IEnumerable<ExpertDto>> GetAll(CancellationToken cancellationToken);
    Task<ExpertDto> GetById(string id, CancellationToken cancellationToken);
    Task<string> Remove(string id, CancellationToken cancellationToken);
    Task<string> SoftDelete(string id, CancellationToken cancellationToken);
    Task<string> Update(ExpertDto dto, CancellationToken cancellationToken);
    Task<string> GetName(string name, CancellationToken cancellationToken);

}
