using Frontend.Model.QuestionItem;
using Frontend.Model.Blocks;
using Frontend.ViewModels;
using Frontend.Model.EditPage;

namespace Frontend.EditPage;

/// <summary>
/// Classe che rappresenta il file di code-behind per il file BlockEditPage.xaml, che permette di editare un blocco
/// </summary>
public partial class BlockEditPage : ContentPage
{
    /// <summary> Variabile che rappresenta il BindingContext </summary>
    public BlockEditPageFlag Flag;

    /// <summary> <see cref="ScrollView"/> contenente il <see cref="Grid"/> dei <see cref="IBlockEditItem"/> </summary>
    private readonly ScrollView _itemsScrollView;

    /// <summary> <see cref="Grid"/> dei bottoni </summary>
    private readonly Grid _buttonsGrid;

    /// <summary> <see cref="Grid"/> delle <see cref="IFrontEndBlock.Questions"/> del blocco da editare </summary>
    private readonly List<IBlockEditItem> _editItems;

    /// <summary>
    /// Costruttore di default, che costruisce la pagina con gli <see cref="IBlockEditItem"/> del blocco passato come parametro
    /// </summary>
    /// <param name="block"> blocco da editare </param>
    /// <param name="btnEliminaEnabled"> booleano che indica se il bottone elimina debba essere abilitato o meno </param>
	public BlockEditPage(IFrontEndBlock block, bool btnEliminaEnabled)
	{
		InitializeComponent();
		BackgroundColor = Color.FromRgb(240, 120, 105);
        BindingContext = new BlockEditPageViewModel(block);

        _editItems = new(block.Questions);
        _itemsScrollView = BuildItemsGrid(block.Questions);
        _buttonsGrid = BuildButtonsGrid(btnEliminaEnabled);

        mainFrame.Content = InitMainGrid(block.Descriptor.Name);
    }

    /// <summary>
    /// Inizializza il <see cref="Grid"/> principale
    /// </summary>
    /// <param name="blockName"> nome del blocco </param>
    /// <returns> il <see cref="Grid"/> principale </returns>
    private Grid InitMainGrid(string blockName)
    {
        mainGrid.RowDefinitions.Add(new RowDefinition(75));
        for (int i = 0; i < 2; i++)
            mainGrid.RowDefinitions.Add(new RowDefinition(GridLength.Star));

        mainGrid.ColumnDefinitions.Add(new ColumnDefinition(GridLength.Star));

        mainGrid.Add(BuildLabel("Blocco " + blockName.ToUpper(), LayoutOptions.Center, LayoutOptions.Center));
        mainGrid.Add(_itemsScrollView, 0, 1);
        mainGrid.Add(_buttonsGrid, 0, 2);

        return mainGrid;
    }

    /// <summary>
    /// Costruisce una <see cref="Label"/>
    /// </summary>
    /// <param name="text"> Testo della label </param>
    /// <param name="horOptions"> Opzione per il posizionamento orizzontale </param>
    /// <param name="verOptions"> Opzione per il posizionamento verticale </param>
    /// <param name="fontSize"> Dimensioni della label </param>
    /// <param name="fontAttr"> Attributi della label </param>
    /// <returns> la <see cref="Label"/> costruita </returns>
    private Label BuildLabel(string text, LayoutOptions horOptions, LayoutOptions verOptions, double fontSize = 15, FontAttributes fontAttr = FontAttributes.Bold)
    {
        return new() {
            Text = text,
            FontSize = fontSize,
            FontAttributes = fontAttr,
            HorizontalOptions = horOptions,
            VerticalOptions = verOptions
        };
    }

    /// <summary>
    /// Costruisce la <see cref="ScrollView"/> con gli <see cref="IBlockEditItem"/> del blocco da editare
    /// </summary>
    /// <param name="items"> Lista di <see cref="IBlockEditItem"/> </param>
    /// <returns> la <see cref="ScrollView"/>, degli <see cref="IBlockEditItem"/>, costruita </returns>
    private ScrollView BuildItemsGrid(List<IBlockEditItem> items)
	{
        ScrollView v = new();

        Grid grid = new() {
            Padding = 10,
            ColumnSpacing = 10,
            RowSpacing = 10,
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
            
        };

        grid.ColumnDefinitions.Add(new ColumnDefinition(GridLength.Auto));
        grid.ColumnDefinitions.Add(new ColumnDefinition(400));

        int i = 0;
        foreach (var item in items) {
            grid.RowDefinitions.Add(new RowDefinition(GridLength.Auto));
            grid.Add(item.Question, 0, i);
            grid.Add((IView)item.Element, 1, i++);
        }

        v.Content = grid;
        return v;
    }

