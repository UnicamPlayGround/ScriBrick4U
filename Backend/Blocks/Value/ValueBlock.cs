using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Blocks.Value
{
    /// <summary>
    /// Definisce il blocco per contenere valori
    /// </summary>
    public class ValueBlock : AbstractBlock
    {
        /// <summary>
        /// Valore del blocco
        /// </summary>
        public int Value { set; get; }
        public ValueBlock(string name, int value) : base(name)
        {
            Value = value;
        }

        public override string GetCode()
        {
            return $"{Value}";
        }

        public override Dictionary<string, string> GetVariables()
        {
            throw new NotImplementedException();
        }
    }
}
