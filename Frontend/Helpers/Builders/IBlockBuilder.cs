using Frontend.Model.Blocks;

namespace Frontend.Builders
{
    /// <summary>
    /// Interfaccia che rappresenta un costruttore di blocchi
    /// </summary>
    /// <typeparam name="T"> Classe che estende <see cref="IFrontEndBlock"/> </typeparam>
    public interface IBlockBuilder<T>
    {
        /// <summary>
        /// Metodo che permette di aggiungere una casella di testo (<see cref="Editor"/>) al blocco
        /// </summary>
        /// <returns> l'oggetto corrente </returns>
        public IBlockBuilder<T> AddInput();

        /// <summary>
        /// Metodo che permette di aggiungere una <see cref="Label"/> al blocco, avente come testo la stringa passata come parametro
        /// </summary>
        /// <returns> l'oggetto corrente </returns>
        public IBlockBuilder<T> AddLabel(string s);

        /// <summary>
        /// Metodo che permette di costruire il blocco
        /// </summary>
        /// <returns> il blocco costruito </returns>
        public T Build();
    }
}