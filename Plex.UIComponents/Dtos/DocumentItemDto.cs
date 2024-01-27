namespace Plex.UIComponents.Dtos;

[Serializable]
public class DocumentItemDto
{
    public int DocumentItemID { get; set; }
    public int DocumentItemTypeID { get; set; }
    public int DF_O_ID { get; set; }
    public string? BGColor { get; set; }
    public string? FGColor { get; set; }
    public int FontSize { get; set; }
    public string? FontName { get; set; }
    public int FontIsBold { get; set; }
    public string? BorderColor { get; set; }
    public virtual DF_ODto? DF_O { get; set; }
    public virtual DocumentItemTypeDto? DocumentItemType { get; set; }
}
