using StackExchange.Redis;
using System.Text.Json;

namespace Gorbilet.TestTask.Services
{
    public class RedisService
    {
        private readonly IDatabase _database;

        public RedisService(IConnectionMultiplexer connectionMultiplexer)
        {
            _database = connectionMultiplexer.GetDatabase();
        }

        public async Task SetValueAsync<T>(string key, T value)
        {
            var json = JsonSerializer.Serialize(value);
            await _database.StringSetAsync(key, json);
        }

        public async Task<T?> GetValueAsync<T>(string key)
        {
            var json = await _database.StringGetAsync(key);
            if (json.HasValue)
            {
                return JsonSerializer.Deserialize<T>(json);
            }
            else
            {
                return default;
            }
        }

        public async Task<bool> UpdateValueAsync<T>(string key, T value)
        {
            if (await _database.KeyExistsAsync(key))
            {
                await _database.StringSetAsync(key, JsonSerializer.Serialize(value));
                return true;
            }
            return false;
        }

        public Task<bool> DeleteValueAsync<T>(string key)
        {
            return _database.KeyDeleteAsync(key);
        }
    }
}
