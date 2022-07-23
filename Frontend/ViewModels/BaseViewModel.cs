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
        /// Evento che rappresenta il cambiamento di una prorpietà del ViewModel
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Metodo che permette di notificare l'avvenuto cambiamento di una proprietà del ViewModel
        /// </summary>
        /// <param name="name"> nome della proprietà del ViewModel che è cambiata </param>
        public void OnPropertyChanged([CallerMemberName] string name = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}