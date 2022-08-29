namespace Backend.Blocks;

/// <summary>
/// Interfaccia che definisce un generico blocco
/// </summary>
public interface IBlock
{
    /// <summary>
    /// Tipologia blocco
    /// </summary>
    public string Type { get; set; }
    /// <summary>
    /// Nome del blocco. Il nome viene utilizzato per generare nomi all'interno dello script C# per unity, e quindi deve essere unico
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// Lista di blocchi figli
    /// </summary>
    public IEnumerable<IBlock> Children { get; set; }
    /// <summary>
    /// Metodo per la generazione del codice Unity associato al blocco
    /// </summary>
    /// <returns>Ritorna una <c>string</c> contente il codice Unity associato al blocco</returns>
    public string GetCode();
    /// <summary>
    /// Metodo per la definizione delle proprieta della classe di Unity 
    /// </summary>
    /// <returns>Ritorna un <c>Dictionary &lt;string,string&gt;</c> dove
    ///     <list type="bullet">
    ///         <item>
    ///             <term>Chiave</term>
    ///             <description>Rappresenta il nome della proprieta</description>
    ///         </item>
    ///         <item>
    ///             <term>Valore</term>
    ///             <description>Rappresenta il tipo della proprieta</description>
    ///         </item>
    ///     </list>
    /// </returns>
    public Dictionary<string, string> GetVariables();
}