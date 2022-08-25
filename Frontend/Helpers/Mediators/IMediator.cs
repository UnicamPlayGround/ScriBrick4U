using Frontend.ViewModels;

namespace Frontend.Helpers.Mediators
{
    public interface IMediator
    {
        void Register(BaseViewModel vm);

        void Notify(object sender, MediatorKey key);

        object NotifyWithReturn(object sender, MediatorKey key);
    }
}
