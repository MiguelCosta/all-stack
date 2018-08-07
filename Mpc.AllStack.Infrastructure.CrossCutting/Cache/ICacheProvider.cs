namespace Mpc.AllStack.Infrastructure.CrossCutting.Cache
{
    using System;
    using System.Threading.Tasks;

    public interface ICacheProvider
    {
        Task<T> GetAsync<T>(string key);

        Task<T> GetAsync<T>(string key, Func<Task<T>> updateCacheHandler);

        Task<T> GetAsync<T>(string key, TimeSpan timeLifeSpanMinutes, Func<Task<T>> updateCacheHandler);

        Task SetAsync<T>(string key, T entry);
    }
}
