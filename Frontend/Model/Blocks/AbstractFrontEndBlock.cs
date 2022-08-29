using Frontend.Model.QuestionItem;
using Frontend.Models.Blocks.Bounds;
using Frontend.Models.Blocks.Descriptors;
using SkiaSharp;
using System.Numerics;
using System.Reflection;
using Color = Microsoft.Maui.Graphics.Color;
using Font = Microsoft.Maui.Graphics.Font;
using IBlockShape = Frontend.Models.Blocks.Shapes.IBlockShape;

namespace Frontend.Model.Blocks
{
    /// <summary>
    /// Classe astratta per tutti i blocchi lato front-end
    /// </summary>
    public abstract class AbstractFrontEndBlock : IFrontEndBlock
    {
        public float HorizontalOffset { get; set; } = 130;
        public float Height { get; set; } = 35;

        public IBlockDescriptor Descriptor { get; set; }
        public IBlockShape Shape { get; set; }
        public IBlockBound Position { get; set; }

        public List<IBlockEditItem> Questions { get; set; }
        public Func<string> TextDropped { get; set; }

        public IFrontEndBlock Father { get; set; }
        public List<IFrontEndBlock> Children { get; set; }

        /// <summary> Lista privata che rappresenta gli elementi contenuti nel blocco </summary>
        private List<Element> _elements;
        public List<Element> Elements
        {
            get => _elements;
            set => _elements = value;
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

            canvas.DrawString(text, pathf.Bounds.Left, pathf.Bounds.Top + 15, pathf.Bounds.Width, pathf.Bounds.Height, HorizontalAlignment.Center, VerticalAlignment.Top);
            HorizontalOffset = defaultOffset;
        }

        public abstract IFrontEndBlock GetInfo();

        /// <summary>
        /// Restituisce una lista contenente un'istanza per ogni classe che eredita da quella corrente
        /// </summary>
        /// <returns> una lista contenente un'istanza per ogni classe che eredita da quella corrente </returns>
        public static IEnumerable<AbstractFrontEndBlock> GetEnumerableOfType()
        {
            List<AbstractFrontEndBlock> objects = new();
            foreach (Type type in
                Assembly.GetAssembly(typeof(AbstractFrontEndBlock)).GetTypes()
                .Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(AbstractFrontEndBlock))))
            {
                objects.Add((AbstractFrontEndBlock)Activator.CreateInstance(type));
            }
            return objects;
        }
    }
}