using Frontend.Builders;
using Frontend.Model.Blocks;
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
                    return new BlockBuilder<LabelEditor>(name, BlockType.Movimento)
                        .AddLabel("muovi di")
                        .AddInput()
                        .AddLabel("passi")
                        .AddTextDroppedFunction(() => "muovi di TOT passi")
                        .Build();
                case "rotate":
                    return new BlockBuilder<LabelEditor>(name, BlockType.Movimento)
                        .AddLabel("routa di ")
                        .AddInput()
                        .AddLabel("gradi")
                        .AddTextDroppedFunction(() => "ruota di TOT gradi")
                        .Build();
                default:
                    throw new Exception("Blocco non implementato");
            }
        }
    }
}
