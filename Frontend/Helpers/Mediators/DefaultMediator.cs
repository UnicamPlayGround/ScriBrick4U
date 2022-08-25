using Frontend.ViewModels;

namespace Frontend.Helpers.Mediators
{
    /// <summary>
    /// Classe che rappresenta un Mediator di default
    /// </summary>
    public class DefaultMediator : IMediator
    {
        /// <summary>
        /// Istanza di <see cref="MainViewModel"/>
        /// </summary>
        private MainViewModel _mainViewModel;
        /// <summary>
        /// Istanza di <see cref="BlockViewModel"/>
        /// </summary>
        private BlockViewModel _blocksViewModel;
        /// <summary>
        /// Istanza di <see cref="BlockTypeViewModel"/>
        /// </summary>
        private BlockTypeViewModel _blocksTypeViewModel;

        /// <summary>
        /// Metodo che registra il ViewModel passato come parametro
        /// </summary>
        /// <param name="vm">ViewModel da registrare nel Mediator</param>
        public void Register(BaseViewModel vm)
        {
            if (vm is MainViewModel mainModel && _mainViewModel == null) _mainViewModel = mainModel;
            else if (vm is BlockViewModel blocksViewModel && _blocksViewModel == null) _blocksViewModel = blocksViewModel;
            else if (vm is BlockTypeViewModel blocksTypeViewModel && _blocksTypeViewModel == null) _blocksTypeViewModel = blocksTypeViewModel;
        }

        /// <summary>
        /// Esegue un'azione, in base alla chiave <see cref="MediatorKey"/> passata come parametro, senza restituire un risultato
        /// </summary>
        /// <param name="sender">Oggetto chiamante</param>
        /// <param name="key">Chiave che rappresenta l'azione da eseguire</param>
        /// <exception cref="NotImplementedException"></exception>
        public void Notify(object sender, MediatorKey key)
        {
            if (key == MediatorKey.UPDATEBLOCKSBYTYPE)
                _blocksViewModel.UpdateBlocksByType(_blocksTypeViewModel.SelectedType);

            if (key == MediatorKey.SETDROPPEDBLOCKSFROMJSON)
                throw new NotImplementedException();
        }

        /// <summary>
        /// Esegue un'azione, in base alla chiave <see cref="MediatorKey"/> passata come parametro, e restituisce un risultato
        /// </summary>
        /// <param name="sender">Oggetto chiamante</param>
        /// <param name="key">Chiave che rappresenta l'azione da eseguire</param>
        /// <returns>Risultato dell'esecuzione dell'azione</returns>
        public object NotifyWithReturn(object sender, MediatorKey key)
        {
            if (key == MediatorKey.GETDROPPEDBLOCKS)
                return _blocksViewModel.DroppedBlocks;

            if (key == MediatorKey.GETJSONDROPPEDBLOCKS)
                return _blocksViewModel.GetJsonDroppedBlocks();

            return null;
        }
    }
}
