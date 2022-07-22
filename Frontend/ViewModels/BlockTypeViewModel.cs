﻿

using Frontend.Helpers;

namespace Frontend.ViewModels
{
    /// <summary>
    /// Classe che rappresenta un ViewModel per i tipi dei blocchi
    /// </summary>
    public class BlockTypeViewModel : BaseViewModel
    {
        /// <summary>
        /// lista di tipo <see cref="List{String}"/> che contiene effettivamente tutti i tipi di blocco
        /// </summary>
        private List<string> _blocksType;

        /// <summary>
        /// lista di tipo <see cref="List{String}"/> che contiene tutti i tipi di blocco
        /// </summary>
        public List<string> BlocksType
        {
            get => _blocksType;
            set
            {
                _blocksType = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// variabile che contiene effetivamente il tipo di blocco selezionato
        /// </summary>
        private string _selectedType;

        /// <summary>
        /// variabile che contiene il tipo di blocco selezionato
        /// </summary>
        public string SelectedType
        {
            get => _selectedType;
            set
            {
                _selectedType = value;
                Mediator.GetInstance().Execute(this, "updateBlocks");
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Costruttore di default
        /// </summary>
        public BlockTypeViewModel()
        {
            InitBlocksTypeList();
            this.SelectedType = this.BlocksType.FirstOrDefault();
        }

        /// <summary>
        /// Metodo che inizializza la lista contenente tutti i tipi di blocco
        /// </summary>
        private void InitBlocksTypeList()
        {
            this.BlocksType = new() { "movement", "conditional" };
        }
    }
}