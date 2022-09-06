using Microsoft.Maui.Controls.Shapes;

namespace Frontend.Models.Blocks.Shapes
{
    /// <summary>
    /// Classe che rappresenta la forma di un blocco di default
    /// </summary>
    public class DefaultShape : IBlockShape
    {
        public string Path { get; set; } = "";
        public string PathTemplate { get; } = "";
        public Geometry? SvgData { get; set; }
        public PointF BlockOffset { get; set; }
        public ShapeType Type { get; set; }
        public Thickness Margin { get; set; }

        /// <summary>
        /// Costruttore che inizializza la classe
        /// </summary>
        /// <param name="path"> Template della stringa che rappresenta la forma </param>
        /// <param name="type"> Tipo della forma </param>
        /// <param name="blockOffset">  </param>
        /// <param name="width"> Larghezza del blocco </param>
        /// <param name="height"> Altezza del blocco </param>
        public DefaultShape(string path, ShapeType type, PointF blockOffset, float width, float height)
        {
            PathTemplate = path;
            Path = GetSvgPath(width, height);
            SvgData = new PathGeometryConverter().ConvertFromInvariantString(Path) as Geometry;
            BlockOffset = blockOffset;
            Margin = !type.Equals(ShapeType.UPPER) ? new(8, 14, 8, 0) : new(8, 30, 8, 0);
            Type = type;
        }
        public string GetSvgPath(float width, float height)
        {
            string template = PathTemplate;
            template = template.Replace("%WIDTH%", width.ToString());
            template = template.Replace("%HEIGHT%", height.ToString());
            return template;
        }
    }
}