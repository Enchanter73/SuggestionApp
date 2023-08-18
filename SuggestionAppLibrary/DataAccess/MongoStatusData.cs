
using Microsoft.Extensions.Caching.Memory;

namespace SuggestionAppLibrary.DataAccess
{
    public class MongoStatusData : IStatusData
    {
        private readonly IMongoCollection<StatusModel> _statuses;
        private readonly IMemoryCache _cache;
        private const string CacheName = "StatusData";

        public MongoStatusData(IDbConnection db, IMemoryCache cache)
        {
            _statuses = db.StatusCollection;
            _cache = cache;
        }

        public async Task<List<StatusModel>> GetAllStatuses()
        {
            var output = _cache.Get<List<StatusModel>>(CacheName);

            if (output != null)
            {
                return output;
            }

            var result = await _statuses.FindAsync(_ => true);

            if (result != null)
            {
                _cache.Set(CacheName, result, TimeSpan.FromDays(1));
            }

            return result.ToList();
        }

        public Task CreateStatus(StatusModel status)
        {
            return _statuses.InsertOneAsync(status);
        }
    }
}
