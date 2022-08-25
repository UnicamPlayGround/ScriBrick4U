namespace Frontend.Model.QuestionItem
{
    /// <summary>
    /// Interfaccia che rappresenta una coppia <see cref="Label"/> / Element per editare un valore di un blocco
    /// </summary>
    public interface IBlockEditItem
    {
        /// <summary> Valore digitato/selezionato </summary>
        public string Value { get; set; }
        
        /// <summary> Domanda da porre quando si chiede il valore </summary>
        public Label Question { get; set; }

        /// <summary> Elemento attraverso cui editare il blocco </summary>
        public Element Element { get; set; }

        /// <summary> Messaggio di errore se il valore digitato/selezionato non e' valido </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Imposta il <see cref="Value"/> con il valore passato come parametro
        /// </summary>
        /// <param name="value"> nuovo valore </param>
        public void SetValue (string value);

        /// <summary>
        /// Valida il valore
        /// </summary>
        /// <returns> true se il valore e' valido, false altrimenti </returns>
        public bool ValidateResult();
    }
}