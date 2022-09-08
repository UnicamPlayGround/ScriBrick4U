using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Blocks.Events
{
    /// <summary>
    /// Blocco per definire un evento da tastiera
    /// </summary>
    public class KeyboardEvent : AbstractBlock
    {
        /// <summary>
        /// Tasto premuto
        /// </summary>
        public string Key { get; set; }
        public KeyboardEvent(string name, string key) : base(name)
        {
            Key = key;
        }

        public override string GetCode()
        {
            string code = "";
            code += $"if(Input.GetKey(\"{Key}\")){{\n";
            foreach(var children in Children)
            {
                code += children.GetCode();
            }
            code += "}\n";
            return code;
        }
    }
}
