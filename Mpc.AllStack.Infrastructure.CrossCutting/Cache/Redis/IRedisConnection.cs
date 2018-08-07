namespace Mpc.AllStack.Infrastructure.CrossCutting.Cache.Redis
{
    using StackExchange.Redis;
    using StackExchange.Redis.Extensions.Core;

    public interface IRedisConnection
    {
        IDatabase Database { get; }

        string KeyPrefix { get; }

        ISerializer Serializer { get; }

        void Close();
    }
}
