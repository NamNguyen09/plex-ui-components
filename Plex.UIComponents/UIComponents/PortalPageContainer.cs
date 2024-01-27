namespace Plex.UIComponents.UIComponents;


[Serializable]
public class PortalPageContainer : BaseUIComponent
{
    public int PortalPageID { get; set; }
    public int OwnerID { get; set; }
    public short No { get; set; }
    public string? Name { get; set; }
    public string? Parameter { get; set; }
    public bool RenderSelectionHeader { get; set; }
    public string? CssClass { get; set; }
    public string? ExportFileNameParameter { get; set; }
    public string? Description { get; set; }

    public List<PortalPartContainer> PortalPartContainers { get; set; } = [];
    public ToolBar? ToolBar { get; set; }
}
