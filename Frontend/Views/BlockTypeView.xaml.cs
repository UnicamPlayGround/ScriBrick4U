using Frontend.ViewModels;

namespace Frontend.Views;

/// <summary>
/// Classe che rappresenta la <see cref="ContentView"/> dei tipi dei blocchi
/// </summary>
public partial class BlockTypeView : ContentView
{
    /// <summary>
    /// variabile che rappresenta il Binding Context della <see cref="ContentView"/>
    /// </summary>
    private readonly BlockTypeViewModel context;

    /// <summary>
    /// Costruttore di default
    /// </summary>
    public BlockTypeView()
    {
        InitializeComponent();
        this.context = this.BindingContext as BlockTypeViewModel;
    }

    /// <summary>
    /// Metodo che permette di gestire la selezione di un tipo di blocco dall'apposita lista
    /// </summary>
    /// <param name="sender"> oggetto selezionato </param>
    /// <param name="e"> argomenti di tipo <see cref="SelectionChangedEventArgs"/> </param>
    private void BlocksType_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        this.context.SelectedType = (string)e.CurrentSelection.ElementAt(0);
    }
}