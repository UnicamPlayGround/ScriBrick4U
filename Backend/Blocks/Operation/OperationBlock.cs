using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Blocks.Operation
{
    /// <summary>
    /// Definisce il blocco per l'applicazione di operazione matematiche
    /// </summary>
    public class OperationBlock : AbstractBlock
    {
        /// <summary>
        /// Parte sinistra dell'operazione
        /// </summary>
        private string Value1 { get; set; }
        /// <summary>
        /// Parte destra dell'operazione
        /// </summary>
        private string Value2 { get; set; }
        /// <summary>
        /// Operatore da utilizzare
        /// </summary>
        private string Operator { get; set; }
        public OperationBlock(string name, string value1, string operation, string value2) : base(name)
        {
            Value1 = value1;
            Value2 = value2;
            Operator = operation;
        }

        public override string GetCode()
        {
            return $"( ({Value1}) {Operator} ({Value2}) )";
        }

        public override Dictionary<string, string> GetVariables()
        {
            return new();
        }
    }
}
