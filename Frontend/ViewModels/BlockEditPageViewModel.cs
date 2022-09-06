using Frontend.Models.Blocks;

namespace Frontend.ViewModels
{
    /// <summary>
    /// Classe che rappresenta un ViewModel per la MainPage
    /// </summary>
    public class BlockEditPageViewModel : BaseViewModel
    {
        /// <summary> Variabile privata che rappresenta il blocco da editare </summary>
        private IFrontEndBlock _block;
        /// <summary> Variabile pubblica che rappresenta il blocco da editare </summary>
        public IFrontEndBlock Block
        {
            get => _block;
            set
            {
                _block = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Costruttore di defualt
        /// </summary>
        /// <param name="block"> Blocco da editare </param>
        public BlockEditPageViewModel(IFrontEndBlock block) {
            SetMediator(this);
            Block = block;
        }
    }
}