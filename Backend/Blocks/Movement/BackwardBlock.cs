using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Blocks.Movement
{
    /// <summary>
    /// Definisce il blocco per il movimento indietro
    /// </summary>
    public class BackwardBlock : AbstractBlock
    {
        /// <summary>
        /// Blocco contente il valore
        /// </summary>
        public string Value { get; set; }
        public BackwardBlock(string name, string value) : base(name)
        {
            Value = value;
        }

        public override string GetCode()
        {
            string code = "";
            code += $"Vector3 {Name} = new Vector3(-1, 0, 0);\n";
            code += "if(gameObject.GetComponent<CharacterController>() == null){\n";
            code += "movementController = gameObject.AddComponent<CharacterController>();\n";
            code += "}\n";
            code += $"movementController.Move( 0.01f * {Value} * {Name});\n";
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
