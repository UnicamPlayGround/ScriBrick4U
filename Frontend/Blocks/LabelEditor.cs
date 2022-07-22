using Microsoft.Maui.Controls.Shapes;

namespace Frontend.Blocks
{
    /// <summary>
    /// Classe che rappresenta un blocco con <c>Label</c> e <c>caselle di testo</c>
    /// </summary>
    public class LabelEditor : IFrontEndBlock
    {
        /// <summary>
        /// Stringa che rappresenta la forma del blocco
        /// </summary>
        private static readonly string _svgData = "m 0,4 A 4,4 0 0,1 4,0 H 12 c 2,0 3,1 4,2 l 4,4 c 1,1 2,2 4,2 h 12 c 2,0 3,-1 4,-2 l 4,-4 c 1,-1 2,-2 4,-2 H 180 a 4,4 0 0,1 4,4 v 40 a 4,4 0 0,1 -4,4 H 48 c -2,0 -3,1 -4,2 l -4,4 c -1,1 -2,2 -4,2 h -12 c -2,0 -3,-1 -4,-2 l -4,-4 c -1,-1 -2,-2 -4,-2 H 4 a 4,4 0 0,1 -4,-4 z";
        
        public Geometry SvgData { get; set; }

        /// <summary>
        /// Lista che conterrà effettivamente gli elementi del blocco
        /// </summary>
        private List<IView> _elements { get; set; }
        public List<IView> Elements {
            get => _elements;
            set => _elements = value; 
        }
        
        public string Name { get; set; }
        
        public string Type { get; set; }


        /// <summary>
        /// Costruttore di default della classe
        /// </summary>
        public LabelEditor()
        {
            this.Elements = new();
            SvgData = (Geometry)new PathGeometryConverter().ConvertFromInvariantString(_svgData);
        }


        public IFrontEndBlock GetNewInstance()
        {
            throw new NotImplementedException("Metodo non implementato.");
        }
    }
}