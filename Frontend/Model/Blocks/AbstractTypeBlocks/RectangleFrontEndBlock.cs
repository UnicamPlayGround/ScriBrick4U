using Frontend.Model.Blocks.Shapes;

namespace Frontend.Model.Blocks.AbstractTypeBlocks
{
    /// <summary>
    /// Classe astratta che rappresenta un blocco con la forma <see cref="ShapeType.RECTANGLE"/>
    /// </summary>
    public abstract class RectangleFrontEndBlock : AbstractFrontEndBlock
    {
        /// <summary>
        /// Costruttore di default che imposta la forma, l'<see cref="IFrontEndBlock.Width"/> e la <see cref="IFrontEndBlock.Height"/> del blocco
        /// </summary>
        public RectangleFrontEndBlock()
        {
            Shape = ShapeTypeMethods.GetShape(ShapeType.RECTANGLE);
            Width = 130;
            Height = 48;
        }
    }
}