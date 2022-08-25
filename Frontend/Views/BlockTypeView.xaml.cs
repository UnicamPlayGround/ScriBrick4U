using Frontend.Model.Blocks;
using Frontend.ViewModels;

namespace Frontend.Views;

/// <summary>
/// Classe che rappresenta la <see cref="ContentView"/> dei tipi dei blocchi
/// </summary>
public partial class BlockTypeView : ContentView
{
    /// <summary>
    /// BindingContext della BlockTypeView
    /// </summary>
    private readonly BlockTypeViewModel context;

    /// <summary>
    /// Costruttore di default
    /// </summary>
    public BlockTypeView()
    {
        InitializeComponent();
        context = BindingContext as BlockTypeViewModel;
    }

    /// <summary>
    /// Metodo che permette di gestire la selezione di un tipo di blocco dall'apposita lista
    /// </summary>
    /// <param name="sender"> oggetto selezionato </param>
    /// <param name="e"> argomenti di tipo <see cref="SelectionChangedEventArgs"/> </param>
    private void BlocksType_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var blockType = (Tuple<BlockType, Color>)e.CurrentSelection.ElementAt(0);
        context.SelectedType = blockType.Item1;
    }
}