namespace Plex.UIComponents.UIComponents;

/// <summary>
/// Class ToolBar.
/// </summary>
[Serializable]
public class ToolBar
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ToolBar"/> class.
    /// </summary>
    public ToolBar()
    {
        EnableReportType = true;
        VisibleCategoriesToolbar = false;
        VisiblePartsToolbar = false;
        RenderExportDetails = true;
        ExportDetailsChecked = false;
    }

    /// <summary>
    /// Gets or sets a value indicating whether [render toolbar].
    /// </summary>
    /// <value><c>true</c> if [render toolbar]; otherwise, <c>false</c>.</value>
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
    public string[] ExportType;
    /// <summary>
    /// Gets or sets the export type names.
    /// </summary>
    /// <value>The export type names.</value>
    public string ExportTypeNames { get; set; }
    /// <summary>
    /// Gets or sets the export formats.
    /// </summary>
    /// <value>The export formats.</value>
    public List<ExportFormat> ExportFormats { get; set; }
    /// <summary>
    /// Gets or sets the default index.
    /// </summary>
    /// <value>The default index.</value>
    public int DefaultIndex { get; set; }
    /// <summary>
    /// Gets or sets a value indicating whether this <see cref="ToolBar"/> is first.
    /// </summary>
    /// <value><c>true</c> if first; otherwise, <c>false</c>.</value>
    public bool First { get; set; }
    /// <summary>
    /// Gets or sets the tab dto.
    /// </summary>
    /// <value>The tab dto.</value>
    //public TabDto TabDto { get; set; }
    /// <summary>
    /// Gets or sets a value indicating whether [enable report type].
    /// </summary>
    /// <value><c>true</c> if [enable report type]; otherwise, <c>false</c>.</value>
    public bool EnableReportType { get; set; }
    /// <summary>
    /// Gets or sets a value indicating whether [visible categories toolbar].
    /// </summary>
    /// <value><c>true</c> if [visible categories toolbar]; otherwise, <c>false</c>.</value>
    public bool VisibleCategoriesToolbar { get; set; }
    /// <summary>
    /// Gets or sets a value indicating whether [visible parts toolbar].
    /// </summary>
    /// <value><c>true</c> if [visible parts toolbar]; otherwise, <c>false</c>.</value>
    public bool VisiblePartsToolbar { get; set; }
    /// <summary>
    /// Gets or sets the view mode.
    /// </summary>
    /// <value>The view mode.</value>
    public string ViewMode { get; set; }
    /// <summary>
    /// Gets or sets a value indicating whether [render export details].
    /// </summary>
    /// <value><c>true</c> if [render export details]; otherwise, <c>false</c>.</value>
    public bool RenderExportDetails { get; set; }
    /// <summary>
    /// Gets or sets a value indicating whether [export details checked].
    /// </summary>
    /// <value><c>true</c> if [export details checked]; otherwise, <c>false</c>.</value>
    public bool ExportDetailsChecked { get; set; }
    /// <summary>
    /// Gets or sets a value indicating whether [is workflow portal page].
    /// </summary>
    /// <value>
    /// <c>true</c> if [is workflow portal page]; otherwise, <c>false</c>.
    /// </value>
    public bool IsWorkflowPortalPage { get; set; }
    /// <summary>
    /// Gets or sets the workflow activity identifier.
    /// </summary>
    /// <value>
    /// The workflow activity identifier.
    /// </value>
    public int WorkflowActivityId { get; set; }
}