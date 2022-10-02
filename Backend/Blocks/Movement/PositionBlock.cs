using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Backend.Blocks.Movement
{
    /// <summary>
    /// Definisce il blocco per il riposizionamento di un elemento
    /// </summary>
    public class PositionBlock : AbstractBlock
    {
        /// <summary>
        /// Blocco contente l'ordinata
        /// </summary>
        public string X { get; set; }
        /// <summary>
        /// Blocco contente l'ascissa
        /// </summary>
        public string Y { get; set; }
        public PositionBlock(string name, string x, string y) : base(name)
        {
            X = x;
            Y = y;
        }

        public override string GetCode()
        {
            string code = "";
            code += $"transform.position = new Vector3({X}f, {Y}f, 0);\n";
            return code;
        }
    }
}
