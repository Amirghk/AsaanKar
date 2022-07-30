using FinalProject.Application.Common.DataTransferObjects;

namespace FinalProject.Application.Common.Interfaces.Services;

public interface IUploadService
{
    Task<int> Set(UploadServiceDto dto, string uploadsRootFolder, CancellationToken cancellationToken);
    Task<IEnumerable<UploadDto>> GetAll(CancellationToken cancellationToken);
    Task<UploadDto> GetById(int id, CancellationToken cancellationToken);
    Task<int> Remove(int id, string uploadsRootFolder, CancellationToken cancellationToken);
    Task<int> Update(UploadDto dto, CancellationToken cancellationToken);
    Task<string> SetExpertWorkSamples(List<UploadServiceDto> workSamples, string uploadsRootFolder, CancellationToken cancellationToken);
    Task<string> GetFileDirectory(int fileId, CancellationToken cancellationToken);
}
