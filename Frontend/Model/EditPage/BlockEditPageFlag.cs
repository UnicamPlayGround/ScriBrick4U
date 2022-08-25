namespace Frontend.Models.EditPage
{
    /// <summary>
    /// Enum che rappresenta le azioni che si possono compiere in un'<see cref="EditPage"/>
    /// </summary>
    public enum BlockEditPageFlag
    {
        /// <summary> Conferma l'edit del blocco </summary>
        CONFERMA,

        /// <summary> Annulla l'edit del blocco </summary>
        ANNULLA,

        /// <summary> Elimina il blocco </summary>
        ELIMINA
    }
}