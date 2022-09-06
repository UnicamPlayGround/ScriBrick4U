using System.Text.RegularExpressions;

namespace Frontend.Models.QuestionItem
{
    /// <summary>
    /// Enum che descrive il tipo di valore atteso da un <see cref="IBlockEditItem"/>
    /// </summary>
    public enum TypeValue
    {
        /// <summary> E' atteso un numero </summary>
        NUMBER,

        /// <summary> E' attesa una stringa </summary>
        STRING,
        /// <summary>
        /// E' atteso un nome di variabile
        /// </summary>
        VARIABLE
    }

    /// <summary>
    /// Classe che rappresenta una serie di operazioni eseguibili sui tipi di valori attesi da un <see cref="IBlockEditItem"/>
    /// </summary>
    public static class TypeValueMethod
    {
        /// <summary>
        /// Restituisce la funzione di validazione associata al valore atteso da un <see cref="IBlockEditItem"/> 
        /// </summary>
        /// <param name="type"> Tipo di valore atteso </param>
        /// <returns> la funzione di validazione associata ad un tipo di valore atteso </returns>
        public static Func<string, bool> GetValidator(TypeValue type)
        {
            Func<string, bool> validator;
            switch (type)
            {
                case TypeValue.NUMBER:
                    validator = (v) => { return int.TryParse(v, out int r); };
                    break;
                case TypeValue.STRING:
                    validator = (v) => { return !string.IsNullOrEmpty(v); };
                    break;
                case TypeValue.VARIABLE:
                    //regex per validare il nome della variabile:
                    Regex rgx = new("^[a-zA-Z_][a-zA-Z_$0-9]");
                    validator = (v) => { return rgx.IsMatch(v); };
                    break;
                default:
                    validator = (v) => true;
                    break;
            }
            return validator;
        }
    }
}