using Frontend.Blocks;

namespace Frontend.Builders
{
    /// <summary>
    /// Classe che rappresenta un costruttore di blocchi
    /// </summary>
    /// <typeparam name="T"> Parametro che estende <see cref="IFrontEndBlock"/> </typeparam>
    public class BlockBuilder<T> where T : IFrontEndBlock, new()
    {
        private readonly T Block;

        /// <summary>
        /// Costruttore che inizializza il nome e il tipo del blocco
        /// </summary>
        /// <param name="name"> nome del blocco </param>
        /// <param name="type"> tipo del blocco </param>
        public BlockBuilder(string name, string type)
        {
            Block = new()
            {
                Name = name,
                Type = type
            };
        }

        /// <summary>
        /// Metodo che permette di aggiungere una casella di testo (<see cref="Editor"/>) al blocco
        /// </summary>
        /// <returns> l'oggetto corrente </returns>
        public BlockBuilder<T> AddInput()
        {
            Block.Elements.Add(new Editor());
            return this;
        }

        /// <summary>
        /// Metodo che permette di aggiungere una <see cref="Label"/> al blocco, avente come testo la stringa passata come parametro
        /// </summary>
        /// <returns> l'oggetto corrente </returns>
        public BlockBuilder<T> AddLabel(string s)
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

        /// <summary>
        /// Metodo che permette di costruire il blocco
        /// </summary>
        /// <returns> il blocco costruito </returns>
        public T Build()
        {
            return Block;
        }
    }
}
