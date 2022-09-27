using Frontend.EditPage;
using Frontend.Models.Blocks;
using Frontend.Models.EditPage;
using Frontend.Models.GraphicViews;
using Frontend.ViewModels;

namespace Frontend.Views;

/// <summary>
/// Classe che rappresenta il file di code-behind per il file BlockView.xaml, cioe' per la <see cref="ContentView"/> dei blocchi
/// </summary>
public partial class BlockView : ContentView
{
    /// <summary> Variabile che rappresenta il BindingContext </summary>
    private readonly BlockViewModel _context;

    /// <summary> Variabile che rappresenta il blocco selezionato, che poi verra' trascinato </summary>
    private IFrontEndBlock? _SelectedBlock;
    /// <summary> <see cref="Grid"/> associato al blocco selezionato </summary>
    private Grid? _grid;


    /// <summary>
    /// Costruttore di default
    /// </summary>
    public BlockView()
    {
        InitializeComponent();
        BindingContext = _context = new BlockViewModel(DroppedBlocksGraphicsView);
    }

    /// <summary>
    /// Metodo che registra il blocco scelto
    /// </summary>
    /// <param name="dropPoint"> Punto selezionato per il posizionamento del blocco scelto </param>
    private void Drop(PointF dropPoint)
    {
        if(_SelectedBlock != null)
        {
            _context.AddDroppedBlock(_SelectedBlock, dropPoint);
            ResetBlockSelection();
        }
    }

    /// <summary>
    /// Metodo che elimina un blocco precedentemente posizionato
    /// </summary>
    /// <param name="block"> Blocco da eliminare </param>
    private void Delete(IFrontEndBlock block)
    {
        _context.DeleteDroppedBlock(block);
    }

    /// <summary>
    /// Metodo che gestisce la creazione dello <see cref="StackLayout"/> contenente gli <see cref="IFrontEndBlock.Elements"/> del blocco associato
    /// </summary>
    /// <param name="sender"> <see cref="StackLayout"/> creato </param>
    /// <param name="e"> Argomenti di tipo <see cref="EventArgs"/> </param>
    private void BlockElementsStackLayout_Loaded(object? sender, EventArgs e)
    {
        var stack = sender as StackLayout;
        var grid = stack?.Parent as Grid;
        var block = grid?.BindingContext as IFrontEndBlock;

        block?.Elements.ForEach((blockElement) =>
        {
            if (blockElement.Parent != null)
            {
                var parentLayout = blockElement.Parent as StackLayout; 
                parentLayout?.Children.Clear();
            } 
            if(stack != null)
            {
                stack.Margin = block.Shape.Margin;
                stack.Children.Add((IView)blockElement);
            }
        });
    }


    /// <summary>
    /// Gestisce la selezione di un elemento della <see cref="CollectionView"/> dei blocchi
    /// </summary>
    /// <param name="sender"> <see cref="CollectionView"/> chiamante </param>
    /// <param name="e"> Argomenti dell'evento </param>
    private void SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.Count > 0)
            if (e.CurrentSelection[0] != null)
            {
                _SelectedBlock = (e.CurrentSelection[0] as IFrontEndBlock)?.GetInfo();
            }
    }

    /// <summary>
    /// Mostra la <see cref="BlockEditPage"/>, in base ai parametri indicati
    /// </summary>
    /// <param name="block"> Blocco da editare </param>
    /// <param name="unloadedAction"> Azione da eseguire alla chiusura della pagina </param>
    /// <param name="btnEliminaEnabled"> Booleano che indica se il bottone elimina debba essere abilitato o meno </param>
    private async void ShowEditPage(IFrontEndBlock block, Action<object?, EventArgs> unloadedAction, bool btnEliminaEnabled = false)
    {
        BlockEditPage editPage = new(block, btnEliminaEnabled);
        editPage.Unloaded += new EventHandler(unloadedAction);
        await Navigation.PushAsync(editPage);
    }

    /// <summary>
    /// Gestisce il click di un elemento della <see cref="CollectionView"/> dei blocchi
    /// </summary>
    /// <param name="sender"> <see cref="Grid"/> chiamante </param>
    /// <param name="e"> Argomenti dell'evento </param>
    private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
    {
        _grid = sender as Grid;
    }

    /// <summary>
    /// Reimposta la selezione di un blocco
    /// </summary>
    private void ResetBlockSelection()
    {
        if (_grid == null) return;
        _SelectedBlock = null;
        VisualStateManager.GoToState(_grid, "Normal");
        blocksCollView.SelectedItem = null;
    }

    /// <summary>
    /// Gestisce la creazione della <see cref="GraphicsView"/> dei blocchi trascinati
    /// </summary>
    /// <param name="sender"> <see cref="GraphicsView"/> chiamante </param>
    /// <param name="e"> Argomenti dell'evento </param>
    private void DroppedBlocksGraphicsView_Loaded(object sender, EventArgs e)
    {
        var graphicsView = sender as GraphicsView;
        if(graphicsView != null)
        {
            graphicsView.Drawable = new BlockDrawable(BaseViewModel.Mediator);
            graphicsView.WidthRequest = DeviceDisplay.MainDisplayInfo.Width + 200;
            graphicsView.HeightRequest = DeviceDisplay.MainDisplayInfo.Height + 200;
        }
    }

    /// <summary>
    /// Gestisce la fine dell'interazione con la <see cref="GraphicsView"/> dei blocchi trascinati
    /// </summary>
    /// <param name="sender"> <see cref="GraphicsView"/> chiamante </param>
    /// <param name="e"> Argomenti dell'evento </param>
    private async void DroppedBlocksGraphicsView_EndInteraction(object sender, TouchEventArgs e)
    {
        if (_SelectedBlock == null)
        {
            var selectedBlock = _context.GetBlockFromPoint(e.Touches.ElementAt(0));
            if (selectedBlock != null) ShowEditPage(selectedBlock, (sender, args) => {
                if ((sender as BlockEditPage)?.Flag == BlockEditPageFlag.ELIMINA)
                    Delete(selectedBlock);
            }, true);
        }
        else
        {
            try
            {
                _context.CanBeDropped(_SelectedBlock, e.Touches.ElementAt(0));
                if (_SelectedBlock.Descriptor.Type.Equals(BlockType.Principale)) Drop(e.Touches.ElementAt(0));
                else ShowEditPage(_SelectedBlock, (sender, args) => {
                    if ((sender as BlockEditPage)?.Flag == BlockEditPageFlag.CONFERMA)
                        Drop(e.Touches.ElementAt(0));
                    ResetBlockSelection();
                });
            }
            catch (InvalidOperationException ex)
            {
                ResetBlockSelection();
                if(Application.Current != null && Application.Current.MainPage != null)
                {
                    await Application.Current.MainPage.DisplayAlert("Posizionamento blocco", ex.Message, "Ok");
                }
            }
        }
    }
}