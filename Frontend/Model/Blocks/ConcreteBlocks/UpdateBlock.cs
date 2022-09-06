using Frontend.Builders;
using Frontend.Model.Blocks.AbstractTypeBlocks;

namespace Frontend.Model.Blocks.ConcreteBlocks
{
    /// <summary>
    /// Classe concreta che rappresenta un blocco Update
    /// </summary>
    public class UpdateBlock : UpperFrontEndBlock
    {

        public override IFrontEndBlock GetInfo()
        {
            return new BlockBuilder<UpdateBlock>("Update", BlockType.Principale)
                .AddLabel("Update", 18)
                .AddTextDroppedFunction(() => { return "Update"; })
                .Build();
        }
    }
}
