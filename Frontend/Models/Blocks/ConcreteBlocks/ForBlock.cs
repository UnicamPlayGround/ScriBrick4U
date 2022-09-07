﻿using Frontend.Helpers.Builders;
using Frontend.Models.Blocks.AbstractTypeBlocks;
using Frontend.Models.QuestionItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frontend.Models.Blocks.ConcreteBlocks
{
    public class ForBlock : WithChildrenFrontEndBlock
    {
        public override IFrontEndBlock GetInfo()
        {
            IBlockEditItem editItem = new EntryEditItem(
                "Digita il numero di ripetizioni: ",
                TypeValue.NUMBER,
                "Il numero di ripetizioni deve essere un numero."
            );
            return new BlockBuilder<ForBlock>("For", BlockType.Condizionale, BlockCategory.Controllo)
                .AddLabel("Ripeti N volte")
                .AddQuestion(editItem)
                .AddTextDroppedFunction(() => { return "Ripeti " + editItem.ToString() + " volte"; })
                .Build();
        }
    }
}