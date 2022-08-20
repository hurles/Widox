using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Widox.Enums;

namespace Widox.Blocks.Impl.Endpoints
{
    public class EndpointBlock : BlockBase
    {
        public new BlockType BlockType { get; set; } = BlockType.Endpoint;

        public Dictionary<HttpRequestMethod, EndpointMethodBlock> Methods { get; set; } = new();

    }
}
