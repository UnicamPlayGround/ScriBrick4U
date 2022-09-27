using Frontend.Helpers.Builders;
using Frontend.Models.Blocks.AbstractTypeBlocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frontend.Models.Blocks.ConcreteBlocks
{
    /// <summary>
    /// Classe concreta che rappresenta un blocco per l'evento collisione in Unity
    /// </summary>
    public class CollisionBlock : UpperFrontEndBlock
    {
        public override IFrontEndBlock GetInfo()
        {
            return new BlockBuilder<CollisionBlock>("Collision", BlockType.Principale, BlockCategory.Principale)
                .AddLabel("Collision", 18)
                .AddTextDroppedFunction(() => { return "Collision"; })
                .Build();
        }
    }
}
