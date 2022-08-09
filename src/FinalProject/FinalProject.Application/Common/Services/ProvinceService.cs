using AutoMapper;
using FinalProject.Application.Common.Interfaces.Services;
using FinalProject.Application.Common.DataTransferObjects;
using FinalProject.Application.Common.Interfaces.Repositories;
using FinalProject.Application.Common.Interfaces.CacheRepositories;
using Microsoft.Extensions.Logging;
using FinalProject.Application.Common.Exceptions;

namespace FinalProject.Application.Common.Services;

public class ProvinceService : IProvinceService
{
    private readonly IMapper _mapper;
    private readonly IProvinceRepositoryCache _repositoryCache;
    private readonly ILogger<ProvinceService> _logger;
    private readonly IProvinceRepository _repository;
    public ProvinceService(IProvinceRepository repository,
                           IMapper mapper,
                           IProvinceRepositoryCache repositoryCache,
                           ILogger<ProvinceService> logger)
    {
        _mapper = mapper;
        _repositoryCache = repositoryCache;
        _logger = logger;
        _repository = repository;

    }

    public async Task<IEnumerable<ProvinceDto>> GetAll(CancellationToken cancellationToken)
    {
        try
        {
            var provinces = await _repositoryCache.GetAll(cancellationToken);
            return provinces;
        }
        catch (CacheNotFoundException)
        {
            _logger.LogInformation("Province Cache was not found! in {method}", nameof(_repositoryCache.GetAll));

            var provinces = await _repository.GetAll(cancellationToken);
            await _repositoryCache.Set(provinces);
            return provinces;
        }
    }

    public async Task<ProvinceDto> GetById(int id, CancellationToken cancellationToken)
    {
        try
        {
            var province = await _repositoryCache.Get(id, cancellationToken);
            return province;
        }
        catch (CacheNotFoundException)
        {
            _logger.LogInformation("Province Cache was not found! in {method}", nameof(_repositoryCache.Get));
        }
        return await _repository.GetById(id, cancellationToken);
    }

    public async Task<int> Remove(int id, CancellationToken cancellationToken)
    {
        return await _repository.Remove(id);
    }

    public async Task<int> Set(ProvinceDto dto, CancellationToken cancellationToken)
    {
        return await _repository.Add(dto);
    }

    public async Task<int> Update(ProvinceDto dto, CancellationToken cancellationToken)
    {
        return await _repository.Update(dto);
    }
}