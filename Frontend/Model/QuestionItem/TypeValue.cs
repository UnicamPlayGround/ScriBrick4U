namespace Frontend.Model.QuestionItem
{
    /// <summary>
    /// Enum che descrive il tipo di valore atteso da un <see cref="IBlockEditItem"/>
    /// </summary>
    public enum TypeValue
    {
        /// <summary> E' atteso un numero </summary>
        NUMBER,

        /// <summary> E' attesa una stringa </summary>
        STRING
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
                default:
                    validator = (v) => true;
                    break;
            }
            return validator;
        }
    }
}