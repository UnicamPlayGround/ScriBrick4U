namespace Frontend.Models.Blocks.Bounds
{
    /// <summary>
    /// Interfaccia che rappresenta le dimensioni e la posizione di un blocco
    /// </summary>
    public interface IBlockBound
    {
        /// <summary> Altezza del blocco </summary>
        public float Width { get; set; }

        /// <summary> Larghezza del blocco </summary>
        public float Height { get; set; }

        /// <summary> Posizione del punto in alto a sinistra del blocco </summary>
        public PointF UpperLeft { get; set; }

        /// <summary> Posizione del punto in basso a destra del blocco </summary>
        public PointF BottomRight { get; }
    }
}