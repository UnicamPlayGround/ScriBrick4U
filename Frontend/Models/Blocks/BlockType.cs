namespace Frontend.Models.Blocks
{
    /// <summary>
    /// Enum che descrive i tipi di blocco
    /// </summary>
    public enum BlockType
    {
        /// <summary> Blocco sotto al quale è possibile posizionare blocchi di altri tipi </summary>
        Principale,

        /// <summary> Blocco che permette di chiamare una funzione </summary>
        ChiamaFunzione,

        /// <summary> Blocco che permette di definire una funzione </summary>
        DefinizioneFunzione,

        /// <summary> Blocco che rappresenta una condizione </summary>
        Condizionale,

        /// <summary> Blocco che rappresenta un evento </summary>
        Evento,

        /// <summary> Blocco che rappresenta un movimento in avanti (o indietro) </summary>
        Movimento,

        /// <summary> Blocco che rappresenta un'operazione (ad esempio somma, sottrazione) </summary>
        Operazionale
    }

    /// <summary>
    /// Classe che rappresenta una serie di operazioni eseguibili sui tipi di blocco
    /// </summary>
    static class BlockTypeMethods
    {
        /// <summary>
        /// Dizionario contenente tutti i tipi di blocco con associato il rispettivo colore
        /// </summary>
        private static readonly Dictionary<BlockType, Color> _dictionary = new() {
            { BlockType.Principale, Color.FromRgb(255, 165, 0) },
            { BlockType.Condizionale, Color.FromRgb(255, 255, 0) },
            { BlockType.ChiamaFunzione, Color.FromRgb(235, 190, 15) },
            { BlockType.DefinizioneFunzione, Color.FromRgb(235, 190, 15) },
            { BlockType.Evento, Color.FromRgb(220, 180, 0) },
            { BlockType.Movimento, Color.FromRgb(0, 97, 62) },
            { BlockType.Operazionale, Color.FromRgb(0, 0, 255) },
        };

        /// <summary>
        /// Restituisce il colore associato al tipo di blocco passato come parametro
        /// </summary>
        /// <param name="type"> Tipo di blocco del quale estrarre il colore </param>
        /// <returns> il colore associato al tipo di blocco passato come parametro </returns>
        public static Color GetColor(BlockType type)
        {
            return _dictionary[type];
        }
    }
}