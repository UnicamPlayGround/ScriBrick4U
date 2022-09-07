using Frontend.Models.Blocks;

namespace Frontend.Models.Blocks.Descriptors
{
    /// <summary>
    /// Classe concreta che rappresenta un descrittore di blocchi di default
    /// </summary>
    public class BlockDescriptor : IBlockDescriptor
    {
        public string Name { get; set; }

        public BlockType Type { get; set; }

        public Color BackgroundColor { get; set; }
        public BlockCategory Category { get; set; }

        /// <summary>
        /// Costruttore che imposta il nome e il tipo del blocco
        /// </summary>
        /// <param name="name"> Nome del blocco </param>
        /// <param name="type"> Tipo del blocco </param>
        public BlockDescriptor(string name, BlockType type, BlockCategory category)
        {
            Name = name;
            Type = type;
            Category = category;
            BackgroundColor = BlockCategoryMethod.GetColor(category);
        }
    }
}