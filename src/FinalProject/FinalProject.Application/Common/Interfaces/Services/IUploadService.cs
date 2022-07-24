using FinalProject.Application.Common.DataTransferObjects;

namespace FinalProject.Application.Common.Interfaces.Services;

public interface IUploadService
{
    Task<int> Set(UploadServiceDto dto, string uploadsRootFolder);
    Task<IEnumerable<UploadDto>> GetAll();
    Task<UploadDto> GetById(int id);
    Task<int> Remove(int id, string uploadsRootFolder);
    Task<int> Update(UploadDto dto);
    Task<string> SetExpertWorkSamples(List<UploadServiceDto> workSamples, string uploadsRootFolder);
    Task<string> GetFileDirectory(string rootPath, int fileId);
}
