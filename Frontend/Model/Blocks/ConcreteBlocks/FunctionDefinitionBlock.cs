﻿using Frontend.Builders;
using Frontend.Model.Blocks;
using Frontend.Model.QuestionItem;
using Frontend.Models.Blocks.Shapes;

namespace Frontend.Models.Blocks.ConcreteBlocks
{
    /// <summary>
    /// Classe concreta che rappresenta un blocco per definire una funzione
    /// </summary>
    public class FunctionDefinitionBlock : AbstractFrontEndBlock
    {
        /// <summary>
        /// Costruttore di default che imposta la forma, l'<see cref="IFrontEndBlock.Width"/> e l'altezza del blocco
        /// </summary>
        public FunctionDefinitionBlock()
        {
            Shape = ShapeTypeMethods.GetShape(ShapeType.UPPER);
            Width = 130;
            Height = 48;
        }

        public override IFrontEndBlock GetInfo()
        {
            IBlockEditItem editItem = new EntryEditItem(
                "Digita il nome della funzione: ",
                TypeValue.STRING,
                "Devi scrivere il nome della funzione."
            );
            return new BlockBuilder<FunctionDefinitionBlock>("Definizione Funzione", BlockType.DefinizioneFunzione)
                .AddLabel("funzione NOME")
                .AddQuestion(editItem)
                .AddTextDroppedFunction(() => { return "funzione " + editItem.ToString().ToUpper(); })
                .Build();
        }
    }
}
