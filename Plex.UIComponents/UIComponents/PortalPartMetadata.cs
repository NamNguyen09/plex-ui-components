using System.Composition;

namespace Plex.UIComponents.UIComponents;

[MetadataAttribute]
public class PortalPartMetadata(int type) : Attribute
{
    public int Type { get; set; } = type;
}
