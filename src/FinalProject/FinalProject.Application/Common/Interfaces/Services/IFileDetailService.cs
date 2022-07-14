using FinalProject.Application.Common.Dtos;

namespace FinalProject.Application.Common.Interfaces.Services;

public interface IFileDetailService
{
    Task<int> Set(FileDetailDto dto);
    Task<IEnumerable<FileDetailDto>> GetAll();
    Task<FileDetailDto> GetById(int id);
    Task<int> Remove(int id);
    Task<int> Update(FileDetailDto dto);
}
