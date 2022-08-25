using System.Collections.Concurrent;
using Widox.Models.Frontend;

namespace Widox.Frontend.Authentication.Services
{
    public class AuthStateCache : IAuthStateCache
    {
        private ConcurrentDictionary<string, AuthStateCacheInstance> _cache = new();

        public bool HasSubjectId(string subjecTId) => _cache.ContainsKey(subjecTId);

        public void Add(string subjectId, DateTimeOffset expiration, string accessToken, string refreshToken)
        {
            var data = new AuthStateCacheInstance()
            {
                SubjectId = subjectId,
                Expiration = expiration,
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
            _cache.AddOrUpdate(subjectId, data, (x, y) => data);
        }

        public AuthStateCacheInstance? GetAuthStateFromCache(string subjectId)
        {
            _cache.TryGetValue(subjectId, out var data);
            return data;
        }

        public void Remove(string subjectId)
        {
            _cache.TryRemove(subjectId, out _);
        }
    }
}