using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Widox.Enums;

namespace Widox.Blocks.Impl.Endpoints
{
    public class EndpointMethodBlock : BlockBase
    {
        public HttpRequestMethod Method { get; set; }
        public List<HttpResponseBlock> Responses { get; set; } = new();
    }
}
