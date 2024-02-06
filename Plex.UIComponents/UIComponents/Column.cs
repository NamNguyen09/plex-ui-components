namespace Plex.UIComponents.UIComponents;

/// </summary>
[Serializable]
public class Column : BaseUIComponent
{
    public Column(int width = 0, string name = "", string cssClass = "", string unit = "px")
    {
        Name = name;
        Width = width;
        CssClass = cssClass;
        Unit = unit;
    }
    public int Width { get; set; }
    public string Name { get; set; } = "";
    public string CssClass { get; set; } = "";
    public string? Unit { get; set; }
}
