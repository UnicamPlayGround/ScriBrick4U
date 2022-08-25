namespace Frontend.Helpers.Mediators
{
    /// <summary>
    /// Enum che descrive le azioni eseguibili da un <see cref="IMediator"/>
    /// </summary>
    public enum MediatorKey
    {
        /// <summary>
        /// 
        /// </summary>
        UPDATEBLOCKSBYTYPE,

        /// <summary>
        /// 
        /// </summary>
        GETDROPPEDBLOCKS,
        
        /// <summary>
        /// 
        /// </summary>
        GETJSONDROPPEDBLOCKS,
        
        /// <summary>
        /// 
        /// </summary>
        SETDROPPEDBLOCKSFROMJSON
    }
}