using AutoMapper;
using FinalProject.Application.Common.Interfaces.Services;
using FinalProject.Application.Common.DataTransferObjects;
using FinalProject.Application.Common.Interfaces.Repositories;
using FinalProject.Application.Common.Exceptions;
using FinalProject.Application.Common.Interfaces.CacheRepositories;
using Microsoft.Extensions.Logging;

namespace FinalProject.Application.Common.Services;

public class CityService : ICityService
{
    private readonly IMapper _mapper;
    private readonly ICityRepositoryCache _repositoryCache;
    private readonly ILogger<CityService> _logger;
    private readonly ICityRepository _repository;
    public CityService(ICityRepository repository, IMapper mapper, ICityRepositoryCache repositoryCache, ILogger<CityService> logger)
    {
        _mapper = mapper;
        _repositoryCache = repositoryCache;
        _logger = logger;
        _repository = repository;
    }

    public async Task<IEnumerable<CityDto>> GetAll(CancellationToken cancellationToken)
    {
        try
        {
            var cities = await _repositoryCache.GetAll(cancellationToken);
            return cities;
        }
        catch (CacheNotFoundException)
        {
            _logger.LogInformation("City Cache was not found! in {method}", nameof(_repositoryCache.GetAll));

            var cities = await _repository.GetAll(cancellationToken);
            await _repositoryCache.Set(cities);
            return cities;
        }
    }

    public async Task<CityDto> GetById(int id, CancellationToken cancellationToken)
    {
        try
        {
            var city = await _repositoryCache.Get(id, cancellationToken);
            return city;
        }
        catch (CacheNotFoundException)
        {
            _logger.LogInformation("City Cache was not found! in {method}", nameof(_repositoryCache.Get));
        }
        return await _repository.GetById(id, cancellationToken);
    }

    public async Task<int> Remove(int id, CancellationToken cancellationToken)
    {
        return await _repository.Remove(id);
    }

    public async Task<int> Set(CityDto dto, CancellationToken cancellationToken)
    {
        return await _repository.Add(dto);
    }

    public async Task<int> Update(CityDto dto, CancellationToken cancellationToken)
    {
        return await _repository.Update(dto);
    }
}