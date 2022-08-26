namespace Backend.Blocks;

public interface IBlock
{
    public string Type { get; set; }
    public string Name { get; set; }
    public IEnumerable<IBlock> Children { get; set; }
    public string GetCode();
    public Dictionary<string, string> GetVariables();
}