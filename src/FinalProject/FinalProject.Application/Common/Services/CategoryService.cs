using AutoMapper;
using FinalProject.Application.Common.Interfaces.Services;
using FinalProject.Application.Common.DataTransferObjects;
using FinalProject.Application.Common.Interfaces.Repositories;
using Microsoft.Extensions.Caching.Distributed;
using System.Text;
using System.Text.Json;
using StackExchange.Redis;
using FinalProject.Application.Common.Interfaces.CacheRepositories;
using FinalProject.Application.Common.Exceptions;
using Microsoft.Extensions.Logging;

namespace FinalProject.Application.Common.Services;

public class CategoryService : ICategoryService
{
    private readonly IMapper _mapper;
    private readonly IUploadRepository _uploadRepository;
    private readonly ICategoryRepositoryCache _repositoryCache;
    private readonly ILogger _logger;
    private readonly ICategoryRepository _repository;
    public CategoryService(
        ICategoryRepository repository,
        IMapper mapper,
        IUploadRepository uploadRepository,
        ICategoryRepositoryCache repositoryCache,
        ILogger<CategoryService> logger
        )
    {
        _mapper = mapper;
        _uploadRepository = uploadRepository;
        _repositoryCache = repositoryCache;
        _logger = logger;
        _repository = repository;
    }

    public async Task<IEnumerable<CategoryDto>> GetAll(CancellationToken cancellationToken)
    {
        try
        {
            var categories = await _repositoryCache.GetAll(cancellationToken);
            return categories;
        }
        catch (CacheNotFoundException)
        {
            _logger.LogInformation("Category Cache was not found! in {method}", nameof(_repositoryCache.GetAll));

            var categories = await _repository.GetAll(cancellationToken);
            await _repositoryCache.Set(categories);
            return categories;
        }
    }

    public async Task<CategoryDto> GetById(int id, CancellationToken cancellationToken)
    {
        try
        {
            var category = await _repositoryCache.Get(id, cancellationToken);
            return category;
        }
        catch (CacheNotFoundException)
        {
            _logger.LogInformation("Category Cache was not found! in {method}", nameof(_repositoryCache.Get));
        }
        return await _repository.GetById(id, cancellationToken);
    }

    public async Task<IEnumerable<CategoryDto>> GetChildren(int id, CancellationToken cancellationToken)
    {
        try
        {
            var categories = await _repositoryCache.GetChildren(id, cancellationToken);
            return categories;
        }
        catch (CacheNotFoundException)
        {
            _logger.LogInformation("Category Cache was not found! in {method}", nameof(_repositoryCache.GetChildren));

            var categories = await _repository.GetAll(cancellationToken);
            await _repositoryCache.Set(categories);
        }

        return await _repository.GetChildren(id, cancellationToken);
    }

    public async Task<int> Remove(int id, CancellationToken cancellationToken)
    {
        var category = await _repository.GetById(id, cancellationToken);
        if (category.PictureId != null)
        {
            await _uploadRepository.Remove((int)category.PictureId);
        }
        return await _repository.Remove(id);
    }

    public async Task<int> Set(CategoryDto dto, CancellationToken cancellationToken)
    {
        return await _repository.Add(dto);
    }


    public async Task<int> Update(CategoryDto dto, CancellationToken cancellationToken)
    {
        return await _repository.Update(dto);
    }
}