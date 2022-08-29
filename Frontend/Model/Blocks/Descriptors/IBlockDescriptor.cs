using Frontend.Model.Blocks;

namespace Frontend.Models.Blocks.Descriptors
{
    /// <summary>
    /// Interfaccia che rappresenta un descrittore di blocchi
    /// </summary>
    public interface IBlockDescriptor
    {
        /// <summary>
        /// Nome del blocco
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Tipo del blocco
        /// </summary>
        public BlockType Type { get; set; }

        /// <summary>
        /// Colore di sfondo del blocco
        /// </summary>
        public Color BackgroundColor { get; set; }
    }
}