using Frontend.Helpers.Builders;
using Frontend.Models.Blocks.AbstractTypeBlocks;
using Frontend.Models.QuestionItem;
using Frontend.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frontend.Models.Blocks.ConcreteBlocks
{
    public class WhileBlock : WithChildrenFrontEndBlock
    {
        public override IFrontEndBlock GetInfo()
        {
            List<string> pickerOptions = new() { "<", ">", "==", "<=", ">=" };
            List<IBlockEditItem> editItems = new()
            {
                new PickerEditItem(
                  "Seleziona una variabile",
                  BlockViewModel.VariableNames
                ),
                new EntryEditItem(
                    "O digita il primo valore: "
                ),
                new PickerEditItem(
                    "Seleziona la condizione: ",
                    TypeValue.STRING,
                    "Devi selezionare una condizione",
                    pickerOptions),
                new PickerEditItem(
                  "Seleziona una variabile",
                  BlockViewModel.VariableNames
                ),
                new EntryEditItem(
                    "O digita il secondo valore: "
                ),
            };

            return new BlockBuilder<WhileBlock>("While", BlockType.Condizionale, BlockCategory.Controllo)
                .AddLabel("While CONDIZIONE")
                .AddQuestions(editItems)
                .AddTextDroppedFunction(() => { return $"While {editItems[0].Value ?? editItems[1].Value} {editItems[2].Value} {editItems[3].Value ?? editItems[4].Value} "; })
                .Build();
        }
    }
}
