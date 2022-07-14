using AutoMapper;
using FinalProject.Application.Common.Dtos;
using FinalProject.Application.Common.Interfaces.Services;
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
        List<ExpertDto> mappedModels = _mapper.Map<List<ExpertDto>>(await _repository.GetAll()).ToList();
        return mappedModels;
    }

    public async Task<ExpertDto> GetById(int id)
    {
        ExpertDto mappedModel = _mapper.Map<ExpertDto>(await _repository.GetById(id));
        return mappedModel;
    }

    public async Task<int> Remove(int id)
    {
        return await _repository.Remove(id);
    }

    public async Task<int> Set(ExpertDto dto)
    {
        return await _repository.Add(_mapper.Map<Expert>(dto));
    }

    public async Task<int> Update(ExpertDto dto)
    {
        return await _repository.Update(_mapper.Map<Expert>(dto));
    }
}