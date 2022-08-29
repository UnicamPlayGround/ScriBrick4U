using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Blocks.Starts
{
    /// <summary>
    /// Definisce il blocco per la generazione del metodo <c>Start</c> all'interno dello script Unity
    /// </summary>
    public class StartBlock : AbstractBlock
    {
        public StartBlock() : base("Start", "Start")
        {

        }

        public override string GetCode()
        {
            string code = "";
            code += "private void Start(){\n";
            foreach(var children in Children)
            {
                code += $"{children.GetCode()}";
            }
            code += "}\n";
            return code;
        }

        public override Dictionary<string, string> GetVariables()
        {
            return new();
        }
    }
}
