using Frontend.Model.Blocks;
using Frontend.Model.QuestionItem;
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
