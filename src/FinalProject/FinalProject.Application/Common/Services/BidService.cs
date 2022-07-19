using AutoMapper;
using FinalProject.Application.Common.Interfaces.Services;
using FinalProject.Domain.Dtos;
using FinalProject.Domain.Entities;
using FinalProject.Domain.Interfaces;

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
        return await _repository.GetAll();
    }

    public async Task<BidDto> GetById(int id)
    {
        return await _repository.GetById(id);
    }

    public async Task<int> Remove(int id)
    {
        return await _repository.Remove(id);
    }

    public async Task<int> Set(BidDto dto)
    {
        return await _repository.Add(dto);
    }

    public async Task<int> Update(BidDto dto)
    {
        return await _repository.Update(dto);
    }
}