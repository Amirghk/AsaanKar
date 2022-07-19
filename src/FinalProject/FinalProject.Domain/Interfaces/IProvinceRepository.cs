using System.Linq.Expressions;
using FinalProject.Domain.Dtos;
using FinalProject.Domain.Entities;
using FinalProject.Domain.Interfaces;

namespace FinalProject.Domain.Interfaces;

public interface IProvinceRepository
{
    Task<int> Add(ProvinceDto model);
    Task<int> Update(ProvinceDto model);
    Task<int> Remove(int id);
    Task<ProvinceDto> GetById(int id);
    Task<IEnumerable<ProvinceDto>> GetAll();
}