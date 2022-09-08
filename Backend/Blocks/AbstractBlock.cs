namespace Backend.Blocks;

public abstract class AbstractBlock : IBlock
{
    protected AbstractBlock(
        string name
        )
    {
        Name = name;
        Children = new List<IBlock>();
    }
    public string Name { get; set; }
    public IEnumerable<IBlock> Children { get; set; } = Enumerable.Empty<IBlock>();
    public abstract string GetCode();
    public virtual Dictionary<string, string> GetVariables()
    {
        return new();
    }
}