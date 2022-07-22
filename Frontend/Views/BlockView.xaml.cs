using Frontend.Blocks;
using Frontend.ViewModels;

namespace Frontend.Views;

/// <summary>
/// Classe che rappresenta la <see cref="ContentView"/> dei blocchi
/// </summary>
public partial class BlockView : ContentView
{
    /// <summary>
    /// variabile che rappresenta il Binding Context della <see cref="ContentView"/>
    /// </summary>
    private readonly BlockViewModel context;

    /// <summary>
    /// Costruttore di default
    /// </summary>
    public BlockView()
    {
        InitializeComponent();
        this.context = this.BindingContext as BlockViewModel;
    }

    /// <summary>
    /// Metodo che permette di gestire l'inizio del trascinamento del blocco
    /// </summary>
    /// <param name="sender"> oggetto che ha invocato il metodo </param>
    /// <param name="e"> argomenti di tipo <see cref="DragStartingEventArgs"/> </param>
    private void DragStarting(object sender, DragStartingEventArgs e)
    {
        e.Data.Properties.Add("block", (((sender as Element)).BindingContext as IFrontEndBlock));
    }

    /// <summary>
    /// Metodo che permette di gestire il momento nel quale il blocco si trova sopra l'area nella quale verrà rilasciato
    /// </summary>
    /// <param name="sender"> oggetto che ha invocato il metodo </param>
    /// <param name="e"> argomenti di tipo <see cref="DragEventArgs"/> </param>
    private void DragOver(object sender, DragEventArgs e)
    {
        e.AcceptedOperation = DataPackageOperation.Copy;
    }


    /// <summary>
    /// Metodo che permette di gestire il rilascio del blocco
    /// </summary>
    /// <param name="sender"> oggetto che ha invocato il metodo </param>
    /// <param name="e"> argomenti di tipo <see cref="DropEventArgs"/> </param>
    private void Drop(object sender, DropEventArgs e)
    {
        var property = e.Data.Properties["block"];

        if (property == null) return;

        this.context.AddDroppedBlockBorder((IFrontEndBlock)property);
    }

    /// <summary>
    /// Metodo invocato quando lo <see cref="StackLayout"/> della lista di tutti i blocchi viene creato
    /// </summary>
    /// <param name="sender"> <see cref="StackLayout"/> che è stato creato </param>
    /// <param name="e"> argomenti di tipo <see cref="EventArgs"/> </param>
    private void Draggable_StackLayout_Loaded(object sender, EventArgs e)
    {
        AddStackLayoutElements(sender, e);
    }

    /// <summary>
    /// Metodo invocato quando lo <see cref="StackLayout"/> della lista dei blocchi trascinati viene creato
    /// </summary>
    /// <param name="sender"> <see cref="StackLayout"/> che è stato creato </param>
    /// <param name="e"> argomenti di tipo <see cref="EventArgs"/> </param>
    private void Dropped_StackLayout_Loaded(object sender, EventArgs e)
    {
        AddStackLayoutElements(sender, e);
    }

    /// <summary>
    /// Metodo che aggiunge gli oggetti <see cref="IView"/> del blocco <see cref="IFrontEndBlock"/> ad uno <see cref="StackLayout"/>
    /// </summary>
    /// <param name="sender"> <see cref="StackLayout"/> che è stato creato </param>
    /// <param name="e"> argomenti di tipo <see cref="EventArgs"/> </param>
    private void AddStackLayoutElements(object sender, EventArgs e)
    {
        var s = sender as StackLayout;
        var elements = (((s.Parent as Grid)).BindingContext as IFrontEndBlock).Elements;

        if (s == null || elements == null) return;

        elements.ForEach((e) =>
        {
            if (e.Parent != null) (e.Parent as StackLayout).Children.Clear();
            s.Children.Add(e);
        });
    }

}