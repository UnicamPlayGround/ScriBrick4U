using Frontend.EditPage;
using Frontend.Model.Blocks;
using Frontend.Model.GraphicViews;
using Frontend.Models.EditPage;
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
        context.AddDroppedBlock(SelectedBlock, dropPoint);
        ResetBlockSelection();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="block"></param>
    private void Delete(IFrontEndBlock block)
    {
        context.DeleteDroppedBlock(block);
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
                SelectedBlock = (e.CurrentSelection[0] as IFrontEndBlock).GetInfo();
            }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="block"></param>
    /// <param name="unloadedAction"></param>
    /// <param name="btnEliminaEnabled"></param>
    private async void ShowEditPage(IFrontEndBlock block, Action<object, EventArgs> unloadedAction, bool btnEliminaEnabled = false)
    {
        BlockEditPage editPage = new(block, btnEliminaEnabled);
        editPage.Unloaded += new EventHandler(unloadedAction);
        await Navigation.PushAsync(editPage);
    }

    /// <summary>
    /// Gestisce il click di un elemento della <see cref="CollectionView"/> dei blocchi
    /// </summary>
    /// <param name="sender"> <see cref="Grid"/> chiamante </param>
    /// <param name="e"> argomenti dell'evento </param>
    private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
    {
        _grid = sender as Grid;
    }

    /// <summary>
    /// 
    /// </summary>
    private void ResetBlockSelection()
    {
        if (_grid == null) return;
        SelectedBlock = null;
        VisualStateManager.GoToState(_grid, "Normal");
        blocksCollView.SelectedItem = null;
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
    private async void DroppedBlocksGraphicsView_EndInteraction(object sender, TouchEventArgs e)
    {
        if (SelectedBlock == null)
        {
            var selectedBlock = context.GetBlockFromPoint(e.Touches.ElementAt(0));
            if (selectedBlock != null) ShowEditPage(selectedBlock, (sender, args) => {
                if ((sender as BlockEditPage).Flag == BlockEditPageFlag.ELIMINA)
                    Delete(selectedBlock);
            }, true);
        }
        else
        {
            try
            {
                context.CanBeDropped(SelectedBlock, e.Touches.ElementAt(0));
                if (SelectedBlock.Descriptor.Type.Equals(BlockType.Principale)) Drop(e.Touches.ElementAt(0));
                else ShowEditPage(SelectedBlock, (sender, args) => {
                    if ((sender as BlockEditPage).Flag == BlockEditPageFlag.CONFERMA)
                        Drop(e.Touches.ElementAt(0));
                    ResetBlockSelection();
                });
            }
            catch (InvalidOperationException ex)
            {
                ResetBlockSelection();
                await Application.Current.MainPage.DisplayAlert("Posizionamento blocco", ex.Message, "Ok");
            }
        }
    }
}