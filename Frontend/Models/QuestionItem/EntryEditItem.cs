namespace Frontend.Models.QuestionItem
{
    /// <summary>
    /// Classe concreta che permette di editare un valore di un blocco attraverso una casella di testo
    /// </summary>
    public class EntryEditItem : EditItem
    {
        /// <summary> Casella di testo nella quale digitare il valore </summary>
        private readonly Entry _entry;

        /// <summary>
        /// Costruttore di default
        /// </summary>
        /// <param name="questionText"> Domanda da porre quando si chiede il valore </param>
        /// <param name="type"> Tipo di valore atteso </param>
        /// <param name="errorMessage"> Messaggio di errore se il valore digitato/selezionato non e' valido </param>
        public EntryEditItem(string questionText, TypeValue type, string errorMessage) : base(questionText, type, errorMessage)
        {
            Element = _entry = new Entry()
            {
                MaxLength = 30
            };
        }


        public override void SetValue(string value)
        {
            Value = value;
            _entry.Text = value;
        }
        public override bool ValidateResult()
        {
            Value = (Element as Entry).Text;
            return ValidationFunc.Invoke(Value);
        }
    }
}
