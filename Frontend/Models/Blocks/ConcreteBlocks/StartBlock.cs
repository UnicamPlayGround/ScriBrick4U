﻿using Frontend.Helpers.Builders;
using Frontend.Models.Blocks.AbstractTypeBlocks;

namespace Frontend.Models.Blocks.ConcreteBlocks
{
    /// <summary>
    /// Classe concreta che rappresenta un blocco Start
    /// </summary>
    public class StartBlock : UpperFrontEndBlock
    {
        public override IFrontEndBlock GetInfo()
        {
            return new BlockBuilder<StartBlock>("Start", BlockType.Principale)
                .AddLabel("Start", 18)
                .AddTextDroppedFunction(() => { return "Start"; })
                .Build();
        }
    }
}
