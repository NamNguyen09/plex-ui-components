namespace Plex.UIComponents.UIComponents;


[Serializable]
public class ReportPageContainer
{
    public int PortalPageId { get; set; }
    public int OwnerId { get; set; }
    public short No { get; set; }
    public string? Name { get; set; }
    public bool RenderSelectionHeader { get; set; }
    public string? CssClass { get; set; }
    public List<PortalPartContainer> PortalPartContainers { get; set; } = [];
    public ToolBar? ToolBar { get; set; }
}
