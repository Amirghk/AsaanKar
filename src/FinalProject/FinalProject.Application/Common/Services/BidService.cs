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

public class BidService : IBidService
{
    private readonly IMapper _mapper;
    private readonly IBidRepository _repository;
    public BidService(IBidRepository repository, IMapper mapper)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<IEnumerable<BidDto>> GetAll()
    {
        List<BidDto> mappedModels = _mapper.Map<List<BidDto>>(await _repository.GetAll()).ToList();
        return mappedModels;
    }

    public async Task<BidDto> GetById(int id)
    {
        BidDto mappedModel = _mapper.Map<BidDto>(await _repository.GetById(id));
        return mappedModel;
    }

    public async Task<int> Remove(int id)
    {
        return await _repository.Remove(id);
    }

    public async Task<int> Set(BidDto dto)
    {
        return await _repository.Add(_mapper.Map<Bid>(dto));
    }

    public async Task<int> Update(BidDto dto)
    {
        return await _repository.Update(_mapper.Map<Bid>(dto));
    }
}

