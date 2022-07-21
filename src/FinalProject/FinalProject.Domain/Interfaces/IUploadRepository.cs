using System.Linq.Expressions;
using FinalProject.Domain.Dtos;
using FinalProject.Domain.Entities;
using FinalProject.Domain.Interfaces;

namespace FinalProject.Domain.Interfaces;

public interface IUploadRepository
{
    Task<int> Add(UploadDto model);
    Task<int> Update(UploadDto model);
    Task<int> Remove(int id);
    Task<UploadDto> GetById(int id);
    Task<IEnumerable<UploadDto>> GetAll();
}