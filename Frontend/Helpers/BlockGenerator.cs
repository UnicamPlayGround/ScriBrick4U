using Frontend.Blocks;
using Frontend.Builders;
namespace ProvaMauiDragAndDrop.Helper
{
    /// <summary>
    /// Classe che rappresenta un generatore di blocchi
    /// </summary>
    public static class BlockGenerator
    {
        /// <summary>
        /// Metodo che permette di costruire un blocco a partire dal nome
        /// </summary>
        /// <param name="name"> nomme del blocco </param>
        /// <returns> blocco di tipo <see cref="IFrontEndBlock"/> </returns>
        /// <exception cref="Exception"></exception>
        public static IFrontEndBlock GetBlock(string name)
        {
            switch (name)
            {
                case "move":
                    return new BlockBuilder<LabelEditor>(name, "movement")
                        .AddLabel("muovi di")
                        .AddInput()
                        .AddLabel("passi")
                        .Build();
                case "rotate":
                    return new BlockBuilder<LabelEditor>(name, "movement")
                        .AddLabel("routa di ")
                        .AddInput()
                        .AddLabel("gradi")
                        .Build();
                default:
                    throw new Exception("Blocco non implementato");
            }
        }
    }
}
