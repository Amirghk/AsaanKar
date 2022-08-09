using AutoMapper;
using FinalProject.Application.Common.Interfaces.Services;
using FinalProject.Application.Common.DataTransferObjects;
using FinalProject.Application.Common.Interfaces.Repositories;
using Microsoft.Extensions.Caching.Distributed;
using System.Text;
using System.Text.Json;
using StackExchange.Redis;

namespace FinalProject.Application.Common.Services;

public class CategoryService : ICategoryService
{
    private readonly IMapper _mapper;
    private readonly IUploadRepository _uploadRepository;
    private readonly IDistributedCache _cache;
    private readonly IConnectionMultiplexer _redis;
    private readonly ICategoryRepository _repository;
    public CategoryService(
        ICategoryRepository repository,
        IMapper mapper,
        IUploadRepository uploadRepository,
        IDistributedCache cache,
        IConnectionMultiplexer redis)
    {
        _mapper = mapper;
        _uploadRepository = uploadRepository;
        _cache = cache;
        _redis = redis;
        _repository = repository;
    }

    public async Task<IEnumerable<CategoryDto>> GetAll(CancellationToken cancellationToken)
    {
        // why use AsQueryable

        List<string>? redisKeys = _redis.GetServer("localhost", 6379).Keys(pattern: "Category_*")
                .AsQueryable().Select(p => p.ToString()).ToList();

        if (!redisKeys.Any())
            await SetCache(cancellationToken);

        var result = new List<CategoryDto>();
        foreach (var redisKey in redisKeys)
        {
            result.Add(JsonSerializer.Deserialize<CategoryDto>(await _cache.GetStringAsync(redisKey))!);
        }
        return result;

        // return await _repository.GetAll(cancellationToken);
    }

    public async Task<CategoryDto> GetById(int id, CancellationToken cancellationToken)
    {
        var category = await _cache.GetStringAsync($"Category_{id}");
        if (category != null)
            return JsonSerializer.Deserialize<CategoryDto>(category)!;
        return await _repository.GetById(id, cancellationToken);
    }

    public async Task<IEnumerable<CategoryDto>> GetChildren(int id, CancellationToken cancellationToken)
    {
        //List<string>? redisKeys = _redis.GetServer("localhost", 6379).Keys(pattern: "Category_*")
        //        .AsQueryable().Select(p => p.ToString()).ToList();

        //if (!redisKeys.Any())
        //    await SetCache(cancellationToken);

        //var result = new List<CategoryDto>();
        //foreach (var redisKey in redisKeys)
        //{
        //    result.Add(JsonSerializer.Deserialize<CategoryDto>(await _cache.GetStringAsync(redisKey))!);
        //}

        //return result.Where(x => x.ParentCategoryId == id).ToList();


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
    private async Task SetCache(CancellationToken cancellationToken)
    {
        var categories = await _repository.GetAll(cancellationToken);

        // TODO : Get these from config
        var options = new DistributedCacheEntryOptions
        {
            AbsoluteExpiration = DateTimeOffset.Now.AddDays(1)
        };


        foreach (var category in categories)
        {
            var content = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(category));
            await _cache.SetAsync("Category_" + category.Id, content, options);
        }
    }
}