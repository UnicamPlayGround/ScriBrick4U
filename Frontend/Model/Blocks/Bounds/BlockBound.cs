namespace Frontend.Models.Blocks.Bounds
{
    public class BlockBound : IBlockBound
    {
        public float Width { get; set; }

        public float Height { get; set; }

        public PointF UpperLeft { get; set; }

        public PointF BottomRight => new(UpperLeft.X + Width, UpperLeft.Y + Height);

        /// <summary>
        /// Costruttore
        /// </summary>
        /// <param name="width"> altezza del blocco </param>
        /// <param name="height"> larghezza del blocco </param>
        /// <param name="upperLeft"> posizione del punto in alto a sinistra del blocco </param>
        public BlockBound(float width = 0, float height = 0, PointF upperLeft = new())
        {
            Width = width;
            Height = height;
            UpperLeft = upperLeft;
        }
    }
}