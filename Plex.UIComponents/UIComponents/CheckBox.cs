using System;

namespace Plex.UIComponents.UIComponents
{
    /// <summary>
    /// Class checkbox
    /// </summary>
    /// <seealso cref="BaseUIComponent" />
    [Serializable]
    public class CheckBox : BaseUIComponent
    {
        /// <summary>
        /// Gets or sets the name of checkbox.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [default value is false].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [default value]; otherwise, <c>false</c>.
        /// </value>
        public bool DefaultValue { get; set; }
        /// <summary>
        /// Gets or sets the size of checkbox.
        /// </summary>
        /// <value>
        /// The size.
        /// </value>
        public int Size { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CheckBox"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="defaultValue">if set to <c>true</c> [default value].</param>
        /// <param name="size">The size.</param>
        public CheckBox(bool defaultValue, string name = "", int size = 0)
        {
            Name = name;
            DefaultValue = defaultValue;
            Size = size;
        }
    }
}
