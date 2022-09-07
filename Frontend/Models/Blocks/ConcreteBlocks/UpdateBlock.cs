using Frontend.Helpers.Builders;
using Frontend.Models.Blocks.AbstractTypeBlocks;

namespace Frontend.Models.Blocks.ConcreteBlocks
{
    /// <summary>
    /// Classe concreta che rappresenta un blocco Update
    /// </summary>
    public class UpdateBlock : UpperFrontEndBlock
    {

        public override IFrontEndBlock GetInfo()
        {
            return new BlockBuilder<UpdateBlock>("Update", BlockType.Principale, BlockCategory.Principale)
                .AddLabel("Update", 18)
                .AddTextDroppedFunction(() => { return "Update"; })
                .Build();
        }
    }
}
