using Backend.Blocks;
using Frontend.Models.Blocks;

namespace Frontend.Translators
{
    /// <summary>
    /// Interfaccia che definisce il traduttore per i blocchi frontend in quelli backend
    /// </summary>
    public interface ITranslator
    {
        /// <summary>
        /// Metodo per la traduzione dei blocchi frontend in quelli backend
        /// </summary>
        /// <param name="frontEndBlocks">Lista dei blocchi posizionati dall'utente</param>
        /// <returns><c>IEnumerable</c> contenente i blocchi tradotti</returns>
        IEnumerable<IBlock> Translate(IEnumerable<IFrontEndBlock> frontEndBlocks);
    }
}