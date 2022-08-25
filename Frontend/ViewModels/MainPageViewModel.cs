using Frontend.Helpers.Mediators;
using System.Diagnostics;

namespace Frontend.ViewModels
{
    /// <summary>
    /// Classe che rappresenta un ViewModel per la MainPage
    /// </summary>
    public class MainViewModel : BaseViewModel
    {
        /// <summary>
        /// Path del file salvato o caricato dal file system
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// Costruttore di default
        /// </summary>
        public MainViewModel()
        {
            SetMediator(this);
        }


        /// <summary>
        /// Apre una nuova finestra del programma
        /// </summary>
        public void NewProgram()
        {
            Process.Start(Environment.ProcessPath);
        }


        /// <summary>
        /// Salva lo script, sotto forma di file .Json, nella directory personale dei documenti
        /// </summary>
        public void SaveScript(string fileName)
        {
            FilePath = fileName ?? throw new ArgumentNullException(nameof(fileName), "il nome del file non può essere nullo");

            string ris = (string)Mediator.NotifyWithReturn(this, MediatorKey.GETJSONDROPPEDBLOCKS);

            if (!Directory.Exists(Environment.SpecialFolder.MyDocuments + "\\ScriBrick4U"))
                try { Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\ScriBrick4U"); }
                catch (UnauthorizedAccessException) { throw new UnauthorizedAccessException("impossibile accedere alla directory personale dei documenti."); }

            File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + $"\\ScriBrick4U\\{FilePath}.json", ris);

        }

        /// <summary>
        /// Carica uno script precedentemente salvato sotto forma di file .Json
        /// </summary>
        public void LoadScript(string path)
        {
            if (path == null) return;

            FilePath = path;
            Mediator.Notify(this, MediatorKey.SETDROPPEDBLOCKSFROMJSON);
        }

        /// <summary>
        /// Esegue la traduzione dello script
        /// </summary>
        public void TranslateScript()
        {
            throw new NotImplementedException("Voce \"Esegui\" non implementata.");
        }

        /// <summary>
        /// Provoca l'uscita dal programma
        /// </summary>
        public void ExitProgram()
        {
            Environment.Exit(0);
        }
    }
}
