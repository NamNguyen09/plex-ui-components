using Plex.UIComponents.UIComponents;

namespace cxPlatform.Content.UIComponents;

[Serializable]
public enum CellVerticalAlign
{
    Top = 0,
    Middle = 1,
    Bottom = 2,
    Undefined = -1,
}

public static partial class ExtensionCellAlign
{
    public static AsposeCellVerticalAlign ToAsposeCellVerticalAlign(this CellVerticalAlign cellVerticalAlign)
    {
        switch (cellVerticalAlign)
        {
            case CellVerticalAlign.Bottom:
                return AsposeCellVerticalAlign.Bottom;
            case CellVerticalAlign.Middle:
                return AsposeCellVerticalAlign.Middle;
            case CellVerticalAlign.Top:
                return AsposeCellVerticalAlign.Top;
            default:
                return AsposeCellVerticalAlign.Undefined;
        }
    }
}
