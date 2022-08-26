namespace Backend.Blocks;

public abstract class AbstractBlock : IBlock
{
    protected AbstractBlock(
        string type,
        string name
        )
    {
        Type = type;
        Name = name;
        Children = new List<IBlock>();
    }

    public string Type { get; set; }
    public string Name { get; set; }
    public IEnumerable<IBlock> Children { get; set; } = Enumerable.Empty<IBlock>();
    public abstract string GetCode();
    public abstract Dictionary<string, string> GetVariables();
}