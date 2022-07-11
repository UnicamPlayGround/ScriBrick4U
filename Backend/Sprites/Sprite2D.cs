namespace Backend.Sprites
{
    /// <summary>
    ///     Implementazione interfaccia <c>ISprite</c> per uno sprite 2D
    /// </summary>
    public class Sprite2D : ISprite
    {
        public string Name { get; set; }

        public Sprite2D(string name)
        {
            Name = name;
        }
    }
}
