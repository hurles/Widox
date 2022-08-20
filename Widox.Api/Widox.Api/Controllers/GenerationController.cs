using Microsoft.AspNetCore.Mvc;
using Widox.Blocks.Impl;
using Widox.Generation.Core;

namespace Widox.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GenerationController
    {
        private readonly IBlockGenerator _blockGenerator;

        public GenerationController(IBlockGenerator blockGenerator)
        {
            _blockGenerator = blockGenerator;
        }

        [HttpGet(Name = "Generate")]
        public async Task<IEnumerable<BlockBase>> Generate()
        {
            var blocks = await _blockGenerator.GenerateBlocksAsync();
            return blocks;
        }
    }
}
