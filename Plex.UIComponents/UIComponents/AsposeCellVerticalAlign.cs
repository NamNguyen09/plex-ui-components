namespace Plex.UIComponents.UIComponents;

/// <summary>
/// Virtical aligment enum.
/// Values isn't continous because its values need to be mapped to values in Aspose.Cells.TextAlignmentType.
/// </summary>
[Serializable]
public enum AsposeCellVerticalAlign
{
    /// <summary>
    /// The bottom
    /// </summary>
    Bottom = 0,
    /// <summary>
    /// The middle
    /// </summary>
    Middle = 1, /* Equal to virtical center alignment */
    /// <summary>
    /// The distributed
    /// </summary>
    Distributed = 3,
    /// <summary>
    /// The justify
    /// </summary>
    Justify = 6,
    /// <summary>
    /// The top
    /// </summary>
    Top = 9,
    /// <summary>
    /// The undefined
    /// </summary>
    Undefined = -1,
}
