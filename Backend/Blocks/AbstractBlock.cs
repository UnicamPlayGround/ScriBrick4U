using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Blocks
{
    /// <summary>
    ///     Classe astratta per implementazione funzionalità di un blocco
    /// </summary>
    internal class AbstractBlock : IBlock
    {
        public string Type { get; set; } = null!;
        public string Name { get; set; } = null!;
        public List<IBlock>? Children { get; set; }
        public IBlock? Next { get; set; }
        public IBlock? Prev { get; set; }
    }
}
