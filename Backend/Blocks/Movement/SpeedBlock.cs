using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Blocks.Movement
{
    /// <summary>
    /// Definisce il blocco per la definizione della velocità base di un oggetto
    /// </summary>
    public class SpeedBlock : AbstractBlock
    {
        /// <summary>
        /// Blocco contente il valore
        /// </summary>
        public string Value { get; set; }
        public SpeedBlock(string name, string value) : base(name)
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
            code += $"movementController.velocity = {Name};\n";
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
