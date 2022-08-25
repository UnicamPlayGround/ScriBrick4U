using Frontend.Model.Blocks;
using Frontend.Model.GraphicViews;
using Frontend.ViewModels;

namespace Frontend.Views;

/// <summary>
/// Classe che rappresenta il file di code-behind per BlockView.xaml
/// </summary>
public partial class BlockView : ContentView
{
    /// <summary> variabile che rappresenta il blocco selezionato, che poi verra' trascinato </summary>
    private IFrontEndBlock SelectedBlock;
    /// <summary>
    /// 
    /// </summary>
    private Grid _grid;
    /// <summary> variabile che rappresenta il Binding Context della <see cref="ContentView"/> </summary>
    private readonly BlockViewModel context;

    /// <summary>
    /// Costruttore di default
    /// </summary>
    public BlockView()
    {
        InitializeComponent();
        BindingContext = context = new BlockViewModel(DroppedBlocksGraphicsView);
    }

    /// <summary>
    /// Metodo che permette di registrare il blocco trascinato
    /// </summary>
    /// <param name="dropPoint"> punto nel quale è stato rilasciato il blocco </param>
    private void Drop(PointF dropPoint)
    {
        context.AddDroppedBlockBorder(SelectedBlock, dropPoint);
    }

    /// <summary>
    /// Metodo che aggiunge gli oggetti <see cref="IView"/> del blocco <see cref="IFrontEndBlock"/> ad uno <see cref="StackLayout"/>
    /// </summary>
    /// <param name="sender"> <see cref="StackLayout"/> che è stato creato </param>
    /// <param name="e"> argomenti di tipo <see cref="EventArgs"/> </param>
    private void BlocksStackLayout_Loaded(object sender, EventArgs e)
    {
        var stack = sender as StackLayout;
        var grid = stack.Parent as Grid;
        var block = grid.BindingContext as IFrontEndBlock;

        block.Elements.ForEach((blockElement) =>
        {
            if (blockElement.Parent != null) (blockElement.Parent as StackLayout).Children.Clear();
            stack.Children.Add((IView)blockElement);
        });
    }


    /// <summary>
    /// Gestisce la selezione di un elemento della <see cref="CollectionView"/> dei blocchi
    /// </summary>
    /// <param name="sender"> <see cref="CollectionView"/> chiamante </param>
    /// <param name="e"> argomenti dell'evento </param>
    private void SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.Count > 0)
            if (e.CurrentSelection[0] != null)
            {
                SelectedBlock = (e.CurrentSelection[0] as IFrontEndBlock).GetNewInstance();
            }
    }

    /// <summary>
    /// Gestisce il click di un elemento della <see cref="CollectionView"/> dei blocchi
    /// </summary>
    /// <param name="sender"> <see cref="Grid"/> chiamante </param>
    /// <param name="e"> argomenti dell'evento </param>
    private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
    {

    }

    /// <summary>
    /// Gestisce la creazione della <see cref="GraphicsView"/> dei blocchi trascinati
    /// </summary>
    /// <param name="sender"> <see cref="GraphicsView"/> chiamante </param>
    /// <param name="e"> argomenti dell'evento </param>
    private void DroppedBlocksGraphicsView_Loaded(object sender, EventArgs e)
    {
        var graphicsView = sender as GraphicsView;
        graphicsView.Drawable = new BlockDrawable(BaseViewModel.Mediator);
        graphicsView.WidthRequest = DeviceDisplay.MainDisplayInfo.Width + 200;
        graphicsView.HeightRequest = DeviceDisplay.MainDisplayInfo.Height + 200;
    }

    /// <summary>
    /// Gestisce la fine dell'interazione con la <see cref="GraphicsView"/> dei blocchi trascinati
    /// </summary>
    /// <param name="sender"> <see cref="GraphicsView"/> chiamante </param>
    /// <param name="e"> argomenti dell'evento </param>
    private void DroppedBlocksGraphicsView_EndInteraction(object sender, TouchEventArgs e)
    {
        Drop(e.Touches.ElementAt(0));
    }
}