using FinalProject.Application.Common.Dtos;
using FinalProject.Domain.Dtos;

namespace FinalProject.Application.Common.Interfaces.Services;

public interface IUploadService
{
    Task<int> Set(UploadServiceDto dto, string uploadsRootFolder);
    Task<IEnumerable<UploadDto>> GetAll();
    Task<UploadDto> GetById(int id);
    Task<int> Remove(int id, string uploadsRootFolder);
    Task<int> Update(UploadDto dto);
}
