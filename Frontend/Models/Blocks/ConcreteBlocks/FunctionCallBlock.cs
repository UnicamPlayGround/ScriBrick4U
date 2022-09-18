﻿using Frontend.Helpers.Builders;
using Frontend.Models.Blocks.Shapes;
using Frontend.Models.QuestionItem;
using Frontend.ViewModels;

namespace Frontend.Models.Blocks.ConcreteBlocks
{
    /// <summary>
    /// Classe concreta che rappresenta un blocco per chiamare una funzione
    /// </summary>
    public class FunctionCallBlock : AbstractFrontEndBlock
    {
        /// <summary>
        /// Costruttore di default che imposta la forma, l'<see cref="IFrontEndBlock.Width"/> e l'altezza del blocco
        /// </summary>
        public FunctionCallBlock()
        {
            Shape = ShapeTypeMethods.GetShape(ShapeType.RECTANGLE);
            Width = 130;
            Height = 48;
        }

        public override IFrontEndBlock GetInfo()
        {
            List<IBlockEditItem> editItems = new()
            {
                 new PickerEditItem(
                    "Seleziona il nome della funzione da richiamare: ",
                    TypeValue.STRING,
                    "Devi selezionare il nome della funzione da richiamare.",
                    BlockViewModel.FunctionNames
                ),
                new PickerEditItem(
                    "Seleziona la variabile dove salvare il valore di ritorno",
                    BlockViewModel.VariableNames
                )
            };

            return new BlockBuilder<FunctionCallBlock>("Chiama Funzione", BlockType.ChiamaFunzione, BlockCategory.Funzione)
                .AddLabel("chiama funzione NOMEFUNZIONE")
                .AddQuestions(editItems)
                .AddTextDroppedFunction(() => 
                {
                    string text = "";
                    if (!string.IsNullOrEmpty(editItems[1].Value))
                    {
                        text += $"{editItems[1].Value} = ";
                    }
                    text += $"{editItems[0].Value}()";
                    return text;
                })
                .Build();
        }
    }
}
