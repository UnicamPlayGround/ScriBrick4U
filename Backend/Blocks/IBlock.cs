namespace Backend.Blocks
{
    public interface IBlock
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public List<IBlock>? Children { get; set; }
        public IBlock? Next { get; set; }
        public IBlock? Prev { get; set; }
    }
}
