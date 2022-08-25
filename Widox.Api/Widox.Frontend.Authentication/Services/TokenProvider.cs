using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Widox.Frontend.Authentication.Services
{
    public class TokenProvider : ITokenProvider
    {
        public string AccessToken { get; set; }
    }
}
