using Backend.Blocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Transpilers
{
    public class Transpiler : ITranspiler
    {
        public string ConvertToCode(string className, IQueryable<IBlock> blocks)
        {
            string code = "";
            code += DefineBaseImport();
            code += DefineClassStart(className);
            Dictionary<string, string> variables = new();
            foreach (IBlock block in blocks)
            {
                foreach(var children in block.Children)
                {
                    foreach (var variable in children.GetVariables().ToList())
                    {
                        if (variables.ContainsKey(variable.Key)) continue;
                        variables.Add(variable.Key, variable.Value);
                    }
                }
            }
            code += DefineGlobalVariables(variables);
            foreach (IBlock block in blocks)
            {
                code += block.GetCode();
            }
            code += "}";
            return code;
        }

        private static string DefineGlobalVariables(Dictionary<string, string> variablesTable)
        {
            string code = "";
            foreach (KeyValuePair<string, string> variable in variablesTable)
            {
                code += $"public {variable.Value} {variable.Key};\n";
            }
            return code;
        }

        private static string DefineClassStart(string name)
        {
            return $"public class {name} : MonoBehaviour {{ \n";
        }

        private static string DefineBaseImport()
        {
            return "using UnityEngine; \n";
        }
    }
}