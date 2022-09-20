using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Blocks.Movement
{
    /// <summary>
    /// Definisce il blocco per il movimento in alto
    /// </summary>
    public class UpBlock : AbstractBlock
    {
        /// <summary>
        /// Blocco contente il valore
        /// </summary>
        public string Value { get; set; }
        public UpBlock(string name, string value) : base(name)
        {
            Value = value;
        }

        public override string GetCode()
        {
            string code = "";
            code += $"Vector3 {Name} = new Vector3(0, 1, 0);\n";
            code += "if(gameObject.GetComponent<CharacterController>() == null){\n";
            code += "movementController = gameObject.AddComponent<CharacterController>();\n";
            code += "}\n";
            code += $"movementController.Move({Value} * {Name});\n";
            return code;
        }
        public override Dictionary<string, string> GetVariables()
        {
            Dictionary<string, string> variables = new()
            {
                { "movementController", "CharacterController" }
            };
            return variables;
        }
    }
}
