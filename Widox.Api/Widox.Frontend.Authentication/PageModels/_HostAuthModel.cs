using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Widox.Frontend.Authentication.Services;
using Widox.Models.Frontend;

namespace Widox.Frontend.Authentication.PageModels
{
    public class _HostAuthModel : PageModel
    {
        private readonly IAuthStateCache _cache;

        public _HostAuthModel(IAuthStateCache cache)
        {
            _cache = cache;
        }

        public async Task<IActionResult> OnGet()
        {
            if (User.Identity.IsAuthenticated)
            {
                var sessionID = GetSessionId();

                if (sessionID != null && !_cache.HasSubjectId(sessionID))
                {
                    var authResult = await HttpContext.AuthenticateAsync("JWT");
                    DateTimeOffset exp = authResult.Properties.ExpiresUtc.Value;
                    string accessToken = await HttpContext.GetTokenAsync("access_token");
                    string refreshToken = await HttpContext.GetTokenAsync("refresh_token");

                    _cache.Add(sessionID, exp, accessToken, refreshToken);

                }
            }
            return Page();
        }

        public string? GetSessionId()
        {
            return User.Claims.Where(c => c.Type == "jti")
                .Select(c => c.Value)
                .FirstOrDefault();
        }

        public AuthStateCacheInstance? GetAuthState()
        {
            if (User.Identity.IsAuthenticated)
            {
                var sessionId = GetSessionId();
                if (sessionId != null)
                    return _cache.GetAuthStateFromCache(sessionId);
            }

            return null;
        }

        public IActionResult OnGetLogin()
        {
            var authProps = new AuthenticationProperties()
            {
                IsPersistent = true,
                ExpiresUtc = DateTimeOffset.Now.AddSeconds(45),
                RedirectUri = Url.Content("~/")
            };
            return Challenge(authProps, "Jwt");
        }

        public async Task OnGetLogout()
        {
            var authProps = new AuthenticationProperties() { RedirectUri = Url.Content("~/") };
            await HttpContext.SignOutAsync("Cookies");
            await HttpContext.SignOutAsync("Jwt", authProps);

        }
    }
}
