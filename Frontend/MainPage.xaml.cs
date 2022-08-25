using Frontend.ViewModels;

namespace Frontend
{
    /// <summary>
    /// Classe che rappresenta il file di code-behind per il file MainPage.xaml, cioe' per la pagina principale
    /// </summary>
    public partial class MainPage : ContentPage
    {
        /// <summary> Variabile che rappresenta il BindingContext </summary>
        private readonly MainViewModel _context;

        /// <summary>
        /// Costruttore di default
        /// </summary>
        public MainPage()
        {
            InitializeComponent();
            _context = BindingContext as MainViewModel;
        }

        /// <summary>
        /// Gestisce il click della voce del menù per aprire una nuova finestra del programma, richiamando un'apposito metodo
        /// nel <see cref="MainViewModel"/>
        /// </summary>
        /// <param name="sender"> Oggetto chiamante </param>
        /// <param name="e"> Parametri dell'evento click </param>
        private void New_Clicked(object sender, EventArgs e)
        {
            _context.NewProgram();
        }

        /// <summary>
        /// Gestisce il click della voce del menù per chiudere il programma, chiedendone conferma e richiamando un'apposito metodo
        /// nel <see cref="MainViewModel"/>
        /// </summary>
        /// <param name="sender"> Oggetto chiamante </param>
        /// <param name="e"> Parametri dell'evento click </param>
        private async void Close_Clicked(object sender, EventArgs e)
        {
            if (await DisplayAlert("Esci", "Chiudere il programma?", "Si", "No"))
                _context.ExitProgram();
        }

        /// <summary>
        /// Gestisce il click della voce del menù per salvare lo script, chiedendone il nome e richiamando un'apposito metodo
        /// nel <see cref="MainViewModel"/>
        /// </summary>
        /// <param name="sender"> Oggetto chiamante </param>
        /// <param name="e"> Parametri dell'evento click </param>
        private async void SaveScript_Clicked(object sender, EventArgs e)
        {
            string fileName = "";

            try
            {
                if (!string.IsNullOrEmpty(_context.FilePath)) fileName = _context.FilePath;
                else
                    while (fileName == "")
                        fileName = await DisplayPromptAsync("Salva file", "Digita il nome del file", "Salva", "Annulla");

                if (fileName != null)
                {

                    _context.SaveScript(fileName);
                    await DisplayAlert("File salvato", $"Il file {_context.FileName} è stato salvato con successo.", "Ok");
                }
            }
            catch (UnauthorizedAccessException ex) { await DisplayAlert("Accesso non autorizzato", $"Errore: il file {fileName}.json non è stato salvato (" + ex.Message + ").", "Ok"); }
        }

        /// <summary>
        /// Gestisce il click della voce del menù per caricare uno script, chiedendone la selezione dal file system e richiamando 
        /// un'apposito metodo nel <see cref="MainViewModel"/>
        /// </summary>
        /// <param name="sender"> Oggetto chiamante </param>
        /// <param name="e"> Parametri dell'evento click </param>
        private async void LoadScript_Clicked(object sender, EventArgs e)
        {
            var file = await FilePicker.PickAsync();

            if (file != null)
                if (!file.FileName.EndsWith("json", StringComparison.OrdinalIgnoreCase))
                    await DisplayAlert("Estensione file", "Il file deve avere estensione .json per poter essere caricato.", "Ok");
                else _context.LoadScript(file.FullPath);
        }

        /// <summary>
        /// Gestisce il click della voce del menù per tradurre uno script, richiamando un'apposito metodo 
        /// nel <see cref="MainViewModel"/>
        /// </summary>
        /// <param name="sender"> Oggetto chiamante </param>
        /// <param name="e"> Parametri dell'evento click </param>
        private async void TranslateScript_Clicked(object sender, EventArgs e)
        {
            _context.TranslateScript();
            await DisplayAlert("Script tradotto", "Lo script è stato tradotto con successo.", "Ok");
        }
    }
}