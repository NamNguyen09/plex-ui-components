namespace Plex.UIComponents.UIComponents;

public class HighChart : BaseUIComponent
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public int No { get; set; }
    public bool Stacked { get; set; }
    public bool Doughnut { get; set; }
    public string? Title { get; set; }
    public string? Type { get; set; }
    public bool Polar { get; set; }
    public int NumberOfPoints { get; set; }
    public List<HighChartSeriesItem> SeriesItems { get; set; } = [];
    public bool IsStandardReport { get; set; }
    public string? Categories { get; set; }
    public List<KeyValuePair<string, List<double>>> Datas { get; set; } = [];
}

public class HighChartSeriesItem
{
    public string? Name { get; set; }
    public string? Type { get; set; }
    public string? Data { get; set; }
    public string? Color { get; set; }
    public string? BorderColor { get; set; }
}

/// <summary>
/// Highcharts type 
/// </summary>
public class HighChartTypes
{
    /// <summary>
    /// Horizontal
    /// </summary>
    public const string Horizontal = "bar";
    /// <summary>
    /// Vertical
    /// </summary>
    public const string Vertical = "column";
    /// <summary>
    /// Solar
    /// </summary>
    public const string Pie = "pie";
}
