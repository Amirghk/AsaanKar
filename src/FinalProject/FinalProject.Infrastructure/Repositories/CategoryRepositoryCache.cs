using FinalProject.Application.Common.ConfigurationModels;
using FinalProject.Application.Common.DataTransferObjects;
using FinalProject.Application.Common.Exceptions;
using FinalProject.Application.Common.Interfaces.CacheRepositories;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

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
            CATEGORYPREFIX = "Category_";
        }


        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<CategoryDto>> GetAll()
        {
            // TODO : move ids to config (Category_) 
            List<string>? redisKeys = _redis.GetServer(_appSettings.Caching.Host, _appSettings.Caching.Port).Keys(pattern: $"{CATEGORYPREFIX}*")
               .AsQueryable().Select(p => p.ToString()).ToList();

            if (!redisKeys.Any())
            {
                throw new CacheNotFoundException("Category", CATEGORYPREFIX);
            }

            var result = new List<CategoryDto>();
            foreach (var redisKey in redisKeys)
            {
                result.Add(JsonSerializer.Deserialize<CategoryDto>(await _cache.GetStringAsync(redisKey))!);
            }
            return result;

        }

        public async Task<CategoryDto> Get(int id)
        {
            var category = await _cache.GetStringAsync($"{CATEGORYPREFIX}{id}");
            if (category == null)
            {
                throw new CacheNotFoundException("Category", CATEGORYPREFIX + id);
            }
            return JsonSerializer.Deserialize<CategoryDto>(category)!;
        }

        public async Task Set(IEnumerable<CategoryDto> categories)
        {
            // TODO : Get these from config
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

        public async Task<IEnumerable<CategoryDto>> GetChildren(int id)
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
                result.Add(JsonSerializer.Deserialize<CategoryDto>(await _cache.GetStringAsync(redisKey))!);
            }

            return result.Where(x => x.ParentCategoryId == id).ToList();
        }
    }
}
