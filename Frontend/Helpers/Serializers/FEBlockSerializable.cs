﻿using Frontend.Models.Blocks;
using Frontend.Models.Blocks.Bounds;
using Frontend.Models.QuestionItem;

namespace Frontend.Helpers.Serializers
{
    /// <summary>
    /// Classe che raccoglie i dati essenziali di un blocco
    /// </summary>
    public class FEBlockSerializable
    {
        /// <summary> Stringa, NON parametrizzata, che rappresenta la forma del blocco </summary>
        public string Path { get; set; } = "";

        /// <summary> Lista di tuple formate dalla posizione e valore degli <see cref="IBlockEditItem"/> del blocco </summary>
        public List<Tuple<int, string>> Questions { get; set; } = new();

        /// <summary> Nome del blocco </summary>
        public string DescriptorName { get; set; } = "";

        /// <summary> Tipo del blocco </summary>
        public BlockType DescriptorType { get; set; }
        /// <summary>
        /// Categoria del blocco
        /// </summary>
        public BlockCategory DescriptorCategory { get; set; }

        /// <summary> Dimensioni e coordinate di un blocco </summary>
        public BlockBound Position { get; set; } = null!;

        /// <summary> Tipo del blocco </summary>
        public string? BlockType { get; set; }

        /// <summary>
        /// Costruttore di default
        /// </summary>
        public FEBlockSerializable()
        {
        }

        /// <summary>
        /// Costruttore che accetta un blocco del quale raccogliere i dati essenziali
        /// </summary>
        public FEBlockSerializable(IFrontEndBlock block) {
            Path = block.Shape.Path;
            DescriptorName = block.Descriptor.Name;
            DescriptorType = block.Descriptor.Type;
            DescriptorCategory = block.Descriptor.Category;
            Position = new(block.Position.Width, block.Position.Height, block.Position.UpperLeft);
            BlockType = block.GetType().FullName;
            Questions = new();
            foreach (var ele in block.Questions)
                Questions.Add(new(block.Questions.IndexOf(ele), ele.Value));
        }
    }
}
