using Frontend.Helpers.Mediators;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Frontend.ViewModels
{
    /// <summary>
    /// Classe che rappresenta un ViewModel di base
    /// </summary>
    public class BaseViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Istanza di <see cref="IMediator"/> per mediare tra i ViewModel
        /// </summary>
        public static IMediator Mediator { get; set; }

        /// <summary>
        /// Evento che rappresenta il cambiamento di una prorpietà del ViewModel
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        
        /// <summary>
        /// Metodo che imposta l'istanza di <see cref="IMediator"/>, registrando il ViewModel passato come parametro
        /// </summary>
        /// <param name="viewModel">ViewModel da registrare nell'<see cref="IMediator"/></param>
        public static void SetMediator(BaseViewModel viewModel)
        {
            Mediator ??= new DefaultMediator();
            Mediator.Register(viewModel);
        }

        /// <summary>
        /// Metodo che permette di notificare l'avvenuto cambiamento di una proprietà del ViewModel
        /// </summary>
        /// <param name="name"> nome della proprietà del ViewModel che è cambiata </param>
        public void OnPropertyChanged([CallerMemberName] string name = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}