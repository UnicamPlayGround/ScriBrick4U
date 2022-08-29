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
        /// Blocco contenente il valore della parte sinistra dell'operazione
        /// </summary>
        private IBlock Value1 { get; set; }
        /// <summary>
        /// Blocco contenente il valore della parte destra dell'operazione
        /// </summary>
        private IBlock Value2 { get; set; }
        /// <summary>
        /// Operatore da utilizzare
        /// </summary>
        private string Operator { get; set; }
        public OperationBlock(string name, IBlock value1, string operation, IBlock value2) : base("Operation", name)
        {
            Value1 = value1;
            Value2 = value2;
            Operator = operation;
        }

        public override string GetCode()
        {
            return $"( ({Value1.GetCode()}) "+Operator+ $" ({Value2.GetCode()}) )";
        }

        public override Dictionary<string, string> GetVariables()
        {
            return new();
        }
    }
}
