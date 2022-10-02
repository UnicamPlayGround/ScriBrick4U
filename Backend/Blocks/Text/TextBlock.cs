using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Blocks.Text
{
    /// <summary>
    /// Definisce il blocco per l'inserimento di un testo
    /// </summary>
    public class TextBlock : AbstractBlock
    {
        public string Tag { get; }
        public string Text { get; }
        public TextBlock(string name, string tag, string text) : base(name)
        {
            Tag = tag;
            Text = text;
        }

        public override string GetCode()
        {
            var code = $"var {Name} = GameObject.FindGameObjectWithTag(\"{Tag}\");\n";
            code += $"{Name}.GetComponent<Text>().text = {Text};\n";
            return code;
        }
    }
}
