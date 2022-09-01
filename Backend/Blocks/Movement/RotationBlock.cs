using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Blocks.Movement
{
    /// <summary>
    /// Definisce il blocco per la rotazione
    /// </summary>
    public class RotationBlock : AbstractBlock
    {
        /// <summary>
        /// Blocco contente il valore
        /// </summary>
        private IBlock Value { set; get; }
        public RotationBlock(string name, IBlock value) : base(name)
        {
            Value = value;
        }

        public override string GetCode()
        {
            return $"transform.Rotate(new Vector3(0, 0, transform.rotation.z + {Value.GetCode()}));\n";
        }

        public override Dictionary<string, string> GetVariables()
        {
            return new();
        }
    }
}
