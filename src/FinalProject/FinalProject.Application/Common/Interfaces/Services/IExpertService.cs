using FinalProject.Application.Common.DataTransferObjects;
using FinalProject.Application.Common.DataTransferObjects.Expert;

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
    Task<string> AddServices(string expertId, List<int> serviceIds, CancellationToken cancellationToken);
    Task<string> RemoveService(string expertId, int serviceId, CancellationToken cancellationToken);
    Task<ExpertPublicProfileDto> GetExpertPublicProfile(string expertId, CancellationToken cancellationToken);
}
