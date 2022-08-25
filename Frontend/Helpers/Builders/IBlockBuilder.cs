using Frontend.Model.Blocks;
using Frontend.Model.QuestionItem;

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
        /// <param name="text"> Testo della Label </param>
        /// <param name="fontSize"> Dimensione della Label </param>
        /// <returns> l'oggetto corrente </returns>
        public IBlockBuilder<T> AddLabel(string text, double fontSize = 12);

        /// <summary>
        /// Aggiunge il <see cref="IBlockEditItem"/>, passato come parametro, al blocco
        /// </summary>
        /// <param name="question"> <see cref="IBlockEditItem"/> da aggiungere </param>
        /// <returns> l'oggetto corrente </returns>
        public IBlockBuilder<T> AddQuestion(IBlockEditItem question);

        /// <summary>
        /// Aggiunge la lista di <see cref="IBlockEditItem"/>, passata come parametro, al blocco
        /// </summary>
        /// <param name="questions"> lista di <see cref="IBlockEditItem"/> da aggiungere </param>
        /// <returns> l'oggetto corrente </returns>
        public IBlockBuilder<T> AddQuestions(List<IBlockEditItem> questions);

        /// <summary>
        /// Aggiunge la funzione <see cref="IFrontEndBlock.TextDropped"/> al blocco
        /// </summary>
        /// <param name="textDropped"></param>
        /// <returns> l'oggetto corrente </returns>
        public IBlockBuilder<T> AddTextDroppedFunction(Func<string> textDropped);

        /// <summary>
        /// Metodo che permette di costruire il blocco
        /// </summary>
        /// <returns> il blocco costruito </returns>
        public T Build();
    }
}