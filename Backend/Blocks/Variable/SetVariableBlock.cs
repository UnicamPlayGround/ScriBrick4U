using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Blocks.Variable
{
    public class SetVariableBlock : AbstractBlock
    {
        public IBlock Value { get; set; }
        public string NameVariable { get; set; }
        public SetVariableBlock(string name, string nameVariable, IBlock value) : base(name)
        {
            Value = value;
            NameVariable = nameVariable;
        }

        public override string GetCode()
        {
            return $"{NameVariable} = {Value.GetCode()}";
        }

        public override Dictionary<string, string> GetVariables()
        {
            throw new NotImplementedException();
        }
    }
}
