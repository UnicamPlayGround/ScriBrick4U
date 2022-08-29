using Backend.Blocks;
using Frontend.Model.Blocks;

namespace Frontend.Translators
{
    public interface ITranslator
    {
        List<IBlock> Translate(IEnumerable<IFrontEndBlock> frontEndBlocks);
    }
}