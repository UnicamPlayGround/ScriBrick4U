using Frontend.Builders;
using Frontend.Model.QuestionItem;
using Frontend.Models.Blocks.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frontend.Model.Blocks.ConcreteBlocks
{
    public class KeyEventBlock : AbstractFrontEndBlock
    {
        public KeyEventBlock()
        {
            Shape = ShapeTypeMethods.GetShape(ShapeType.WITH_CHILDREN);
            Width = 158;
            Height = 32;
        }
        public override IFrontEndBlock GetInfo()
        {
            List<string> pickerOptions = new() { "left", "right",};
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
