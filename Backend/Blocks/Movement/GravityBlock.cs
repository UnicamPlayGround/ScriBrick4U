using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Blocks.Movement
{
    public class GravityBlock : AbstractBlock
    {
        /// <summary>
        /// Blocco contente il valore
        /// </summary>
        public string Value { get; set; }
        public GravityBlock(string name, string value) : base(name)
        {
            Value = value;
        }

        public override string GetCode()
        {
            string code = "";
            code += $"Vector3 {Name} = new Vector3({Value}, 0, 0);\n";
            code += "if(gameObject.GetComponent<Rigidbody2D>() == null){\n";
            code += "movementController = gameObject.AddComponent<Rigidbody2D>();\n";
            code += "}\n";
            code += $"movementController.gravityScale = {Value}f;\n";
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

