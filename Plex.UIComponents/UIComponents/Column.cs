
using System;

namespace Plex.UIComponents.UIComponents
{
    /// <summary>
    /// Class Column.
    /// </summary>
    [Serializable]
    public class Column : BaseUIComponent
    {
        /// <summary>
        /// The width
        /// </summary>
        public int Width;
        /// <summary>
        /// The name
        /// </summary>
        public string Name = "";
        /// <summary>
        /// The CSS class
        /// </summary>
        public string CssClass = "";

        /// <summary>
        /// The Unit px or %
        /// </summary>
        public string Unit;

        /// <summary>
        /// Initializes a new instance of the <see cref="Column"/> class.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="name">The name.</param>
        /// <param name="cssClass">The CSS class.</param>
        /// <param name="unit">The unit</param>
        public Column(int width = 0, string name = "", string cssClass = "", string unit = "px")
        {
            Name = name;
            Width = width;
            CssClass = cssClass;
            Unit = unit;
        }
    }
}
