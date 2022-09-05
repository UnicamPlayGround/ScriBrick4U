using Frontend.EditPage;
using Frontend.Model.Blocks;
using Frontend.Model.EditPage;
using Frontend.Model.GraphicViews;
using Frontend.ViewModels;

namespace Frontend.Views;

/// <summary>
/// Classe che rappresenta il file di code-behind per il file BlockView.xaml, cioe' per la <see cref="ContentView"/> dei blocchi
/// </summary>
public partial class BlockView : ContentView
{
    /// <summary> Variabile che rappresenta il BindingContext </summary>
    private readonly BlockViewModel context;

    /// <summary>
    /// Tupla che rappresenta il blocco selezionato, con il rispettivo <see cref="Grid"/>
    /// </summary>
    private (IFrontEndBlock, Grid) selected;

    /// <summary>
    /// Costruttore di default
    /// </summary>
    public BlockView()
    {
        InitializeComponent();
        BindingContext = context = new BlockViewModel(DroppedBlocksGraphicsView);
    }

    /// <summary>
    /// Metodo che gestisce la creazione dello <see cref="StackLayout"/> contenente gli <see cref="IFrontEndBlock.Elements"/> del blocco associato
    /// </summary>
    /// <param name="sender"> <see cref="StackLayout"/> creato </param>
    /// <param name="e"> Argomenti di tipo <see cref="EventArgs"/> </param>
    private void BlockElementsStackLayout_Loaded(object sender, EventArgs e)
    {
        var stack = sender as StackLayout;
        var grid = stack.Parent as Grid;
        var block = grid.BindingContext as IFrontEndBlock;

        block.Elements.ForEach((blockElement) =>
        {
            if (blockElement.Parent != null) (blockElement.Parent as StackLayout).Children.Clear();
            stack.Margin = block.Shape.Margin;
            stack.Children.Add((IView)blockElement);
        });
    }

    /// <summary>
    /// Gestisce la creazione della <see cref="GraphicsView"/> dei blocchi trascinati
    /// </summary>
    /// <param name="sender"> <see cref="GraphicsView"/> chiamante </param>
    /// <param name="e"> Argomenti dell'evento </param>
    private void DroppedBlocksGraphicsView_Loaded(object sender, EventArgs e)
    {
        var graphicsView = sender as GraphicsView;
        graphicsView.Drawable = new BlockDrawable(BaseViewModel.Mediator);
        graphicsView.WidthRequest = DeviceDisplay.MainDisplayInfo.Width + 200;
        graphicsView.HeightRequest = DeviceDisplay.MainDisplayInfo.Height + 200;
    }

    /// <summary>
    /// Gestisce la selezione di un elemento della <see cref="CollectionView"/> dei blocchi
    /// </summary>
    /// <param name="sender"> <see cref="CollectionView"/> chiamante </param>
    /// <param name="e"> Argomenti dell'evento </param>
    private void SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.Count > 0 && e.CurrentSelection[0] != null)
            selected = ((e.CurrentSelection[0] as IFrontEndBlock).GetInfo(), null);
    }

    /// <summary>
    /// Gestisce il click di un elemento della <see cref="CollectionView"/> dei blocchi
    /// </summary>
    /// <param name="sender"> <see cref="Grid"/> chiamante </param>
    /// <param name="e"> Argomenti dell'evento </param>
    private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
    {
        selected.Item2 = sender as Grid;
    }

    /// <summary>
    /// Gestisce la fine dell'interazione con la <see cref="GraphicsView"/> dei blocchi trascinati
    /// </summary>
    /// <param name="sender"> <see cref="GraphicsView"/> chiamante </param>
    /// <param name="e"> Argomenti dell'evento </param>
    private async void DroppedBlocksGraphicsView_EndInteraction(object sender, TouchEventArgs e)
    {
        if (selected.Item1 == null)
        {
            var selectedBlock = context.GetBlockFromPoint(e.Touches.ElementAt(0));
            if (selectedBlock != null)
            {
                List<string> values = new();
                selectedBlock.Questions.ForEach(question => values.Add(question.Value));

                ShowEditPage(selectedBlock, (sender, args) => {
                    if ((sender as BlockEditPage).Flag == BlockEditPageFlag.ANNULLA)
                        for (int i = 0; i < selectedBlock.Questions.Count; i++)
                            selectedBlock.Questions.ElementAt(i).SetValue(values.ElementAt(i));

                    if ((sender as BlockEditPage).Flag == BlockEditPageFlag.ELIMINA) Delete(selectedBlock);
                });
            }
            return;
        }

        try
        {
            context.CanBeDropped(selected.Item1, e.Touches.ElementAt(0));
            Drop(e.Touches.ElementAt(0));
        }
        catch (InvalidOperationException ex)
        {
            ResetBlockSelection();
            await Application.Current.MainPage.DisplayAlert("Posizionamento blocco", ex.Message, "Ok");
        }
    }

    /// <summary>
    /// Metodo che registra il blocco selezionato
    /// </summary>
    /// <param name="dropPoint"> Punto nel quale posizionare il blocco selezionato </param>
    private void Drop(PointF dropPoint) {
        if (selected.Item1.Descriptor.Type is not BlockType.Principale)
            ShowEditPage(selected.Item1, (sender, args) =>
            {
                if ((sender as BlockEditPage).Flag == BlockEditPageFlag.CONFERMA) context.AddDroppedBlock(selected.Item1, dropPoint);
                ResetBlockSelection();
            });
        else {
            context.AddDroppedBlock(selected.Item1, dropPoint);
            ResetBlockSelection();
        }
    }

    /// <summary>
    /// Metodo che elimina un blocco precedentemente posizionato
    /// </summary>
    /// <param name="block"> Blocco da eliminare </param>
    private void Delete(IFrontEndBlock block) {
        context.DeleteDroppedBlock(block);
    }

    /// <summary>
    /// Resetta la selezione di un blocco
    /// </summary>
    private void ResetBlockSelection()
    {
        if (selected.Item2 == null) return;
        VisualStateManager.GoToState(selected.Item2, "Normal");
        selected.Item1 = null; selected.Item2 = null;
    }

    /// <summary>
    /// Mostra la <see cref="BlockEditPage"/>, in base ai parametri indicati
    /// </summary>
    /// <param name="block"> Blocco da editare </param>
    /// <param name="unloadedAction"> Azione da eseguire alla chiusura della pagina </param>
    /// <param name="btnEliminaEnabled"> Booleano che indica se il bottone elimina debba essere abilitato o meno </param>
    private async void ShowEditPage(IFrontEndBlock block, Action<object, EventArgs> unloadedAction, bool btnEliminaEnabled = true)
    {
        BlockEditPage editPage = new(block, btnEliminaEnabled);
        editPage.Unloaded += new EventHandler(unloadedAction);
        await Navigation.PushAsync(editPage);
    }
}