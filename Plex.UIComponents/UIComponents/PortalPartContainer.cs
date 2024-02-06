using System.Collections;

namespace Plex.UIComponents.UIComponents;

[Serializable]
public abstract class PortalPartContainer : BaseUIComponent
{
    public bool IsRender { get; set; } = true;
    public string? AfterContentText { get; set; }
    public string? BeforeContentText { get; set; }
    public string? FooterText { get; set; }
    public string? HeaderText { get; set; }
    public int PortalPartId { get; set; }
    public int PortalPartTypeID { get; set; } = 1;
    public bool RenderName { get; set; }
    public string? TextAbove { get; set; }
    public string? TextBelow { get; set; }
    public string? Text { get; set; }
    public bool RenderHeaderDiv { get; set; }
    public bool RenderCoverContentDiv { get; set; }
    public string? Name { get; set; }
    public string? InlineStyle { get; set; }
    public string? CssClass { get; set; }
    public string? ComponentName { get; set; }
    public string? DataSource { get; set; }
    public List<int> SubPortalPageIDs { get; set; } = [];
    public List<object> Items { get; set; } = [];
    public abstract string? PortalPartType { get; }


    ////public PortalPartContainer(PortalPart portalPart, IWorkContext workContext, IApplicationService applicationService)
    ////{
    ////    if (portalPart != null)
    ////    {
    ////        var languageId = workContext.CurrentLanguageId;
    ////        PortalPartId = portalPart.PortalPartID;
    ////        PortalPartType = portalPart.PortalPartTypeID;
    ////        Name = applicationService.GetPortalPartName(portalPart, languageId);
    ////        TextAbove = applicationService.GetPortalPartTextAbove(portalPart, languageId);
    ////        TextBelow = applicationService.GetPortalPartTextBelow(portalPart, languageId);
    ////        HeaderText = applicationService.GetPortalPartHeaderText(portalPart, languageId);
    ////        FooterText = applicationService.GetPortalPartFooterText(portalPart, languageId);
    ////        BeforeContentText = applicationService.GetPortalPartBeforeContentText(portalPart, languageId);
    ////        AfterContentText = applicationService.GetPortalPartAfterContentText(portalPart, languageId);
    ////        if (!string.IsNullOrEmpty(portalPart.CssClass))
    ////        {
    ////            CssClass = portalPart.CssClass;
    ////        }
    ////        if (!string.IsNullOrEmpty(portalPart.InlineStyle))
    ////        {
    ////            InlineStyle = portalPart.InlineStyle;
    ////        }
    ////        IsRender = true;
    ////    }
    ////}

    ////public void ParseExpressions(IExpressionParser expressionParser, CurrentUserDto currentUserDto)
    ////{
    ////    TextAbove = ParseText(TextAbove, expressionParser, currentUserDto);
    ////    TextBelow = ParseText(TextBelow, expressionParser, currentUserDto);
    ////    HeaderText = ParseText(HeaderText, expressionParser, currentUserDto);
    ////    FooterText = ParseText(FooterText, expressionParser, currentUserDto);
    ////    BeforeContentText = ParseText(BeforeContentText, expressionParser, currentUserDto);
    ////    AfterContentText = ParseText(AfterContentText, expressionParser, currentUserDto);
    ////    Name = ParseText(Name, expressionParser, currentUserDto);
    ////    Text = ParseText(Text, expressionParser, currentUserDto);
    ////}

    ////private string ParseText(string text, IExpressionParser expressionParser, CurrentUserDto currentUserDto)
    ////{
    ////    if (!string.IsNullOrWhiteSpace(text) && text.Contains("{") && text.Contains("}"))
    ////    {
    ////        return expressionParser.ParseExpressionString(text, currentUserDto, string.Empty);
    ////    }

    ////    return text;
    ////}

    public int FindMaxTableolumnsInItems()
    {
        var maxTableColumn = 0;
        foreach (var item in Items)
        {
            if (ReferenceEquals(item.GetType(), typeof(Table)))
            {
                var count = ((Table)item).Columns.Count;
                if (count > maxTableColumn)
                {
                    maxTableColumn = count;
                }
            }
        }
        return maxTableColumn;
    }
}