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
    public class CityRepositoryCache : ICityRepositoryCache
    {
        private readonly IDistributedCache _cache;
        private readonly IConnectionMultiplexer _redis;
        private readonly AppSettings _appSettings;
        private readonly string PREFIX;

        public CityRepositoryCache(
            IDistributedCache cache,
            IConnectionMultiplexer redis,
            IOptions<AppSettings> appSettings)
        {
            _cache = cache;
            _redis = redis;
            _appSettings = appSettings.Value;
            PREFIX = _appSettings.Caching.Prefixes.City;
        }



        public async Task<IEnumerable<CityDto>> GetAll(CancellationToken cancellationToken)
        {
            List<string>? redisKeys = _redis.GetServer(_appSettings.Caching.Host, _appSettings.Caching.Port).Keys(pattern: $"{PREFIX}*")
               .AsQueryable().Select(p => p.ToString()).ToList();

            if (!redisKeys.Any())
            {
                throw new CacheNotFoundException("City", PREFIX);
            }

            var result = new List<CityDto>();
            foreach (var redisKey in redisKeys)
            {
                result.Add(JsonSerializer.Deserialize<CityDto>(await _cache.GetStringAsync(redisKey, cancellationToken))!);
            }
            return result;

        }

        public async Task<CityDto> Get(int id, CancellationToken cancellationToken)
        {
            var category = await _cache.GetStringAsync($"{PREFIX}{id}", cancellationToken);
            if (category == null)
            {
                throw new CacheNotFoundException("City", PREFIX + id);
            }
            return JsonSerializer.Deserialize<CityDto>(category)!;
        }

        public async Task Set(IEnumerable<CityDto> services)
        {
            // TODO : Get these from config
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpiration = DateTimeOffset.Now.AddDays(1)
            };


            foreach (var service in services)
            {
                var content = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(service));
                await _cache.SetAsync(PREFIX + service.Id, content, options);
            }
        }

        public async Task Set(CityDto cityDto)
        {
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpiration = DateTimeOffset.Now.AddDays(1)
            };


            var content = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(cityDto));
            await _cache.SetAsync(PREFIX + cityDto.Id, content, options);
        }

        public async Task Delete(int id)
        {
            await _cache.RemoveAsync(PREFIX + id);
        }
    }
}
