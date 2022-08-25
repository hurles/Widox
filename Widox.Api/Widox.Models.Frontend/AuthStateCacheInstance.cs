namespace Widox.Models.Frontend
{
    public class AuthStateCacheInstance
    {
        public string SubjectId { get; set; }

        public DateTimeOffset Expiration { get; set; }

        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }

    }
}