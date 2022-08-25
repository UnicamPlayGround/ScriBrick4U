using Frontend.Models.Blocks.Bounds;
using Frontend.Models.Blocks.Descriptors;
using Frontend.Models.Blocks.Shapes;
using ProvaMauiDragAndDrop.Helper;
using SkiaSharp;
using System.Numerics;
using Font = Microsoft.Maui.Graphics.Font;

namespace Frontend.Model.Blocks
{
    /// <summary>
    /// Classe che rappresenta un blocco con <c>Label</c> e <c>caselle di testo</c>
    /// </summary>
    public class LabelEditor : IFrontEndBlock
    {
        /// <summary> Rappresenta la larghezza del blocco </summary>
        public float HorizontalOffset { get; set; } = 130;
        /// <summary> Rappresenta l'altezza del blocco </summary>
        public float Height { get; set; } = 35;

        public IBlockDescriptor Descriptor { get; set; }
        public IBlockShape Shape { get; set; }
        public IBlockBound Position { get; set; }

        public Func<string> TextDropped { get; set; }

        /// <summary>
        /// Lista privata che conterrà effettivamente gli elementi del blocco
        /// </summary>
        private List<IView> _elements { get; set; }
        public List<IView> Elements {
            get => _elements;
            set => _elements = value; 
        }


        /// <summary>
        /// Costruttore di default della classe
        /// </summary>
        public LabelEditor()
        {
            Shape = ShapeTypeMethods.GetShape(ShapeType.RECTANGLE);
            HorizontalOffset = 130;
            Height = 48;
        }

        public void Draw(ICanvas canvas)
        {
            canvas.FillColor = Descriptor.BackgroundColor;
            canvas.Font = Font.DefaultBold;
            canvas.FontSize = (float)((Label)Elements.ElementAt(0)).FontSize;
            canvas.FontColor = Color.FromRgb(255, 255, 255);

            string text = TextDropped.Invoke();

            var defaultOffset = HorizontalOffset;
            HorizontalOffset += text.Length;
            Shape.Path = Shape.GetSvgPath(HorizontalOffset, Height);

            var pathf = ((IFrontEndBlock)this).PointsToPath(SKPath.ParseSvgPathData(Shape.Path).Points);
            pathf.Transform(Matrix3x2.CreateTranslation(Position.UpperLeft.X, Position.UpperLeft.Y));
            canvas.FillPath(pathf);
            canvas.DrawPath(pathf);

            Position.Width = pathf.Bounds.Width;
            Position.Height = pathf.Bounds.Height;

            //TODO da aggiustare
            //       canvas.DrawText(MarkdownAttributedTextReader.Read(text), pathf.Bounds.Left, pathf.Bounds.Top + 15, pathf.Bounds.Width, pathf.Bounds.Height);
            canvas.DrawString(text, pathf.Bounds.Left, pathf.Bounds.Top + 15, pathf.Bounds.Width, pathf.Bounds.Height, HorizontalAlignment.Center, VerticalAlignment.Top);
            HorizontalOffset = defaultOffset;
        }

        public IFrontEndBlock GetNewInstance()
        {
            return BlockGenerator.GetBlock(Descriptor.Name);
        }
    }
}