using Microsoft.Maui.Controls.Shapes;

namespace Frontend.Blocks
{
    /// <summary>
    /// Interfaccia di base per i blocchi lato front-end
    /// </summary>
    public interface IFrontEndBlock
    {
        /// <summary>
        /// Forma del blocco
        /// </summary>
        public Geometry SvgData { get; set; }
        
        /// <summary>
        /// Elementi contenuti nel blocco
        /// </summary>
        public List<IView> Elements { get; set; }

        /// <summary>
        /// Nome del blocco
        /// </summary>
        public string name { get; set; }
        
        /// <summary>
        /// Tipo del blocco
        /// </summary>
        public string type { get; set; }

        /// <summary>
        /// Metodo che permette di ottenere una nuova istanza del blocco
        /// </summary>
        /// <returns>una nuova istanza del blocco</returns>
        public IFrontEndBlock GetNewInstance();

    }
}
