namespace Plex.UIComponents.UIComponents;


[Serializable]
public class Hyperlink : BaseUIComponent
{
    public string? DisplayText { get; set; }
    public string? UrlOrBookmark { get; set; }
    public bool IsBookmark { get; set; }
    public Hyperlink(string displayText, string urlOrBookmark, bool isBookmark = false)
    {
        DisplayText = displayText;
        UrlOrBookmark = urlOrBookmark;
        IsBookmark = isBookmark;
    }
}
