using Frontend.ViewModels;

namespace Frontend.Helpers.Mediators
{
    /// <summary>
    /// Classe che rappresenta un mediatore di default
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


        public void Register(BaseViewModel vm)
        {
            if (vm is MainViewModel mainModel && _mainViewModel == null) _mainViewModel = mainModel;
            else if (vm is BlockViewModel blocksViewModel && _blocksViewModel == null) _blocksViewModel = blocksViewModel;
            else if (vm is BlockTypeViewModel blocksTypeViewModel && _blocksTypeViewModel == null) _blocksTypeViewModel = blocksTypeViewModel;
        }

        public void Notify(object sender, MediatorKey key)
        {
            if (key == MediatorKey.UPDATEBLOCKSBYTYPE)
                _blocksViewModel.UpdateBlocksByType(_blocksTypeViewModel.SelectedType);

            if (key == MediatorKey.SETDROPPEDBLOCKSFROMJSON)
                throw new NotImplementedException();
        }

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
