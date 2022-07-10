namespace Backend.Sprites
{
    public class Sprite2D : ISprite
    {
        public string Name { get; set; }

        public Sprite2D(string name)
        {
            Name = name;
        }
    }
}
