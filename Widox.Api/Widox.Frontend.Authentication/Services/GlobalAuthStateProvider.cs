using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Widox.Frontend.Authentication.Services
{
    public class GlobalAuthStateProvider : RevalidatingServerAuthenticationStateProvider
    {
        private readonly IAuthStateCache _cache;

        public GlobalAuthStateProvider(IAuthStateCache cache, ILoggerFactory loggerFactory) 
            : base(loggerFactory)
        {
            _cache = cache;
        }

        protected override TimeSpan RevalidationInterval => TimeSpan.FromMinutes(1);

        protected override async Task<bool> ValidateAuthenticationStateAsync(AuthenticationState authenticationState, CancellationToken cancellationToken)
        {
            var subjectId = authenticationState.User.Claims
                 .Where(c => c.Type.Equals("jti"))
                 .Select(c => c.Value)
                 .FirstOrDefault();

            if (subjectId != null && _cache.HasSubjectId(subjectId))
            {
                var data = _cache.GetAuthStateFromCache(subjectId);
                if (DateTimeOffset.Now >= data.Expiration)
                {
                    _cache.Remove(subjectId);
                    return true;
                }
            }

            return false;
        }
    }
}
