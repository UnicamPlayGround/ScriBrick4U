namespace Frontend.Models.QuestionItem
{
    /// <summary>
    /// Classe concreta che permette di editare un valore di un blocco attraverso un menu' a tendina
    /// </summary>
    public class PickerEditItem : EditItem
    {
        /// <summary> Menu' a tendina dal quale selezionare il valore </summary>
        private Picker _picker;

        /// <summary>
        /// Costruttore di default
        /// </summary>
        /// <param name="questionText"> Domanda da porre quando si chiede il valore </param>
        /// <param name="type"> Tipo di valore atteso </param>
        /// <param name="errorMessage"> Messaggio di errore se il valore digitato/selezionato non e' valido </param>
        /// <param name="items"> Elementi del menu' a tendina </param>
        public PickerEditItem(string questionText, TypeValue type, string errorMessage, List<string> items) : base(questionText, type, errorMessage)
        {
            Picker p = new();

            foreach (var item in items)
                p.Items.Add(item);

            Element = _picker = p;
        }

        public override void SetValue(string value)
        {
            Value = value;
            _picker.SelectedItem = value;
        }

        public override bool ValidateResult()
        {
            var element = Element as Picker;
            if (element != null)
            {
                Value = (string)element.SelectedItem;
                return ValidationFunc.Invoke(Value);
            }
            return false;
        }
    }
}
