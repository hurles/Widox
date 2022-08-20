using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Widox.Enums;

namespace Widox.Blocks.Impl.Endpoints
{
    public class HttpResponseBlock : BlockBase
    {
        public string? ResponseType { get; set; }

        public HttpRequestMethod Method { get; set; }

        public new BlockType BlockType { get; set; } = BlockType.HttpResponse;

    }
}
