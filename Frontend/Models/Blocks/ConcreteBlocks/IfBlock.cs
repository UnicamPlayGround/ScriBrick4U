using Frontend.Helpers.Builders;
using Frontend.Models.Blocks.AbstractTypeBlocks;
using Frontend.Models.QuestionItem;

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
                new EntryEditItem(
                    "Digita il primo numero: ",
                    TypeValue.NUMBER,
                    "Il primo numero non è valido."
                ),
                new PickerEditItem(
                    "Seleziona la condizione: ",
                    TypeValue.STRING,
                    "Devi selezionare una condizione",
                    pickerOptions),
                new EntryEditItem(
                    "Digita il secondo numero: ",
                    TypeValue.NUMBER,
                    "Il secondo numero non è valido."
                )
            };

            return new BlockBuilder<IfBlock>("If", BlockType.Condizionale, BlockCategory.Controllo)
                .AddLabel("If VALORE1 CONDIZIONE VALORE2")
                .AddQuestions(editItems)
                .AddTextDroppedFunction(() => { return "If " + string.Join(" ", editItems); })
                .Build();

        }
    }
}
