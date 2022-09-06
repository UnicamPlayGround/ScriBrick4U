using Frontend.Views;

namespace Frontend.ViewModels
{
    /// <summary>
    /// Classe che rappresenta un ViewModel per la <see cref="TabView"/>
    /// </summary>
    public class TabViewModel : BaseViewModel
    {
        /// <summary>
        /// Variabile privata che indica la tab selezionata
        /// </summary>
        private int _selectedTab;

        /// <summary>
        /// Variabile pubblica che indica la tab selezionata
        /// </summary>
        public int SelectedTab
        {
            get => _selectedTab;
            set {
                _selectedTab = value;
            }
        }


        /// <summary>
        /// Variabile privata che rappresenta i nome delle Tab
        /// </summary>
        private List<string> _names;

        /// <summary>
        /// Variabile pubblica che rappresenta i nome delle Tab
        /// </summary>
        public List<string> Names
        {
            get => _names;
            set {
                _names = value;
                OnPropertyChanged();
            }
        }



        /// <summary>
        /// Costruttore di default
        /// </summary>
        public TabViewModel()
        {
            SetMediator(this);
            Names = new() { "Tab1" };
            _selectedTab = 0;
        }


    }
}