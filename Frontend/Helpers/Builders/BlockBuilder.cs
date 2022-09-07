using Frontend.Models.Blocks;
using Frontend.Models.Blocks.Bounds;
using Frontend.Models.Blocks.Descriptors;
using Frontend.Models.QuestionItem;

namespace Frontend.Helpers.Builders
{
    /// <summary>
    /// Classe che rappresenta un costruttore di blocchi di default
    /// </summary>
    /// <typeparam name="T"> Classe che estende <see cref="IFrontEndBlock"/> </typeparam>
    public class BlockBuilder<T> : IBlockBuilder<T> where T : IFrontEndBlock, new()
    {
        private readonly T Block;

        /// <summary>
        /// Costruttore con, in input, il nome e il tipo del blocco
        /// </summary>
        /// <param name="name"> Nome del blocco </param>
        /// <param name="type"> Tipo del blocco </param>
        public BlockBuilder(string name, BlockType type, BlockCategory category)
        {
            Block = new()
            {
                Descriptor = new BlockDescriptor(name, type, category),
                Elements = new(),
                Questions = new(),
                Children = new(),
                Father = null,
                Position = new BlockBound()
            };
        }

        public IBlockBuilder<T> AddInput()
        {
            Block.Elements.Add(new Editor());
            return this;
        }

        public IBlockBuilder<T> AddLabel(string s, double fontSize = 12)
        {
            Label l = new()
            {
                Text = s,
                FontSize = fontSize,
                FontAttributes = FontAttributes.Bold,
                VerticalOptions = LayoutOptions.Center
            };

            Block.Elements.Add(l);
            return this;
        }

        public IBlockBuilder<T> AddQuestion(IBlockEditItem question)
        {
            Block.Questions.Add(question);
            return this;
        }

        public IBlockBuilder<T> AddQuestions(List<IBlockEditItem> questions)
        {
            foreach (var question in questions)
                AddQuestion(question);
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
