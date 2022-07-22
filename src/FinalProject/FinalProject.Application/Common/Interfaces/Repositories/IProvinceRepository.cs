using System.Linq.Expressions;
using FinalProject.Application.Common.DataTransferObjects;
using FinalProject.Domain.Entities;
using FinalProject.Domain.Interfaces;

namespace FinalProject.Application.Common.Interfaces.Repositories;

public interface IProvinceRepository
{
    Task<int> Add(ProvinceDto model);
    Task<int> Update(ProvinceDto model);
    Task<int> Remove(int id);
    Task<ProvinceDto> GetById(int id);
    Task<IEnumerable<ProvinceDto>> GetAll();
}