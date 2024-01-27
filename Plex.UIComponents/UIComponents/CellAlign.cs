using Plex.UIComponents.UIComponents;

namespace cxPlatform.Content.UIComponents;


[Serializable]
public enum CellAlign
{
    Left = 0,
    Center = 1,
    Right = 2,
    Undefined = -1,
}

public static partial class ExtensionCellAlign
{
    public static AsposeCellAlign ToAsposeCellAlign(this CellAlign cellAlign)
    {
        switch (cellAlign)
        {
            case CellAlign.Center:
                return AsposeCellAlign.Center;
            case CellAlign.Left:
                return AsposeCellAlign.Left;
            case CellAlign.Right:
                return AsposeCellAlign.Right;
            default:
                return AsposeCellAlign.Undefined;
        }
    }
}
