using Frontend.Model.QuestionItem;
using Frontend.Models.Blocks.Bounds;
using Frontend.Models.Blocks.Descriptors;
using Frontend.Models.Blocks.Shapes;
using Microsoft.Maui.Controls.Shapes;
using SkiaSharp;

namespace Frontend.Model.Blocks
{
    /// <summary>
    /// Interfaccia di base per i blocchi lato front-end
    /// </summary>
    public interface IFrontEndBlock
    {
        /// <summary>
        /// Descrittore del blocco di tipo <see cref="IBlockDescriptor"/>
        /// </summary>
        public IBlockDescriptor Descriptor { get; set; }

        /// <summary>
        /// Forma del blocco
        /// </summary>
        public IBlockShape Shape { get; set; }

        /// <summary>
        /// Dimensioni e posizione del blocco
        /// </summary>
        public IBlockBound Position { get; set; }

        /// <summary>
        /// Lista di <see cref="IBlockEditItem"/>
        /// </summary>
        public List<IBlockEditItem> Questions { get; set; }

        /// <summary>
        /// Funzione che restituisce il testo del blocco quando questo è stato trascinato
        /// </summary>
        public Func<string> TextDropped { get; set; }

        /// <summary>
        /// Elementi contenuti nel blocco
        /// </summary>
        public List<IView> Elements { get; set; }

        /// <summary>
        /// Disegna il blocco nel canvas passato come parametro
        /// </summary>
        /// <param name="canvas"> canvas nel quale disegnare il blocco </param>
        public void Draw(ICanvas canvas);

        /// <summary>
        /// Restituisce, partendo da un serie di punti passati come parametro, una stringa che rappresenta la forma
        /// </summary>
        /// <param name="points"> punti dai quali generare la stringa che rappresenta la forma </param>
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

        /// <summary>
        /// Metodo che permette di ottenere una nuova istanza del blocco
        /// </summary>
        /// <returns>una nuova istanza del blocco</returns>
        public IFrontEndBlock GetNewInstance();
    }
}