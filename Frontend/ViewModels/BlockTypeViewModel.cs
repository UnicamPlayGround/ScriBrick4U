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
        private List<Tuple<BlockCategory, Color>> _blockCategory = new();
        /// <summary>
        /// Lista pubblica di tuple contenente tutti i tipi di blocco, con associato il rispettivo colore
        /// </summary> 
        public List<Tuple<BlockCategory, Color>> BlockCategory
        {
            get => _blockCategory;
            set
            {
                _blockCategory = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Variabile che contiene effettivamente il tipo di blocco selezionato
        /// </summary>
        private BlockCategory _selectedCategory;
        /// <summary>
        /// Variabile pubblica che contiene il tipo di blocco selezionato
        /// </summary>
        public BlockCategory SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                _selectedCategory = value;
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
            BlockCategory = new();

            foreach (var blockType in Enum.GetValues(typeof(BlockCategory)).Cast<BlockCategory>().ToList())
                BlockCategory.Add(new(blockType, BlockCategoryMethods.GetColor(blockType)));
        }

       /// <summary>
       /// Imposta la categoria selezionata con quella passata come parametro
       /// </summary>
       /// <param name="selectedCategory"> Categoria selezionata </param>
        public void SetSelectedCategory(BlockCategory selectedCategory) {
            SelectedCategory = selectedCategory;
        }
    }
}