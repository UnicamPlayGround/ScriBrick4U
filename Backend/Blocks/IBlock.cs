namespace Backend.Blocks
{
    ///
    /// <summary>
    ///     Interfaccia che definisce il comportamento di un generico blocco
    /// </summary>
    public interface IBlock
    {
        /// <summary>
        ///     Tipologia blocco
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        ///     Nome blocco
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        ///     Lista contenente blocchi figli
        /// </summary>
        public List<IBlock>? Children { get; set; }
        /// <summary>
        ///     Blocco successivo da eseguire. <c>Null</c> se non è presente un blocco successivo.
        /// </summary>
        public IBlock? Next { get; set; }
        /// <summary>
        ///     Blocco precedente. <c>Null</c> se non è presente un blocco precedente
        /// </summary>
        public IBlock? Prev { get; set; }
    }
}
