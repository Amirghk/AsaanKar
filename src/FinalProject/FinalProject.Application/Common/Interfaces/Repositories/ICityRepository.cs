using System.Linq.Expressions;
using FinalProject.Application.Common.DataTransferObjects;
using FinalProject.Domain.Entities;
using FinalProject.Domain.Interfaces;

namespace FinalProject.Application.Common.Interfaces.Repositories;

public interface ICityRepository
{
    Task<int> Add(CityDto model);
    Task<int> Update(CityDto model);
    Task<int> Remove(int id);
    Task<CityDto> GetById(int id, CancellationToken cancellationToken);
    Task<IEnumerable<CityDto>> GetAll(CancellationToken cancellationToken);
}