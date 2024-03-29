﻿using Frontend.Models.Blocks.Shapes;

namespace Frontend.Models.Blocks.AbstractTypeBlocks
{
    /// <summary>
    /// Classe astratta che rappresenta un blocco con la forma <see cref="ShapeType.WITH_CHILDREN"/>
    /// </summary>
    public abstract class WithChildrenFrontEndBlock : AbstractFrontEndBlock
    {
        /// <summary>
        /// Costruttore di default che imposta la forma, l'<see cref="IFrontEndBlock.Width"/> e la <see cref="IFrontEndBlock.Height"/> del blocco
        /// </summary>
        public WithChildrenFrontEndBlock()
        {
            Shape = ShapeTypeMethods.GetShape(ShapeType.WITH_CHILDREN);
            Width = 158;
            Height = 32;
        }
    }
}