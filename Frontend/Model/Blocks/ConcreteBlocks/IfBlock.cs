using Frontend.Helpers.Builders;
using Frontend.Model.Blocks.AbstractTypeBlocks;
using Frontend.Model.QuestionItem;

namespace Frontend.Model.Blocks.ConcreteBlocks
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

            return new BlockBuilder<IfBlock>("If", BlockType.Condizionale)
                .AddLabel("If VALORE1 CONDIZIONE VALORE2")
                .AddQuestions(editItems)
                .AddTextDroppedFunction(() => { return "If " + string.Join(" ", editItems); })
                .Build();

        }
    }
}