    /// <summary>
    /// Costruisce il <see cref="Grid"/> con i bottoni per confermare/annulare l'editing ed eliminare il blocco
    /// </summary>
    /// <param name="btnEliminaEnabled"> Booleano che indica se il bottone elimina debba essere abilitato o meno </param>
    /// <returns> il <see cref="Grid"/> dei bottoni costruito </returns>
    private Grid BuildButtonsGrid(bool btnEliminaEnabled)
    {
        List<Button> buttons = new() {
            BuildConfermaButton("Conferma"),
            BuildAnnullaButton("Annulla"),
            BuildEliminaButton("Elimina", btnEliminaEnabled)
        };

        Grid grid = new(){
            Margin = 5,
            ColumnSpacing = 5,
            HorizontalOptions = LayoutOptions.Fill,
            VerticalOptions = LayoutOptions.Center,
        };

        grid.RowDefinitions.Add(new RowDefinition(GridLength.Star));

        int i = 0;
        foreach (var button in buttons) {
            grid.ColumnDefinitions.Add(new ColumnDefinition(GridLength.Star));
            grid.Add(button, i++, 0);
        }

        return grid;
    }

    /// <summary>
    /// Costruisce il bottone per confermare l'editing del blocco
    /// </summary>
    /// <param name="text"> Testo del bottone </param>
    /// <returns> il bottone, per confermare l'editing del blocco, costruito </returns>
	private Button BuildConfermaButton(string text) {
        return BuildButton(text, new((sender, args) => {
            string errors = "";
            foreach (var question in _editItems)
                if (!question.ValidateResult()) errors += errors + question.ErrorMessage + "\n";

            if (errors == "") ClosePage(BlockEditPageFlag.CONFERMA);
            else DisplayAlert("Errore", errors, "Ok");
            }));
    }

    /// <summary>
    /// Costruisce il bottone per annullare l'editing del blocco
    /// </summary>
    /// <param name="text"> Testo del bottone </param>
    /// <returns> il bottone, per annullare l'editing del blocco, costruito </returns>
    private Button BuildAnnullaButton(string text) {
        return BuildButton(text, new((sender, args) => {
            ClosePage(BlockEditPageFlag.ANNULLA); 
        }));
    }

    /// <summary>
    /// Costruisce il bottone per eliminare il blocco
    /// </summary>
    /// <param name="text"> Testo del bottone </param>
    /// <param name="enabled"> Booleano che indica se il bottone debba essere abilitato o meno </param>
    /// <returns> il bottone, per eliminare il blocco, costruito </returns>
    private Button BuildEliminaButton(string text, bool enabled) {
        return BuildButton(text, new((sender, args) => { ClosePage(BlockEditPageFlag.ELIMINA); }), enabled);
    }

    /// <summary>
    /// Costruisce un bottone con le proprieta' specificate
    /// </summary>
    /// <param name="text"> Testo del bottone </param>
    /// <param name="clickedFunction"> Funzione che gestisce il click del bottone </param>
    /// <param name="enabled"> Booleano che indica se il bottone e' abilitato o meno </param>
    /// <returns> il bottone costruito </returns>
    private Button BuildButton(string text, EventHandler clickedFunction, bool enabled = true) {
        Button button = new() {
            Text = text,
            IsEnabled = enabled
        };

        button.Clicked += clickedFunction;
        return button;
    }

    /// <summary>
    /// Chiude la pagina, impostando l'azione da eseguire successivamente. Quest'ultima e' indicata dal parametro del metodo
    /// </summary>
    /// <param name="flag"> flag per indicare l'azione da eseguire dopo la chiusura della pagina </param>
    private void ClosePage(BlockEditPageFlag flag) {
        Flag = flag;
        (_itemsScrollView?.Content as Grid).Children.Clear();
        Navigation.PopAsync();
    }
}