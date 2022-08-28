using Backend.Blocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Transpilers
{
    /// <summary>
    /// Interfaccia che definisce il traduttore per la creazione degli script C# per unity
    /// </summary>
    public interface ITranspiler
    {
        /// <summary>
        /// Funzione per la conversione da una lista di blocchi ad una string contenente il codice C# per Unity
        /// </summary>
        /// <param name="className">Nome della classe C#</param>
        /// <param name="blocks"><c>IQueryable</c> contenente tutti i blocchi iniziali per la creazione del codice C#</param>
        /// <returns><c>string</c> contenente il codice C#</returns>
        public string ConvertToCode(string className, IQueryable<IBlock> blocks);
    }
}
