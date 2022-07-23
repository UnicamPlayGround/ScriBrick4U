namespace Frontend.ViewModels
{
    /// <summary>
    /// Classe che rappresenta un ViewModel per la MainPage
    /// </summary>
    public class MainViewModel : BaseViewModel
    {
        /// <summary>
        /// <see cref="Command"/> per la gestione dell'apertura di una nuova finestra del programma
        /// </summary>
        public Command NewProgram { get; set; }

        /// <summary>
        /// <see cref="Command"/> per la gestione del salvataggio dello script
        /// </summary>
        public Command SaveProgram { get; set; }

        /// <summary>
        /// <see cref="Command"/> per la gestione dell'apertura di uno script già esistente
        /// </summary>
        public Command LoadProgram { get; set; }

        /// <summary>
        /// <see cref="Command"/> per la gestione dell'esecuzione dello script
        /// </summary>
        public Command StartProgram { get; set; }

        /// <summary>
        /// <see cref="Command"/> per la gestione dell'uscita dal programma
        /// </summary>
        public Command Exit { get; set; }

        /// <summary>
        /// Costruttore di default
        /// </summary>
        public MainViewModel()
        {
            this.NewProgram = new(ProgramNew);
            this.SaveProgram = new(ProgramSave);
            this.LoadProgram = new(ProgramLoad);
            this.StartProgram = new(ProgramStart);
            this.Exit = new(ProgramExit);
        }


        /// <summary>
        /// metodo per la gestione dell'apertura di una nuova finestra del programma
        /// </summary>
        private void ProgramNew()
        {
            throw new NotImplementedException("Voce \"File > Nuovo\" non implementata.");
        }


        /// <summary>
        /// metodo per la gestione del salvataggio dello script
        /// </summary>
        private void ProgramSave()
        {
            throw new NotImplementedException("Voce \"File > Salva\" non implementata.");
        }

        /// <summary>
        /// metodo per la gestione dell'apertura di uno script già esistente
        /// </summary>
        private void ProgramLoad()
        {
            throw new NotImplementedException("Voce \"File > Carica\" non implementata.");
        }

        /// <summary>
        /// metodo per la gestione dell'esecuzione dello script
        /// </summary>
        private void ProgramStart()
        {

            throw new NotImplementedException("Voce \"Esegui\" non implementata.");
        }

        /// <summary>
        /// metodo per la gestione dell'uscita dal programma
        /// </summary>
        private void ProgramExit()
        {
            Environment.Exit(0);
        }
    }
}
