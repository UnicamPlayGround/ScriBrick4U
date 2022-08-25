using Frontend.Model.Blocks;
using Frontend.ViewModels;

namespace Frontend.Views;

/// <summary>
/// Classe che rappresenta il file di code-behind per il file BlockTypeView.xaml, cioe' per la <see cref="ContentView"/> dei tipi di blocchi
/// </summary>
public partial class BlockTypeView : ContentView
{
    /// <summary> Variabile che rappresenta il BindingContext </summary>
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
    /// <param name="sender"> Oggetto selezionato </param>
    /// <param name="e"> Argomenti di tipo <see cref="SelectionChangedEventArgs"/> </param>
    private void BlocksType_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var blockType = (Tuple<BlockType, Color>)e.CurrentSelection.ElementAt(0);
        context.SelectedType = blockType.Item1;
    }
}