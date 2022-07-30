using AutoMapper;
using FinalProject.Application.Common.Interfaces.Services;
using FinalProject.Application.Common.DataTransferObjects;
using FinalProject.Application.Common.Interfaces.Repositories;

namespace FinalProject.Application.Common.Services;

public class AddressService : IAddressService
{
    private readonly IMapper _mapper;
    private readonly ICityRepository _cityRepository;
    private readonly IProvinceService _provinceService;
    private readonly IAddressRepository _repository;
    public AddressService(IAddressRepository repository,
        IMapper mapper,
        ICityRepository cityRepository,
        IProvinceService provinceService)
    {
        _mapper = mapper;
        _cityRepository = cityRepository;
        _provinceService = provinceService;
        _repository = repository;
    }

    public async Task<IEnumerable<AddressDto>> GetAll(CancellationToken cancellationToken)
    {
        return await _repository.GetAll(cancellationToken);
    }

    public async Task<AddressDto> GetById(int id, CancellationToken cancellationToken)
    {
        return await _repository.GetById(id, cancellationToken);
    }

    public async Task<IEnumerable<AddressDto>> GetByUserId(string userId, CancellationToken cancellationToken)
    {
        return await _repository.GetByUserId(userId, cancellationToken);
    }

    public async Task<string> GetFullAddressToString(int id, CancellationToken cancellationToken)
    {
        var address = await _repository.GetById(id, cancellationToken);
        var city = await _cityRepository.GetById(address.CityId, cancellationToken);
        var cityName = city.Name;
        var ProvinceName = (await _provinceService.GetById(city.ProvinceId, cancellationToken)).Name;
        var content = address.Content;
        var postalCode = address.PostalCode;

        return $"{ProvinceName} - {cityName} - {content}  -  PostalCode : {postalCode}";
    }

    public async Task<int> Remove(int id, CancellationToken cancellationToken)
    {
        return await _repository.Remove(id);
    }

    public async Task<int> Set(AddressDto dto, CancellationToken cancellationToken)
    {
        return await _repository.Add(dto);
    }

    public async Task<int> Update(AddressDto dto, CancellationToken cancellationToken)
    {
        return await _repository.Update(dto);
    }
}