using AutoMapper;
using FinalProject.Application.Common.Interfaces.Services;
using FinalProject.Application.Common.DataTransferObjects;
using FinalProject.Application.Common.Interfaces.Repositories;

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
        return await _repository.GetAll();
    }

    public async Task<AddressDto> GetById(int id)
    {
        return await _repository.GetById(id);
    }

    public async Task<IEnumerable<AddressDto>> GetByUserId(int userId)
    {
        return await _repository.GetByUserId(userId);
    }

    public async Task<int> Remove(int id)
    {
        return await _repository.Remove(id);
    }

    public async Task<int> Set(AddressDto dto)
    {
        return await _repository.Add(dto);
    }

    public async Task<int> Update(AddressDto dto)
    {
        return await _repository.Update(dto);
    }
}