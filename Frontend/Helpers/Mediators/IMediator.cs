﻿using Frontend.ViewModels;

namespace Frontend.Helpers.Mediators
{
    /// <summary>
    /// Interfaccia che rappresenta un mediatore tra classi che ereditano da <see cref="BaseViewModel"/>
    /// </summary>
    public interface IMediator
    {
        /// <summary>
        /// Metodo che registra il ViewModel passato come parametro
        /// </summary>
        /// <param name="vm"> ViewModel da registrare nel mediatore </param>
        void Register(BaseViewModel vm);

        /// <summary>
        /// Esegue un'azione, in base alla chiave passata come parametro, senza restituire un risultato
        /// </summary>
        /// <param name="sender"> Oggetto chiamante </param>
        /// <param name="key"> Chiave che rappresenta l'azione da eseguire </param>
        /// <exception cref="NotImplementedException"></exception>
        void Notify(object sender, MediatorKey key);

        /// <summary>
        /// Esegue un'azione, in base alla chiave passata come parametro, e restituisce un risultato
        /// </summary>
        /// <param name="sender"> Oggetto chiamante </param>
        /// <param name="key"> Chiave che rappresenta l'azione da eseguire </param>
        /// <returns>risultato dell'esecuzione dell'azione</returns>
        object? NotifyWithReturn(object sender, MediatorKey key);
    }
}
