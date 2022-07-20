using AutoMapper;
using FinalProject.Application.Common.Interfaces.Services;
using FinalProject.Domain.Dtos;
using FinalProject.Domain.Entities;
using FinalProject.Domain.Interfaces;

namespace FinalProject.Application.Common.Services;

public class ExpertService : IExpertService
{
    private readonly IMapper _mapper;
    private readonly IExpertRepository _repository;
    public ExpertService(IExpertRepository repository, IMapper mapper)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<IEnumerable<ExpertDto>> GetAll()
    {
        return await _repository.GetAll();
    }

    public async Task<ExpertDto> GetById(int id)
    {
        return await _repository.GetById(id);
    }

    public async Task<ExpertDto> GetByUserId(string userId)
    {
        return await _repository.GetByUserId(userId);
    }

    public async Task<int> Remove(int id)
    {
        return await _repository.Remove(id);
    }

    public async Task<int> Set(ExpertDto dto)
    {
        return await _repository.Add(dto);
    }

    public async Task<int> Update(ExpertDto dto)
    {
        return await _repository.Update(dto);
    }
}