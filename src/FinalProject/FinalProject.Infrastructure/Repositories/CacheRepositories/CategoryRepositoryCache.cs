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
        private readonly string CATEGORYPREFIX;

        public CategoryRepositoryCache(
            IDistributedCache cache,
            IConnectionMultiplexer redis,
            IOptions<AppSettings> appSettings)
        {
            _cache = cache;
            _redis = redis;
            _appSettings = appSettings.Value;
            CATEGORYPREFIX = _appSettings.Caching.Prefixes.Category;
        }


        public async Task<IEnumerable<CategoryDto>> GetAll(CancellationToken cancellationToken)
        {
            List<string>? redisKeys = _redis.GetServer(_appSettings.Caching.Host, _appSettings.Caching.Port).Keys(pattern: $"{CATEGORYPREFIX}*")
               .AsQueryable().Select(p => p.ToString()).ToList();

            if (!redisKeys.Any())
            {
                throw new CacheNotFoundException("Category", CATEGORYPREFIX);
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
            var category = await _cache.GetStringAsync($"{CATEGORYPREFIX}{id}", cancellationToken);
            if (category == null)
            {
                throw new CacheNotFoundException("Category", CATEGORYPREFIX + id);
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
                await _cache.SetAsync(CATEGORYPREFIX + category.Id, content, options);
            }
        }

        public async Task<IEnumerable<CategoryDto>> GetChildren(int id, CancellationToken cancellationToken)
        {
            List<string>? redisKeys = _redis.GetServer(_appSettings.Caching.Host, _appSettings.Caching.Port).Keys(pattern: "Category_*")
                    .AsQueryable().Select(p => p.ToString()).ToList();

            if (!redisKeys.Any())
            {
                throw new CacheNotFoundException("Category", CATEGORYPREFIX);
            }

            var result = new List<CategoryDto>();
            foreach (var redisKey in redisKeys)
            {
                result.Add(JsonSerializer.Deserialize<CategoryDto>(await _cache.GetStringAsync(redisKey, cancellationToken))!);
            }

            return result.Where(x => x.ParentCategoryId == id).ToList();
        }
    }
}
