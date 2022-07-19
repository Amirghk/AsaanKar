using FinalProject.Domain.Dtos;

namespace FinalProject.Application.Common.Interfaces.Services;

public interface IUploadService
{
    Task<int> Set(UploadDto dto);
    Task<IEnumerable<UploadDto>> GetAll();
    Task<UploadDto> GetById(int id);
    Task<int> Remove(int id);
    Task<int> Update(UploadDto dto);
}
