namespace Frontend.Models.QuestionItem;

/// <summary>
/// Classe astratta, di default, che rappresenta una coppia <see cref="Label"/> / Element per editare un valore di un blocco
/// </summary>
public abstract class EditItem : IBlockEditItem
{
    public Label Question { get; set; }
    public Element? Element { get; set; }
    public string ErrorMessage { get; set; }

    /// <summary> Variabile privata che rappresenta il valore digitato/selezionato </summary>
    private string _value = "";
    public string Value
    {
        get => _value;
        set
        {
            _value = value;

        }
    }

    /// <summary> Tipo di valore atteso </summary>
    public TypeValue Type { get; }

    /// <summary> Funzione per indicare se il valore digitato/selezionato e' valido oppure no </summary>
    public Func<string, bool> ValidationFunc { get; }

    /// <summary>
    /// Costruttore di default
    /// </summary>
    /// <param name="questionText"> Domanda da porre quando si chiede il valore </param>
    /// <param name="type"> Tipo di valore atteso </param>
    /// <param name="errorMessage"> Messaggio di errore se il valore digitato/selezionato non e' valido </param>
    public EditItem(string questionText, TypeValue type, string errorMessage)
    {
        Question = new()
        {
            Text = questionText
        };
        ValidationFunc = TypeValueMethod.GetValidator(type);
        ErrorMessage = errorMessage;
    }

    public abstract bool ValidateResult();
    public abstract void SetValue(string value);
    public override string ToString()
    {
        return Value;
    }
}