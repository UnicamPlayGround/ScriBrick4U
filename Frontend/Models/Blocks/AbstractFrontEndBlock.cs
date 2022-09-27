using Frontend.Models.Blocks.Bounds;
using Frontend.Models.Blocks.Descriptors;
using Frontend.Models.Blocks.Shapes;
using Frontend.Models.QuestionItem;
using SkiaSharp;
using System.Numerics;
using System.Reflection;
using Color = Microsoft.Maui.Graphics.Color;
using Font = Microsoft.Maui.Graphics.Font;
using IBlockShape = Frontend.Models.Blocks.Shapes.IBlockShape;

namespace Frontend.Models.Blocks
{
    /// <summary>
    /// Classe astratta per tutti i blocchi lato front-end
    /// </summary>
    public abstract class AbstractFrontEndBlock : IFrontEndBlock
    {
        public float Width { get; set; }
        public float Height { get; set; }

        public IBlockDescriptor Descriptor { get; set; } = null!;
        public IBlockShape Shape { get; set; } = null!;
        public IBlockBound Position { get; set; } = null!;

        public List<IBlockEditItem> Questions { get; set; } = null!;
        public Func<string> TextDropped { get; set; } = null!;

        public IFrontEndBlock? Father { get; set; }
        public IFrontEndBlock? Next { get; set; }
        public List<IFrontEndBlock> Children { get; set; } = new();

        /// <summary> Lista privata che rappresenta gli elementi contenuti nel blocco </summary>
        private List<Element> _elements = new();
        public List<Element> Elements
        {
            get => _elements;
            set => _elements = value;
        }

        public bool CanContainChildren => Shape.Type.Equals(ShapeType.WITH_CHILDREN);
        public bool IsStart => Descriptor.Type.Equals(BlockType.Principale) || Descriptor.Type.Equals(BlockType.DefinizioneFunzione);

        public abstract IFrontEndBlock GetInfo();

        public void Draw(ICanvas canvas)
        {
            canvas.FillColor = Descriptor.BackgroundColor;
            canvas.Font = Font.DefaultBold;
            canvas.FontSize = (float)((Label)Elements.ElementAt(0)).FontSize;
            canvas.FontColor = Color.FromRgb(255, 255, 255);

            string text = TextDropped.Invoke();

            var defaultWidth = Width;
            var defaultHeight = Height;
            Width += text.Length + 25;
            Shape.Path = Shape.GetSvgPath(Width, Height);

            var pathf = ((IFrontEndBlock)this).PointsToPath(SKPath.ParseSvgPathData(Shape.Path).Points);
            pathf.Transform(Matrix3x2.CreateTranslation(Position.UpperLeft.X, Position.UpperLeft.Y));
            canvas.FillPath(pathf);
            canvas.DrawPath(pathf);

            Position.Width = pathf.Bounds.Width;
            Position.Height = pathf.Bounds.Height;

            canvas.DrawString(text, pathf.Bounds.Left, (float)(pathf.Bounds.Top + Shape.Margin.Top), pathf.Bounds.Width, pathf.Bounds.Height, HorizontalAlignment.Center, VerticalAlignment.Top);
            Width = defaultWidth;
            Height = defaultHeight;
        }

        /// <summary>
        /// Restituisce una lista contenente un'istanza per ogni classe che eredita da quella corrente
        /// </summary>
        /// <returns> una lista contenente un'istanza per ogni classe che eredita da quella corrente </returns>
        public static IEnumerable<AbstractFrontEndBlock> GetEnumerableOfType()
        {
            List<AbstractFrontEndBlock> objects = new();
            Assembly? assembly = Assembly.GetAssembly(typeof(AbstractFrontEndBlock));
            if(assembly != null)
            {
                foreach (Type type in
                assembly.GetTypes().Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(AbstractFrontEndBlock))))
                {
                    AbstractFrontEndBlock? block = Activator.CreateInstance(type) as AbstractFrontEndBlock;
                    if(block != null)
                    {
                        objects.Add(block);
                    }
                }
            }
            return objects;
        }
    }
}