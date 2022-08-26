using Backend.Blocks;
using System.Numerics;

namespace Backend.Blocks.Movement
{
    public class ForwardBlock : AbstractBlock
    {
        public IBlock Block { get; set; }
        public ForwardBlock(string name, IBlock block) : base("Movement", name)
        {
            Block = block;
        }

        public override string GetCode()
        {
            string code = "";
            code += $"Vector3 {Name} = new Vector3(1, 0, 0);\n";
            code += "movementController = gameObject.AddComponent<CharacterController>();\n";
            code += $"movementController.Move({Block.GetCode()} * {Name});\n";
            return code;
        }
        public override Dictionary<string, string> GetVariables()
        {
            Dictionary<string, string> variables = new();
            variables.Add("movementController", "CharacterController");
            return variables;
        }
    }
}
