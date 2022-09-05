using Frontend.EditPage;

namespace Frontend.Model.EditPage
{
    /// <summary>
    /// Enum che descrive le azioni che si possono compiere in un'<see cref="BlockEditPage"/>
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