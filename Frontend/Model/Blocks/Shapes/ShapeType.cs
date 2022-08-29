namespace Frontend.Models.Blocks.Shapes
{
    /// <summary>
    /// Enum che descrive la forma di un blocco
    /// </summary>
    public enum ShapeType
    {
        /// <summary> Forma per blocco principale </summary>
        UPPER,

        /// <summary> Forma per blocco rettangolo </summary>
        RECTANGLE,

        /// <summary> Forma per blocco con figli </summary>
        WITH_CHILDREN,
    }

    /// <summary>
    /// Classe che rappresenta una serie di operazioni eseguibili sulle forme di un blocco
    /// </summary>
    public static class ShapeTypeMethods
    {
        /// <summary>
        /// Dizionario contenente tutte le forme di blocco con associata la rispettiva istanza di <see cref="IBlockShape"/>
        /// </summary>
        private static readonly Dictionary<ShapeType, IBlockShape> _dictionary = new() {
            { ShapeType.UPPER, new DefaultShape("m 0,4 A 4,4 0 0,1 4,0 H 50 c 2,0 3,1 4,2 v 15 H %WIDTH% a 4,4 0 0,1 4,4 v %HEIGHT% a 4,4 0 0,1 -4,4 H 48   c -2,0 -3,1 -4,2 l -4,4 c -1,1 -2,2 -4,2 h -12 c -2,0 -3,-1 -4,-2 l -4,-4 c -1,-1 -2,-2 -4,-2 H 4 a 4,4 0 0,1 -4,-4 z", ShapeType.UPPER, new(0, 74), 139, 40) },
            { ShapeType.RECTANGLE, new DefaultShape("m 0,4 A 4,4 0 0,1 4,0 H 12 c 2,0 3,1 4,2 l 4,4 c 1,1 2,2 4,2 h 12 c 2,0 3,-1 4,-2 l 4,-4 c 1,-1 2,-2 4,-2 H %WIDTH% a 4,4 0 0,1 4,4 v 32 a 4,4 0 0,1 -4,4 H 48 c -2,0 -3,1 -4,2 l -4,4 c -1,1 -2,2 -4,2 h -12 c -2,0 -3,-1 -4,-2 l -4,-4 c -1,-1 -2,-2 -4,-2 H 4 a 4,4 0 0,1 -4,-4 z", ShapeType.RECTANGLE, new(0, 39), 130, 35) },
            { ShapeType.WITH_CHILDREN, new DefaultShape("m 0,4 A 4,4 0 0,1 4,0 H 12 c 2,0 3,1 4,2 l 4,4 c 1,1 2,2 4,2 h 12 c 2,0 3,-1 4,-2 l 4,-4 c 1,-1 2,-2 4,-2 H %WIDTH% a 4,4 0 0,1 4,4 v 32 a 4,4 0 0,1 -4,4 H 64 c -2,0 -3,1 -4,2 l -4,4 c -1,1 -2,2 -4,2 h -12 c -2,0 -3,-1 -4,-2 l -4,-4 c -1,-1 -2,-2 -4,-2 h -8 a 4,4 0 0,0 -4,4 v %HEIGHT% a 4,4 0 0,0 4,4 h 8 c 2,0 3,1 4,2 l 4,4 c 1,1 2,2 4,2 h 12 c 2,0 3,-1 4,-2 l 4,-4 c 1,-1 2,-2 4,-2 H 156 a 4,4 0 0,1 4,4 v 24  a 4,4 0 0,1 -4,4 H 48 c -2,0 -3,1 -4,2 l -4,4 c -1,1 -2,2 -4,2 h -12 c -2,0 -3,-1 -4,-2 l -4,-4 c -1,-1 -2,-2 -4,-2 H 4 a 4,4 0 0,1 -4,-4 z", ShapeType.WITH_CHILDREN, new(0, 112), 158, 35) },
        };

        /// <summary>
        /// Restituisce l'<see cref="IBlockShape"/> associata al tipo di forma passato come parametro
        /// </summary>
        /// <param name="type"> Tipo di forma del quale estrarre l'<see cref="IBlockShape"/> </param>
        /// <returns> l'<see cref="IBlockShape"/> associata al tipo di forma passato come parametro </returns>
        public static IBlockShape GetShape(ShapeType type) {
            return _dictionary[type];
        }
    }
}
