using System.Linq.Expressions;
using FinalProject.Domain.Dtos;
using FinalProject.Domain.Entities;
using FinalProject.Domain.Interfaces;

namespace FinalProject.Domain.Interfaces;

public interface ICityRepository
{
    Task<int> Add(CityDto model);
    Task<int> Update(CityDto model);
    Task<int> Remove(int id);
    Task<CityDto> GetById(int id);
    Task<IEnumerable<CityDto>> GetAll();
}