using AutoMapper;
using FinalProject.Application.Common.Interfaces.Services;
using FinalProject.Domain.Dtos;
using FinalProject.Domain.Entities;
using FinalProject.Domain.Interfaces;

namespace FinalProject.Application.Common.Services;

public class UploadService : IUploadService
{
    private readonly IMapper _mapper;
    private readonly IUploadRepository _repository;
    public UploadService(IUploadRepository repository, IMapper mapper)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<IEnumerable<UploadDto>> GetAll()
    {
        return await _repository.GetAll();
    }

    public async Task<UploadDto> GetById(int id)
    {
        return await _repository.GetById(id);
    }

    public async Task<int> Remove(int id)
    {
        return await _repository.Remove(id);
    }

    public async Task<int> Set(UploadDto dto)
    {
        return await _repository.Add(dto);
    }

    public async Task<int> Update(UploadDto dto)
    {
        return await _repository.Update(dto);
    }
}