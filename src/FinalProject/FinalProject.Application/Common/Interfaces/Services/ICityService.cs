using FinalProject.Domain.Dtos;
namespace FinalProject.Application.Common.Interfaces.Services;

public interface ICityService
{
    Task<int> Set(CityDto dto);
    Task<IEnumerable<CityDto>> GetAll();
    Task<CityDto> GetById(int id);
    Task<int> Remove(int id);
    Task<int> Update(CityDto dto);
}
