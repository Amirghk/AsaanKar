using FinalProject.Application.Common.DataTransferObjects;
namespace FinalProject.Application.Common.Interfaces.Services;

public interface IProvinceService
{
    Task<int> Set(ProvinceDto dto);
    Task<IEnumerable<ProvinceDto>> GetAll();
    Task<ProvinceDto> GetById(int id);
    Task<int> Remove(int id);
    Task<int> Update(ProvinceDto dto);
}
