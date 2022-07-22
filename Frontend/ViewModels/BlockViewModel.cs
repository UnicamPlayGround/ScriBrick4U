using Frontend.Blocks;
using ProvaMauiDragAndDrop.Helper;
using System.Text.Json;

namespace Frontend.ViewModels
{
    /// <summary>
    /// Classe che rappresenta un ViewModel per i blocchi
    /// </summary>
    public class BlockViewModel : BaseViewModel
    {
        /// <summary>
        /// lista di tipo <see cref="List{IFrontEndBlock}"/> che contiene tutti i blocchi
        /// </summary>
        private List<IFrontEndBlock> _allBlocks;

        /// <summary>
        /// lista di tipo <see cref="List{IFrontEndBlock}"/> che contiene effettivamente i blocchi mostrati all'utente, 
        /// che possono essere trascinati
        /// </summary>
        private List<IFrontEndBlock> _blocks;

        /// <summary>
        /// lista di tipo <see cref="List{IFrontEndBlock}"/> che contiene tutti i blocchi
        /// </summary>
        public List<IFrontEndBlock> Blocks
        {
            get => _blocks;
            set
            {
                _blocks = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// lista di tipo <see cref="List{IFrontEndBlock}"/> che contiene effettivamente i blocchi trascinati dall'utente
        /// </summary>
        private List<IFrontEndBlock> _droppedBlocks;

        /// <summary>
        /// lista di tipo <see cref="List{IFrontEndBlock}"/> che contiene i blocchi trascinati dall'utente
        /// </summary>
        public List<IFrontEndBlock> DroppedBlocks
        {
            get => _droppedBlocks;
            set
            {
                _droppedBlocks = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Costruttore di default
        /// </summary>
        public BlockViewModel()
        {
            InitBlocksList();
            this.DroppedBlocks = new();
        }

        /// <summary>
        /// Metodo che inizializza la lista contenente tutti i blocchi <see cref="IFrontEndBlock"/>
        /// </summary>
        private void InitBlocksList()
        {
            this._allBlocks = new();

            // questa lista, per ora inizializzata manualmente, dovrà successivamente essere inizializzata con tutti i
            // nomi "ufficiali" dei blocchi
            List<string> nomi = new() { "move", "rotate" };
            nomi.ForEach((nome) => this._allBlocks.Add(BlockGenerator.GetBlock(nome)));
            this.Blocks = this._allBlocks;
        }

        /// <summary>
        /// Metodo che permette di aggiungere, alla lista dei blocchi trascinati, un nuovo blocco <see cref="IFrontEndBlock"/>
        /// </summary>
        /// <param name="block"> blocco <see cref="IFrontEndBlock"/> trascinato da aggiungere alla lista </param>
        /// <exception cref="NullReferenceException"> se il blocco passato è nullo </exception>
        public void AddDroppedBlockBorder(IFrontEndBlock block)
        {
            if (block is null) throw new NullReferenceException("Border is null!");
            this.DroppedBlocks = new(this.DroppedBlocks) { block.GetNewInstance() };
        }

        /// <summary>
        /// Metodo che aggiorna la lista dei blocchi mostrati all'utente in base al tipo
        /// </summary>
        /// <param name="type"> tipo di blocco in base al quale filtrare la lista </param>
        public void UpdateBlocksByType(string type)
        {
            this.Blocks = this._allBlocks.FindAll((e) => e.Type.Equals(type));
        }

        /// <summary>
        /// Metodo che retituisce una stringa, in formato Json, che rappresenta i blocchi trascinati
        /// </summary>
        /// <returns></returns>
        public string GetJsonDroppedBlocks()
        {
            return JsonSerializer.Serialize(this.DroppedBlocks);
        }
    }
}
