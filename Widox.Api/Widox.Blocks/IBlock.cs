using Widox.Enums;

namespace Widox.Blocks
{
    public interface IBlock
    {
        public string? Identifier { get; set; }
                     
        public string? Title { get; set; }
                     
        public string? Description { get; set; }
                     
        public string? Body { get; set; }
                     
        public string? Example { get; set; }

        public BlockType BlockType { get; set; }

        public Dictionary<BlockType, List<IBlock>> Children { get; set; }
    }
}