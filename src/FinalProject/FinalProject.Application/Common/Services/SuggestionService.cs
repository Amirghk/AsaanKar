using AutoMapper;
using FinalProject.Application.Common.Dtos;
using FinalProject.Application.Common.Interfaces.Services;
using FinalProject.Domain.Entities;
using FinalProject.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Application.Common.Services;

public class SuggestionService : ISuggestionService
{
    private readonly IMapper _mapper;
    private readonly ISuggestionRepository _repository;
    public SuggestionService(ISuggestionRepository repository, IMapper mapper)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<IEnumerable<SuggestionDto>> GetAll()
    {
        List<SuggestionDto> mappedModels = _mapper.Map<List<SuggestionDto>>(await _repository.GetAll()).ToList();
        return mappedModels;
    }

    public async Task<SuggestionDto> GetById(int id)
    {
        SuggestionDto mappedModel = _mapper.Map<SuggestionDto>(await _repository.GetById(id));
        return mappedModel;
    }

    public async Task<int> Remove(int id)
    {
        return await _repository.Remove(id);
    }

    public async Task<int> Set(SuggestionDto dto)
    {
        return await _repository.Add(_mapper.Map<Suggestion>(dto));
    }

    public async Task<int> Update(SuggestionDto dto)
    {
        return await _repository.Update(_mapper.Map<Suggestion>(dto));
    }
}

