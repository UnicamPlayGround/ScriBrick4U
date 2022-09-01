using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Blocks.Variable
{
    /// <summary>
    /// Definisce il blocco per contenere valori
    /// </summary>
    public class VariableBlock : AbstractBlock
    {
        /// <summary>
        /// Valore del blocco
        /// </summary>
        public int Value { set; get; }
        public VariableBlock(string name, int value) : base(name)
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
