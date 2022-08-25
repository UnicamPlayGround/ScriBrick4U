using Frontend.Helpers.Mediators;
using Frontend.Model.Blocks;

namespace Frontend.Model.GraphicViews
{
    /// <summary>
    /// Classe che rappresenta l'attributo <see cref="GraphicsView.Drawable"/> 
    /// </summary>
    public class BlockDrawable : View, IDrawable
    {
        /// <summary>
        /// Istanza di <see cref="IMediator"/>
        /// </summary>
        private readonly IMediator _mediator;

        /// <summary>
        /// Costruttore di default che inizializza l'attributo <see cref="IMediator"/> con la variabile passata come parametro
        /// </summary>
        /// <param name="mediator"> Variabile con cui inizializzare l'attributo <see cref="IMediator"/> </param>
        public BlockDrawable(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Disegna nel canvas passato come parametro
        /// </summary>
        /// <param name="canvas"> Canvas nel quael disegnare </param>
        /// <param name="dirtyRect"> Informazioni del canvas </param>
        /// <exception cref="NotImplementedException"></exception>
        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            List<IFrontEndBlock> blocks = (List<IFrontEndBlock>)_mediator.NotifyWithReturn(this, MediatorKey.GETDROPPEDBLOCKS);

            foreach (var block in blocks)
                throw new NotImplementedException();
        }
    }
}