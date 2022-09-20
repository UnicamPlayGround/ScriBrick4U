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
        Operazionale,
        /// <summary>
        /// Blocco che rappresenta la definizione di una variabile
        /// </summary>
        DefinizioneVariabile,
        /// <summary>
        /// Blocco che rappresenta la modifica di una variabile
        /// </summary>
        ModificaVariabile,
        /// <summary>
        /// Blocco che rappresenta il ritorno di un valore
        /// </summary>
        RitornaValore
    }
}