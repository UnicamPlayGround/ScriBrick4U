﻿using Microsoft.Maui.Controls.Shapes;

namespace Frontend.Models.Blocks.Shapes
{
    /// <summary>
    /// Interfaccia che rappresenta la forma di un blocco
    /// </summary>
    public interface IBlockShape
    {
        /// <summary> Stringa NON parametrizzata che rappresenta la forma </summary>
        public string Path { get; set; }

        /// <summary> Stringa parametrizzata che rappresenta la forma </summary>
        public string PathTemplate { get; }

        /// <summary> Oggetto che rappresenta la forma dopo il parsing del <see cref="Path"/> </summary>
        public Geometry SvgData { get; set; }

        /// <summary>   </summary>
        public PointF BlockOffset { get; set; }

        /// <summary> Tipo di forma </summary>
        public ShapeType Type { get; set; }

        /// <summary>
        /// Parsa il <see cref="PathTemplate"/> con larghezza e altezza passata come parametro
        /// </summary>
        /// <param name="width"> larghezza del blocco </param>
        /// <param name="height"> altezza del blocco </param>
        /// <returns> il <see cref="PathTemplate"/> parsato </returns>
        public string GetSvgPath(float width, float height);
    }
}