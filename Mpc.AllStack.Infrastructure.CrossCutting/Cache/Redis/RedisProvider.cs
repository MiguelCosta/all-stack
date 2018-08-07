namespace Mpc.AllStack.Infrastructure.CrossCutting.Cache.Redis
{
    using System;
    using System.Threading.Tasks;

    public class RedisProvider : ICacheProvider
    {
        private readonly IRedisConnection connection;

        public RedisProvider(IRedisConnection redisConnection)
        {
            this.connection = redisConnection;
        }

        public async Task<T> GetAsync<T>(string key)
        {
            var valueBytes = await this.connection.Database
                .StringGetAsync(this.connection.KeyPrefix + key)
                .ConfigureAwait(false);

            if (!valueBytes.HasValue)
            {
                return default(T);
            }

            var entry = await this.connection.Serializer
                .DeserializeAsync<T>(valueBytes)
                .ConfigureAwait(false);

            return entry;
        }

        public async Task<T> GetAsync<T>(string key, Func<Task<T>> updateCacheHandler)
        {
            var valueBytes = await this.connection.Database
                .StringGetAsync(this.connection.KeyPrefix + key)
                .ConfigureAwait(false);

            if (valueBytes.HasValue)
            {
                return this.connection.Serializer.Deserialize<T>(valueBytes);
            }

            var data = await updateCacheHandler().ConfigureAwait(false);
            await this.SetAsync(key, data).ConfigureAwait(false);

            return data;
        }

        public async Task<T> GetAsync<T>(string key, TimeSpan timeLifeSpanMinutes, Func<Task<T>> updateCacheHandler)
        {
            var valueBytes = await this.connection.Database
                .StringGetAsync(this.connection.KeyPrefix + key)
                .ConfigureAwait(false);

            if (valueBytes.HasValue)
            {
                return this.connection.Serializer.Deserialize<T>(valueBytes);
            }

            var data = await updateCacheHandler().ConfigureAwait(false);

            var entryBytes = await this.connection.Serializer
                .SerializeAsync(data)
                .ConfigureAwait(false);

            await this.connection.Database
                .StringSetAsync(this.connection.KeyPrefix + key, entryBytes, timeLifeSpanMinutes)
                .ConfigureAwait(false);

            return data;
        }

        public async Task SetAsync<T>(string key, T entry)
        {
            var entryBytes = await this.connection.Serializer
                .SerializeAsync(entry)
                .ConfigureAwait(false);

            await this.connection.Database
                .StringSetAsync(this.connection.KeyPrefix + key, entryBytes)
                .ConfigureAwait(false);
        }
    }
}
