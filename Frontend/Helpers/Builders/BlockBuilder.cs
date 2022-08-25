using Frontend.Model.Blocks;
using Frontend.Models.Blocks.Bounds;
using Frontend.Models.Blocks.Descriptors;

namespace Frontend.Builders
{
    /// <summary>
    /// Classe che rappresenta un costruttore di blocchi
    /// </summary>
    /// <typeparam name="T"> Classe che estende <see cref="IFrontEndBlock"/> </typeparam>
    public class BlockBuilder<T> : IBlockBuilder<T> where T : IFrontEndBlock, new()
    {
        private readonly T Block;

        /// <summary>
        /// Costruttore che inizializza il nome e il tipo del blocco
        /// </summary>
        /// <param name="name"> nome del blocco </param>
        /// <param name="type"> tipo del blocco </param>
        public BlockBuilder(string name, BlockType type)
        {
            Block = new()
            {
                Descriptor = new BlockDescriptor(name, type),
                Elements = new(),
                Position = new BlockBound()
            };
        }

        public IBlockBuilder<T> AddInput()
        {
            Block.Elements.Add(new Editor());
            return this;
        }

        public IBlockBuilder<T> AddLabel(string s)
        {
            Label l = new()
            {
                Text = s,
                FontSize = 12,
                FontAttributes = FontAttributes.Bold,
                VerticalOptions = LayoutOptions.Center
            };

            Block.Elements.Add(l);
            return this;
        }

        public IBlockBuilder<T> AddTextDroppedFunction(Func<string> textDropped)
        {
            Block.TextDropped = textDropped;
            return this;
        }


        public T Build()
        {
            return Block;
        }
    }
}
