using Widox.Blocks;
using Widox.Blocks.Impl;

namespace Widox.Generation.Core
{
    public interface IBlockGenerator
    {
        public Task<IEnumerable<BlockBase>> GenerateBlocksAsync();
    }
}