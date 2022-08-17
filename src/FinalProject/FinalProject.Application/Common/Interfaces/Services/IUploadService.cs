using FinalProject.Application.Common.DataTransferObjects;
using FinalProject.Domain.Enums;

namespace FinalProject.Application.Common.Interfaces.Services;

public interface IUploadService
{
    Task<int> Set(UploadServiceDto dto, string uploadsRootFolder, CancellationToken cancellationToken);
    Task<IEnumerable<UploadDto>> GetAll(CancellationToken cancellationToken, string? expertId = null, FileCategory? fileCategory = null);
    Task<UploadDto> GetById(int id, CancellationToken cancellationToken);
    Task<int> Remove(int id, string uploadsRootFolder, CancellationToken cancellationToken);
    Task<int> Update(UploadDto dto, CancellationToken cancellationToken);
    Task<int> SetExpertWorkSamples(UploadServiceDto workSample, string uploadsRootFolder, CancellationToken cancellationToken);
    Task<string> GetFileDirectory(int fileId, CancellationToken cancellationToken);
}
