﻿using Frontend.Helpers.Mediators;
using Frontend.Models.Blocks;
using Frontend.ViewModels;
using Frontend.Views;

namespace Frontend.Models.GraphicViews
{
    /// <summary>
    /// Classe che rappresenta l'attributo <see cref="GraphicsView.Drawable"/> della <see cref="GraphicsView"/> presente nella <see cref="BlockView"/>
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
        /// Disegna, nel canvas passato come parametro, la blocchi posizionati contenuta nel <see cref="BlockViewModel"/>
        /// </summary>
        /// <param name="canvas"> Canvas nel quale disegnare </param>
        /// <param name="dirtyRect"> Informazioni del canvas </param>
        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            List<IFrontEndBlock>? blocks = (List<IFrontEndBlock>?)_mediator.NotifyWithReturn(this, MediatorKey.GETDROPPEDBLOCKS);
            if(blocks != null)
            {
                foreach (var block in blocks)
                    block.Draw(canvas);
            }
        }
    }
}