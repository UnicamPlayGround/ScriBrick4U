using Frontend.Models.Blocks;
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
        BindingContext = context = new BlockTypeViewModel();
    }

    /// <summary>
    /// Metodo che permette di gestire la selezione di un tipo di blocco dall'apposita lista
    /// </summary>
    /// <param name="sender"> Oggetto selezionato </param>
    /// <param name="e"> Argomenti di tipo <see cref="SelectionChangedEventArgs"/> </param>
    private void BlocksCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var blockCategory = (Tuple<BlockCategory, Color>)e.CurrentSelection.ElementAt(0);
        context.SelectedCategory = blockCategory.Item1;
    }
}