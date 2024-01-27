namespace Plex.UIComponents.Dtos;

[Serializable]
public class DF_ODto
{
    public int DF_O_ID { get; set; }
    public int OwnerID { get; set; }
    public int DocumentFormatID { get; set; }
    public virtual ICollection<DocumentItemDto> DocumentItems { get; set; } = [];
}
