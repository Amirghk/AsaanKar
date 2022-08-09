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
    public class ProvinceRepositoryCache : IProvinceRepositoryCache
    {
        private readonly IDistributedCache _cache;
        private readonly IConnectionMultiplexer _redis;
        private readonly AppSettings _appSettings;
        private readonly string PREFIX;

        public ProvinceRepositoryCache(
            IDistributedCache cache,
            IConnectionMultiplexer redis,
            IOptions<AppSettings> appSettings)
        {
            _cache = cache;
            _redis = redis;
            _appSettings = appSettings.Value;
            PREFIX = _appSettings.Caching.Prefixes.Province;
        }



        public async Task<IEnumerable<ProvinceDto>> GetAll(CancellationToken cancellationToken)
        {
            List<string>? redisKeys = _redis.GetServer(_appSettings.Caching.Host, _appSettings.Caching.Port).Keys(pattern: $"{PREFIX}*")
               .AsQueryable().Select(p => p.ToString()).ToList();

            if (!redisKeys.Any())
            {
                throw new CacheNotFoundException("Province", PREFIX);
            }

            var result = new List<ProvinceDto>();
            foreach (var redisKey in redisKeys)
            {
                result.Add(JsonSerializer.Deserialize<ProvinceDto>(await _cache.GetStringAsync(redisKey, cancellationToken))!);
            }
            return result;

        }

        public async Task<ProvinceDto> Get(int id, CancellationToken cancellationToken)
        {
            var category = await _cache.GetStringAsync($"{PREFIX}{id}", cancellationToken);
            if (category == null)
            {
                throw new CacheNotFoundException("Province", PREFIX + id);
            }
            return JsonSerializer.Deserialize<ProvinceDto>(category)!;
        }

        public async Task Set(IEnumerable<ProvinceDto> provinces)
        {
            // TODO : Get these from config
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpiration = DateTimeOffset.Now.AddDays(1)
            };


            foreach (var province in provinces)
            {
                var content = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(province));
                await _cache.SetAsync(PREFIX + province.Id, content, options);
            }
        }
    }
}
