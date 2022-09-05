namespace Frontend.Model.Blocks.Bounds
{
    /// <summary>
    /// Classe di default che rappresenta le dimensioni e le coordinate di un blocco
    /// </summary>
    public class BlockBound : IBlockBound
    {
        public float Width { get; set; }

        public float Height { get; set; }

        public PointF UpperLeft { get; set; }

        public PointF BottomRight => new(UpperLeft.X + Width, UpperLeft.Y + Height);

        /// <summary>
        /// Costruttore che imposta l'altezza, la larghezza e le coordinate del punto in alto a sinistra
        /// </summary>
        /// <param name="width"> Altezza del blocco </param>
        /// <param name="height"> Larghezza del blocco </param>
        /// <param name="upperLeft"> Coordinate del punto in alto a sinistra del blocco </param>
        public BlockBound(float width = 0, float height = 0, PointF upperLeft = new())
        {
            Width = width;
            Height = height;
            UpperLeft = upperLeft;
        }
    }
}