namespace Plex.UIComponents.UIComponents;

[Serializable]
public class ToolBar
{
    public ToolBar()
    {
        EnableReportType = true;
        VisibleCategoriesToolbar = false;
        VisiblePartsToolbar = false;
        RenderExportDetails = true;
        ExportDetailsChecked = false;
    }

    public bool RenderToolbar { get; set; }
    /// <summary>
    /// Gets or sets the menu.
    /// </summary>
    /// <value>The menu.</value>
    //public MenuItem Menu { get; set; }
    /// <summary>
    /// Gets or sets the left menu.
    /// </summary>
    /// <value>The left menu.</value>
    //public MenuItem LeftMenu { get; set; }
    /// <summary>
    /// Gets or sets the tab.
    /// </summary>
    /// <value>The tab.</value>
    //public Tab Tab { get; set; }
    /// <summary>
    /// The export type
    /// </summary>
    public string[]? ExportType { get; set; }
    public string? ExportTypeNames { get; set; }
    public List<ExportFormat>? ExportFormats { get; set; }
    public int DefaultIndex { get; set; }
    public bool First { get; set; }
    public bool EnableReportType { get; set; }
    public bool VisibleCategoriesToolbar { get; set; }
    public bool VisiblePartsToolbar { get; set; }
    public string? ViewMode { get; set; }
    public bool RenderExportDetails { get; set; }
    public bool ExportDetailsChecked { get; set; }
    public bool IsWorkflowPortalPage { get; set; }
    public int WorkflowActivityId { get; set; }
}