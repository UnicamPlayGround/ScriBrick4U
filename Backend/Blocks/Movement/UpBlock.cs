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
            code += $"Vector2 {Name} = new Vector2(0, 1);\n";
            code += "if(gameObject.GetComponent<Rigidbody2D>() == null){\n";
            code += "movementController = gameObject.AddComponent<Rigidbody2D>();\n";
            code += "movementController.gravityScale = 0;\n";
            code += "}\n";
            code += $"movementController.MovePosition(movementController.position + 0.01f * {Value} * {Name});\n";
            return code;
        }
        public override Dictionary<string, string> GetVariables()
        {
            Dictionary<string, string> variables = new()
            {
                { "movementController", "Rigidbody2D" }
            };
            return variables;
        }
    }
}
