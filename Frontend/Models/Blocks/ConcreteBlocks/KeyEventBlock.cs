using Frontend.Helpers.Builders;
using Frontend.Models.Blocks.AbstractTypeBlocks;
using Frontend.Models.QuestionItem;

namespace Frontend.Models.Blocks.ConcreteBlocks
{
    /// <summary>
    /// Classe concreta che rappresenta un blocco Evento Tastiera
    /// </summary>
    public class KeyEventBlock : WithChildrenFrontEndBlock
    {
        public override IFrontEndBlock GetInfo()
        {
            List<string> pickerOptions = new() { "left", "right", };
            List<IBlockEditItem> editItems = new()
            {
                new PickerEditItem(
                    "Seleziona il tasto da premere: ",
                    TypeValue.STRING,
                    "Devi selezionare un tasto",
                    pickerOptions),
            };

            return new BlockBuilder<KeyEventBlock>("KeyboardEvent", BlockType.Evento)
                .AddLabel("Quando premi TASTO")
                .AddQuestions(editItems)
                .AddTextDroppedFunction(() => { return "Quando premi tasto " + string.Join(" ", editItems); })
                .Build();
        }
    }
}
