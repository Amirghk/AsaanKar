using AutoMapper;
using FinalProject.Application.Common.Dtos;
using FinalProject.Application.Common.Interfaces.Services;
using FinalProject.Domain.Entities;
using FinalProject.Domain.Interfaces;

namespace FinalProject.Application.Common.Services;

public class AddressService : IAddressService
{
    private readonly IMapper _mapper;
    private readonly IAddressRepository _repository;
    public AddressService(IAddressRepository repository, IMapper mapper)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<IEnumerable<AddressDto>> GetAll()
    {
        List<AddressDto> mappedModels = _mapper.Map<List<AddressDto>>(await _repository.GetAll()).ToList();
        return mappedModels;
    }

    public async Task<AddressDto> GetById(int id)
    {
        AddressDto mappedModel = _mapper.Map<AddressDto>(await _repository.GetById(id));
        return mappedModel;
    }

    public async Task<int> Remove(int id)
    {
        return await _repository.Remove(id);
    }

    public async Task<int> Set(AddressDto dto)
    {
        return await _repository.Add(_mapper.Map<Address>(dto));
    }

    public async Task<int> Update(AddressDto dto)
    {
        return await _repository.Update(_mapper.Map<Address>(dto));
    }
}