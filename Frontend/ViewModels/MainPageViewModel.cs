using Backend.Blocks;
using Backend.Transpilers;
using Frontend.Helpers.Mediators;
using Frontend.Models.Blocks;
using Frontend.Translators;
using System.Diagnostics;

namespace Frontend.ViewModels
{
    /// <summary>
    /// Classe che rappresenta un ViewModel per la <see cref="MainPage"/>
    /// </summary>
    public class MainViewModel : BaseViewModel
    {

        /// <summary>
        /// Nome del file salvato o caricato dal file system
        /// </summary>
        public string FileName { get => Path.GetFileName(FilePath); }

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
            FilePath = (FilePath!=null) ? FilePath : Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + $"\\ScriBrick4U\\{fileName}.json";

            string ris = (string)Mediator.NotifyWithReturn(this, MediatorKey.GETJSONDROPPEDBLOCKS);

            if (!Directory.Exists(Environment.SpecialFolder.MyDocuments + "\\ScriBrick4U"))
                try { Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\ScriBrick4U"); }
                catch (UnauthorizedAccessException) { throw new UnauthorizedAccessException("impossibile accedere alla directory personale dei documenti."); }

            File.WriteAllText(FilePath, ris);

        }

        /// <summary>
        /// Carica uno script precedentemente salvato sotto forma di file .Json
        /// </summary>
        public void LoadScript(string path)
        {
            FilePath = path;
            Mediator.Notify(this, MediatorKey.SETDROPPEDBLOCKSFROMJSON);
        }

        /// <summary>
        /// Esegue la traduzione dello script
        /// </summary>
        public void TranslateScript()
        {
            IEnumerable<IBlock> tradotti = (new Translator()).Translate((List<IFrontEndBlock>)Mediator.NotifyWithReturn(this, MediatorKey.GETDROPPEDBLOCKS));
            string code = (new Transpiler()).ConvertToCode((FileName==null) ? "script" : FileName, tradotti.AsQueryable());
            File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + $"\\test.cs", code);
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
