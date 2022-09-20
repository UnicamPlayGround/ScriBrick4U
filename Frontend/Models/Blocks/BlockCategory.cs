﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frontend.Models.Blocks
{
    public enum BlockCategory
    {
        /// <summary> Categoria blocchi iniziali</summary>
        Principale,

        /// <summary> Categoria blocchi funzioni </summary>
        Funzione,

        /// <summary> Categoria blocchi di controllo </summary>
        Controllo,

        /// <summary> Categoria blocchi eventi </summary>
        Evento,

        /// <summary> Categoria blocchi per il movimento </summary>
        Movimento,

        /// <summary>
        /// Categoria blocchi per le operazioni sulle variabili
        /// </summary>
        Variabile,
    }

    static class BlockCategoryMethods
    {
        /// <summary>
        /// Dizionario contenente tutti i tipi di blocco con associato il rispettivo colore
        /// </summary>
        private static readonly Dictionary<BlockCategory, Color> _dictionary = new() {
            { BlockCategory.Principale, Color.FromRgb(255, 165, 0) },
            { BlockCategory.Funzione, Color.FromRgb(255, 0, 0) },
            { BlockCategory.Controllo, Color.FromRgb(0, 0, 255) },
            { BlockCategory.Evento, Color.FromRgb(204, 0, 102) },
            { BlockCategory.Movimento, Color.FromRgb(0, 102, 0) },
            { BlockCategory.Variabile, Color.FromRgb(96, 96, 96) },
        };

        /// <summary>
        /// Restituisce il colore associato al tipo di blocco passato come parametro
        /// </summary>
        /// <param name="type"> Tipo di blocco del quale estrarre il colore </param>
        /// <returns> il colore associato al tipo di blocco passato come parametro </returns>
        public static Color GetColor(BlockCategory type)
        {
            return _dictionary[type];
        }
    }
}
