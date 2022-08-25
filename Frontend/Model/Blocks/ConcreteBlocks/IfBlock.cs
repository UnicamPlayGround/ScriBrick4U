using Frontend.Builders;
using Frontend.Model.Blocks;
using Frontend.Model.QuestionItem;
using Frontend.Models.Blocks.Shapes;

namespace Frontend.Models.Blocks.ConcreteBlocks
{
    public class IfBlock : AbstractFrontEndBlock
    {
        public IfBlock()
        {
            Shape = ShapeTypeMethods.GetShape(ShapeType.WITH_CHILDREN);
            HorizontalOffset = 158;
            Height = 32;
        }

        public override IFrontEndBlock GetInfo()
        {
            List<string> pickerOptions = new() { "<", ">", "=", "<=", ">=" };
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
