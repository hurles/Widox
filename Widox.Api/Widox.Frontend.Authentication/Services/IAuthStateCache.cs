using Widox.Models.Frontend;

namespace Widox.Frontend.Authentication.Services
{
    public interface IAuthStateCache
    {
        void Add(string subjectId, DateTimeOffset expiration, string accessToken, string refreshToken);
        AuthStateCacheInstance? GetAuthStateFromCache(string subjectId);
        bool HasSubjectId(string subjecTId);
        void Remove(string subjectId);
    }
}