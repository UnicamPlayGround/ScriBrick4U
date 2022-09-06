using Backend.Blocks;
using System.Numerics;

namespace Backend.Blocks.Movement
{
    /// <summary>
    /// Definisce il blocco per il movimento
    /// </summary>
    public class ForwardBlock : AbstractBlock
    {
        /// <summary>
        /// Blocco contente il valore
        /// </summary>
        public string Value { get; set; }
        public ForwardBlock(string name, string value) : base(name)
        {
            Value = value;
        }

        public override string GetCode()
        {
            string code = "";
            code += $"Vector3 {Name} = new Vector2(1, 0);\n";
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
