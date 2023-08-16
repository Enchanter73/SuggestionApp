
using Microsoft.Extensions.Caching.Memory;

namespace SuggestionAppLibrary.DataAccess
{
    public class MongoCategoryData : ICategoryData
    {
        private readonly IMongoCollection<CategoryModel> _categories;
        private readonly IMemoryCache _cache;
        private const string cacheName = "CategoryData";

        public MongoCategoryData(IDbConnection db, IMemoryCache cache)
        {
            _cache = cache;
            _categories = db.CategoryCollection;
        }

        public async Task<List<CategoryModel>> GetAllCategories()
        {
            var output = _cache.Get<List<CategoryModel>>(cacheName);

            if (output != null)
            {
                return output;
            }

            var result = await _categories.FindAsync(_ => true);

            if (result != null)
            {
                _cache.Set(cacheName, result, TimeSpan.FromDays(1));
            }

            return result.ToList();
        }

        public Task CreateCategory(CategoryModel category)
        {
            return _categories.InsertOneAsync(category);
        }
    }
}
