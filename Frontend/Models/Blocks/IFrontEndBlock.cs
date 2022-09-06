using Frontend.Models.Blocks.Bounds;
using Frontend.Models.Blocks.Descriptors;
using Frontend.Models.Blocks.Shapes;
using Frontend.Models.QuestionItem;
using SkiaSharp;

namespace Frontend.Models.Blocks
{
    /// <summary>
    /// Interfaccia di base per i blocchi lato front-end
    /// </summary>
    public interface IFrontEndBlock
    {
        /// <summary>
        /// Larghezza iniziale del blocco
        /// </summary>
        public float Width { get; set; }

        /// <summary>
        /// Altezza iniziale del blocco
        /// </summary>
        public float Height { get; set; }

        /// <summary>
        /// Descrittore del blocco
        /// </summary>
        public IBlockDescriptor Descriptor { get; set; }

        /// <summary>
        /// Forma del blocco
        /// </summary>
        public IBlockShape Shape { get; set; }

        /// <summary>
        /// Dimensioni e coordinate del blocco
        /// </summary>
        public IBlockBound Position { get; set; }

        /// <summary>
        /// Lista di <see cref="IBlockEditItem"/>
        /// </summary>
        public List<IBlockEditItem> Questions { get; set; }

        /// <summary>
        /// Funzione che restituisce il testo del blocco quando questo è stato posizionato
        /// </summary>
        public Func<string> TextDropped { get; set; }

        /// <summary>
        /// Padre del blocco
        /// </summary>
        public IFrontEndBlock? Father { get; set; }

        /// <summary>
        /// Blocco successivo
        /// </summary>
        public IFrontEndBlock? Next { get; set; }

        /// <summary>
        /// Figli del blocco
        /// </summary>
        public List<IFrontEndBlock> Children { get; set; }

        /// <summary>
        /// Elementi contenuti nel blocco (utilizzati prima che questo venga posizionato)
        /// </summary>
        public List<Element> Elements { get; set; }
        public bool CanContainChildren { get; }
        public bool IsStart { get; }

        /// <summary>
        /// Disegna il blocco nel canvas passato come parametro
        /// </summary>
        /// <param name="canvas"> Canvas nel quale disegnare il blocco </param>
        public void Draw(ICanvas canvas);

        /// <summary>
        /// Restituisce una nuova istanza del blocco corrente
        /// </summary>
        /// <returns> una nuova istanza del blocco corrente </returns>
        public IFrontEndBlock GetInfo();

        /// <summary>
        /// Restituisce, partendo da un serie di punti passati come parametro, un'oggetto che rappresenta la forma
        /// </summary>
        /// <param name="points"> Punti dai quali generare la stringa che rappresenta la forma </param>
        /// <returns> un'oggetto che rappresenta la forma </returns>
        public virtual PathF PointsToPath(SKPoint[] points)
        {
            PointF point;
            PathF path = new();

            for (var i = 1; i < points.Length; i++)
            {
                point = new PointF(points[i].X, points[i].Y);

                if (i == points.Length - 1)
                {
                    path.LineTo(point);
                    path.Close();
                }
                else
                    path.LineTo(point);
            }

            return path;
        }
    }
}