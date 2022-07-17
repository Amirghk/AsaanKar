using AutoMapper;
using FinalProject.Application.Common.Dtos;
using FinalProject.Application.Common.Interfaces.Services;
using FinalProject.Domain.Entities;
using FinalProject.Domain.Interfaces;

namespace FinalProject.Application.Common.Services;

public class SubServiceService : ISubServiceService
{
    private readonly IMapper _mapper;
    private readonly ISubServiceRepository _repository;
    public SubServiceService(ISubServiceRepository repository, IMapper mapper)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<IEnumerable<SubServiceDto>> GetAll()
    {
        List<SubServiceDto> mappedModels = _mapper.Map<List<SubServiceDto>>(await _repository.GetAll()).ToList();
        return mappedModels;
    }

    public async Task<SubServiceDto> GetById(int id)
    {
        SubServiceDto mappedModel = _mapper.Map<SubServiceDto>(await _repository.GetById(id));
        return mappedModel;
    }

    public async Task<int> Remove(int id)
    {
        return await _repository.Remove(id);
    }

    public async Task<int> Set(SubServiceDto dto)
    {
        return await _repository.Add(_mapper.Map<Service>(dto));
    }

    public async Task<int> Update(SubServiceDto dto)
    {
        return await _repository.Update(_mapper.Map<Service>(dto));
    }
}