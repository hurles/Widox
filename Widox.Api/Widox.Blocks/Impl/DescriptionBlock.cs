using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Widox.Enums;

namespace Widox.Blocks.Impl
{
    public class DescriptionBlock : BlockBase
    {
        public new BlockType BlockType { get; set; } = BlockType.Paragraph;

    }
}
