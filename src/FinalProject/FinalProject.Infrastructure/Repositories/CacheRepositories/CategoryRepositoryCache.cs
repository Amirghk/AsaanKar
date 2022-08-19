using FinalProject.Application.Common.ConfigurationModels;
using FinalProject.Application.Common.DataTransferObjects;
using FinalProject.Application.Common.Exceptions;
using FinalProject.Application.Common.Interfaces.CacheRepositories;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using System.Text;
using System.Text.Json;

namespace FinalProject.Infrastructure.Repositories
{
    public class CategoryRepositoryCache : ICategoryRepositoryCache
    {
        private readonly IDistributedCache _cache;
        private readonly IConnectionMultiplexer _redis;
        private readonly AppSettings _appSettings;
        private readonly string PREFIX;

        public CategoryRepositoryCache(
            IDistributedCache cache,
            IConnectionMultiplexer redis,
            IOptions<AppSettings> appSettings)
        {
            _cache = cache;
            _redis = redis;
            _appSettings = appSettings.Value;
            PREFIX = _appSettings.Caching.Prefixes.Category;
        }


        public async Task<IEnumerable<CategoryDto>> GetAll(CancellationToken cancellationToken)
        {
            List<string>? redisKeys = _redis.GetServer(_appSettings.Caching.Host, _appSettings.Caching.Port).Keys(pattern: $"{PREFIX}*")
               .AsQueryable().Select(p => p.ToString()).ToList();

            if (!redisKeys.Any())
            {
                throw new CacheNotFoundException("Category", PREFIX);
            }

            var result = new List<CategoryDto>();
            foreach (var redisKey in redisKeys)
            {
                result.Add(JsonSerializer.Deserialize<CategoryDto>(await _cache.GetStringAsync(redisKey, cancellationToken))!);
            }
            return result;

        }

        public async Task<CategoryDto> Get(int id, CancellationToken cancellationToken)
        {
            var category = await _cache.GetStringAsync($"{PREFIX}{id}", cancellationToken);
            if (category == null)
            {
                throw new CacheNotFoundException("Category", PREFIX + id);
            }
            return JsonSerializer.Deserialize<CategoryDto>(category)!;
        }

        public async Task Set(IEnumerable<CategoryDto> categories)
        {
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpiration = DateTimeOffset.Now.AddDays(1)
            };


            foreach (var category in categories)
            {
                var content = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(category));
                await _cache.SetAsync(PREFIX + category.Id, content, options);
            }
        }

        public async Task<IEnumerable<CategoryDto>> GetChildren(int id, CancellationToken cancellationToken)
        {
            List<string>? redisKeys = _redis.GetServer(_appSettings.Caching.Host, _appSettings.Caching.Port).Keys(pattern: $"{PREFIX}*")
                    .AsQueryable().Select(p => p.ToString()).ToList();

            if (!redisKeys.Any())
            {
                throw new CacheNotFoundException("Category", PREFIX);
            }

            var result = new List<CategoryDto>();
            foreach (var redisKey in redisKeys)
            {
                result.Add(JsonSerializer.Deserialize<CategoryDto>(await _cache.GetStringAsync(redisKey, cancellationToken))!);
            }

            return result.Where(x => x.ParentCategoryId == id).ToList();
        }

        public async Task Set(CategoryDto categoryDto)
        {
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpiration = DateTimeOffset.Now.AddDays(1)
            };


            var content = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(categoryDto));
            await _cache.SetAsync(PREFIX + categoryDto.Id, content, options);
        }

        public async Task Delete(int id)
        {
            await _cache.RemoveAsync(PREFIX + id);
        }
    }
}
