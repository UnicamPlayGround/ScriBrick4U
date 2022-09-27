using Frontend.Helpers.Builders;
using Frontend.Models.Blocks.AbstractTypeBlocks;
using Frontend.Models.QuestionItem;

namespace Frontend.Models.Blocks.ConcreteBlocks
{
    /// <summary>
    /// 
    /// </summary>
    public class CollisionWithBlock : WithChildrenFrontEndBlock
    {
        public override IFrontEndBlock GetInfo()
        {
            IBlockEditItem editItems = new EntryEditItem("Digita il nome dell'oggetto con cui hai colliso: ", TypeValue.STRING, "Valore non valido");

            return new BlockBuilder<CollisionWithBlock>("CollisionWith", BlockType.Condizionale, BlockCategory.Controllo)
                .AddLabel("HAI COLLISO CON ")
                .AddQuestion(editItems)
                .AddTextDroppedFunction(() => { return $"Hai colliso con {editItems.Value}"; })
                .Build();

        }
    }
}
