using Frontend.Model.Blocks.Shapes;

namespace Frontend.Model.Blocks.AbstractTypeBlocks
{
    /// <summary>
    /// Classe astratta che rappresenta un blocco con la forma <see cref="ShapeType.WITH_CHILDREN"/>
    /// </summary>
    public abstract class WithChildrenForntEndBlock : AbstractFrontEndBlock
    {
        /// <summary>
        /// Costruttore di default che imposta la forma, l'<see cref="IFrontEndBlock.Width"/> e la <see cref="IFrontEndBlock.Height"/> del blocco
        /// </summary>
        public WithChildrenForntEndBlock()
        {
            Shape = ShapeTypeMethods.GetShape(ShapeType.WITH_CHILDREN);
            Width = 158;
            Height = 32;
        }
    }
}