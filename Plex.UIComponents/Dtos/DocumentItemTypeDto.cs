namespace Plex.UIComponents.Dtos;

public class DocumentItemTypeDto
{
    public int DocumentItemTypeID { get; set; }
    public string? Name { get; set; }
    public DateTime Created { get; set; }
    public ICollection<DocumentItemDto> DocumentItems { get; set; } = [];
}
