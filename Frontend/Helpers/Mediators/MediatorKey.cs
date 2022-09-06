using Frontend.Models.Blocks;
using Frontend.ViewModels;

namespace Frontend.Helpers.Mediators
{
    /// <summary>
    /// Enum che descrive le azioni eseguibili da un <see cref="IMediator"/>
    /// </summary>
    public enum MediatorKey
    {
        /// <summary> Aggiornare la lista dei blocchi selezionabili, contenuta nel <see cref="BlockViewModel"/>, in base ad un <see cref="BlockType"/> </summary>
        UPDATEBLOCKSBYTYPE,

        /// <summary> Restituire la lista dei blocchi trascinati, contenuta nel <see cref="BlockViewModel"/> </summary>
        GETDROPPEDBLOCKS,

        /// <summary> Restituire la lista dei blocchi trascinati, contenuta nel <see cref="BlockViewModel"/>, in formato Json </summary>
        GETJSONDROPPEDBLOCKS,

        /// <summary> Impostare la lista dei blocchi trascinati, contenuta nel <see cref="BlockViewModel"/>, partendo da una stringa in formato Json </summary>
        SETDROPPEDBLOCKSFROMJSON
    }
}