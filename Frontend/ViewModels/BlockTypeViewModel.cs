using Frontend.Helpers.Mediators;
using Frontend.Models.Blocks;
using Frontend.Views;

namespace Frontend.ViewModels
{
    /// <summary>
    /// Classe che rappresenta un ViewModel per <see cref="BlockTypeView"/>
    /// </summary>
    public class BlockTypeViewModel : BaseViewModel
    {
        /// <summary>
        /// Lista privata di tuple contenente effettivamente tutti i tipi di blocco, con associato il rispettivo colore
        /// </summary>
        private List<Tuple<BlockType, Color>> _blockTypes;

        /// <summary>
        /// Lista pubblica di tuple contenente tutti i tipi di blocco, con associato il rispettivo colore
        /// </summary> 
        public List<Tuple<BlockType, Color>> BlockTypes
        {
            get => _blockTypes;
            set
            {
                _blockTypes = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Variabile che contiene effettivamente il tipo di blocco selezionato
        /// </summary>
        private BlockType _selectedType;

        /// <summary>
        /// Variabile pubblica che contiene il tipo di blocco selezionato
        /// </summary>
        public BlockType SelectedType
        {
            get => _selectedType;
            set
            {
                _selectedType = value;
                Mediator.Notify(this, MediatorKey.UPDATEBLOCKSBYTYPE);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Costruttore di default
        /// </summary>
        public BlockTypeViewModel()
        {
            SetMediator(this);
            BlockTypes = new();

            foreach (var blockType in Enum.GetValues(typeof(BlockType)).Cast<BlockType>().ToList())
                BlockTypes.Add(new(blockType, BlockTypeMethods.GetColor(blockType)));
        }
    }
}
