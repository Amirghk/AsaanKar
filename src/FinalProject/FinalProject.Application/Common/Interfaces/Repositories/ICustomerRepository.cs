using System.Linq.Expressions;
using FinalProject.Application.Common.DataTransferObjects;
using FinalProject.Domain.Entities;
using FinalProject.Domain.Interfaces;

namespace FinalProject.Application.Common.Interfaces.Repositories;

public interface ICustomerRepository
{
    Task<string> Add(CustomerDto model);
    Task<string> Update(CustomerDto model);
    Task<string> Remove(string id);
    Task<string> SoftDelete(string id);
    Task<CustomerDto> GetById(string id, CancellationToken cancellationToken);
    Task<IEnumerable<CustomerDto>> GetAll(CancellationToken cancellationToken);
}