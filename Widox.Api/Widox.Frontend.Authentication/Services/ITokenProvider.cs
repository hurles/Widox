namespace Widox.Frontend.Authentication.Services
{
    public interface ITokenProvider
    {
        string AccessToken { get; set; }
    }
}