using Frontend.Helpers.Builders;
using Frontend.Models.Blocks.AbstractTypeBlocks;
using Frontend.Models.QuestionItem;
using Frontend.ViewModels;

namespace Frontend.Models.Blocks.ConcreteBlocks
{
    /// <summary>
    /// Classe concreta che rappresenta un blocco If
    /// </summary>
    public class IfBlock : WithChildrenFrontEndBlock
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

            return new BlockBuilder<IfBlock>("If", BlockType.Condizionale, BlockCategory.Controllo)
                .AddLabel("If VALORE1 CONDIZIONE VALORE2")
                .AddQuestions(editItems)
                .AddTextDroppedFunction(() => { return $"If {editItems[0].Value ?? editItems[1].Value} {editItems[2].Value} {editItems[3].Value ?? editItems[4].Value} "; } )
                .Build();

        }
    }
}
