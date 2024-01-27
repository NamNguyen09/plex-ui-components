namespace Plex.UIComponents.UIComponents;

/// <summary>
/// Class image with Hyperlink
/// </summary>
[Serializable]
public class ImageHyperlink : BaseUIComponent
{
    public string FileName { get; set; }
    public double Width { get; set; }
    public override int Height { get; }
    public string Href { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ImageHyperlink"/> class.
    /// </summary>
    /// <param name="fileName">Name of the file.</param>
    /// <param name="href">The href.</param>
    /// <param name="width">The width.</param>
    /// <param name="height">The height.</param>
    public ImageHyperlink(string fileName, string href, double width = 0, int height = 0)
    {
        FileName = fileName;
        Href = href;
        Width = width;
        Height = height;
    }
}
