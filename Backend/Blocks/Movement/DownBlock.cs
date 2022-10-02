﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Blocks.Movement
{
    /// <summary>
    /// Definisce il blocco per il movimento in basso
    /// </summary>
    public class DownBlock : AbstractBlock
    {
        /// <summary>
        /// Blocco contente il valore
        /// </summary>
        public string Value { get; set; }
        public DownBlock(string name, string value) : base(name)
        {
            Value = value;
        }

        public override string GetCode()
        {
            string code = "";
            code += $"Vector2 {Name} = new Vector2(0, -1);\n";
            code += "if(gameObject.GetComponent<Rigidbody2D>() == null){\n";
            code += "movementController = gameObject.AddComponent<Rigidbody2D>();\n";
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
