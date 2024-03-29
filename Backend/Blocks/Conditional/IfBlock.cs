﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Blocks.Conditional
{
    /// <summary>
    /// Definisce il blocco per la creazione dell'If
    /// </summary>
    public class IfBlock : AbstractBlock
    {
        /// <summary>
        /// Definisce la parte sinsitra dell'if
        /// </summary>
        public string First { get; set; } = null!;
        /// <summary>
        /// Definisce la parte destra dell'if
        /// </summary>
        public string Second { get; set; } = null!;
        /// <summary>
        /// Definisce l'operatore da applicare 
        /// </summary>
        public string Condition { get; set; }
        public IfBlock(string name, string first, string condition, string second) : base(name)
        {
            First = first;
            Second = second;
            Condition = condition;
        }

        public override string GetCode()
        {
            string code = "";
            code += $"if( ( {First} ) {Condition} ( {Second} ) ){{\n";
            foreach(var child in Children)
            {
                code += child.GetCode();
            }
            code += "}\n";
            return code;
        }
    }
}
