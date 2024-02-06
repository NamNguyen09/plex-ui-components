using System.Text;

namespace Plex.UIComponents.UIComponents;

[Serializable]
public class TextBlock : BaseUIComponent
{
    #region Fields
    public Dictionary<string, string> Attributes { get; set; } = [];
    public string CssClass { get; set; } = string.Empty;
    public string DivId { get; set; } = string.Empty;
    public string HtmlElement { get; set; } = "div";
    public bool IsBold { get; set; }
    public bool IsUnderLine { get; set; }
    public IList<BaseUIComponent> Items { get; set; } = [];
    public string Text { get; set; } = string.Empty;
    //public GenericFormDto GenericForm;
    #endregion

    #region Constructors
    public TextBlock(string text = "", string cssClass = "", string divId = "", string htmlElement = "div")
    {
        Text = text;
        CssClass = cssClass;
        DivId = divId;
        HtmlElement = htmlElement;
    }
    #endregion

    #region Methods
    ////public void ToWord(DocumentBuilder documentBuilder, double pageWidth, ICollection<DocumentItemDto> docItems,
    ////                   int format)
    ////{
    ////    var slPath = Utilities.GetBaseUrl();

    ////    if (CssClass.Split(new[] { ' ', ',' }).ToArray().Contains("pageBreak"))
    ////    {
    ////        documentBuilder.InsertBreak(BreakType.PageBreak);
    ////    }
    ////    if (HtmlElement.Length > 0)
    ////    {
    ////        documentBuilder.InsertHtml("<base href='" + slPath + "'/>" + TextToWordHtml()); //ToHTML())
    ////    }
    ////    else
    ////    {
    ////        documentBuilder.Font.Bold = IsBold;
    ////        documentBuilder.Writeln(Text);
    ////    }
    ////    if (Attributes.ContainsKey("type"))
    ////    {
    ////        if (Attributes["type"].ToLower() == "radio" || Attributes["type"].ToLower() == "checkbox")
    ////        {
    ////            if (format == (short)ExportHandler.ExportFormat.Pdf)
    ////            {
    ////                if (Attributes.ContainsKey("checked"))
    ////                {
    ////                    if (System.Web.HttpContext.Current != null)
    ////                        documentBuilder.InsertImage(
    ////                            System.Web.HttpContext.Current.Server.MapPath("/styles/images") + "/icons/cb_checked.gif", 12, 12);
    ////                }
    ////                else
    ////                {
    ////                    if (System.Web.HttpContext.Current != null)
    ////                        documentBuilder.InsertImage(
    ////                            System.Web.HttpContext.Current.Server.MapPath("/styles/images") + "/icons/cb_unchecked.gif", 12, 12);
    ////                }
    ////            }
    ////            else
    ////            {
    ////                documentBuilder.PushFont();
    ////                documentBuilder.Font.Name = "Wingdings 2";
    ////                documentBuilder.Font.Size = 14;
    ////                documentBuilder.Write(Attributes.ContainsKey("checked") ? "R" : "£");
    ////                documentBuilder.PopFont();
    ////            }
    ////        }
    ////    }

    ////    foreach (var item in Items)
    ////    {
    ////        if (item is Table)
    ////        {
    ////            (item as Table).ToWord(documentBuilder, pageWidth, docItems, format);
    ////            //Tekst
    ////        }
    ////        else if (item is TextBlock)
    ////        {
    ////            (item as TextBlock).ToWord(documentBuilder, pageWidth, docItems, format);
    ////            // Graf
    ////        }
    ////        else if (item is Chart)
    ////        {
    ////            (item as Chart).ToWord(documentBuilder, pageWidth, docItems);
    ////        }
    ////        else if (item is Image)
    ////        {
    ////            (item as Image).ToWord(documentBuilder, pageWidth, docItems);
    ////        }
    ////    }
    ////}
    /// <summary>
    /// Texts to word HTML.
    /// </summary>
    /// <returns>System.String.</returns>
    public string TextToWordHtml()
    {
        if (string.IsNullOrWhiteSpace(Text))
            return string.Empty;
        var strBuilder = new StringBuilder();
        if (HtmlElement.Length > 0)
            strBuilder.Append("<" + HtmlElement + (CssClass.Length > 0 ? " class='" + CssClass + "'" : "") + (DivId.Length > 0 ? " id='" + DivId + "'" : ""));
        strBuilder.Append(">");
        strBuilder.Append(Text);
        strBuilder.Append("</" + HtmlElement + ">");
        return strBuilder.ToString();
    }

    /// <summary>
    /// PPTs the add text.
    /// </summary>
    /// <param name="presentation">The presentation.</param>
    /// <param name="text">The text.</param>
    /// <param name="slide">The slide.</param>
    /// <param name="yPos">The y position.</param>
    /// <param name="pageWidth">Width of the page.</param>
    /// <param name="documentItem">The document item.</param>
    ////public void PptAddText(Presentation presentation, string text, ref Slide slide, ref double yPos, double pageWidth, DocumentItemDto documentItem)
    ////{
    ////    //Options: Show line numbers   Alternate line color  
    ////    if (string.IsNullOrWhiteSpace(text)) return;
    ////    var oTable = slide.Shapes.AddTable(200, (int)yPos, (int)pageWidth, 200, 0, 1, 0.0, System.Drawing.Color.Black);
    ////    PptAddCell(presentation, oTable, 0, 0, text, pageWidth, documentItem, "");
    ////    yPos += oTable.Height + 100;
    ////}


    ////public void ToPpt(Presentation presentation, Aspose.Slides.Table pptTable, int rowIndex, int columnIndex, double width, DocumentItemDto documentItem, string backColor)
    ////{
    ////    if (Attributes.ContainsKey("type"))
    ////    {
    ////        if (Attributes["type"].ToLower() == "radio" || Attributes["type"].ToLower() == "checkbox")
    ////        {
    ////            if (Attributes.ContainsKey("checked"))
    ////            {
    ////                Text = "X";
    ////                PptAddCell(presentation, pptTable, rowIndex, columnIndex, Text, width, documentItem, backColor, TextAlignment.Center);
    ////            }
    ////        }
    ////    }
    ////    else if (!string.IsNullOrEmpty(Text))
    ////    {
    ////        var cell = pptTable.GetCell(columnIndex, rowIndex);
    ////        //var sText = RemoveHTMLTagsPpt(Text);
    ////        var aParagraph = new Aspose.Slides.Paragraph();
    ////        var portion = cell.TextFrame.Paragraphs[0].Portions[0];
    ////        aParagraph.Text = Text;
    ////        aParagraph.Portions[0].FontHeight = portion.FontHeight;
    ////        aParagraph.Portions[0].FontBold = portion.FontBold;
    ////        aParagraph.Portions[0].FontIndex = portion.FontIndex;

    ////        aParagraph.HasBullet = false;
    ////        cell.TextFrame.Paragraphs.Add(aParagraph);
    ////    }
    ////}


    ////public void ToPpt(Presentation presentation, Slide slide, ref double yCurrentPos, double docWidth, ICollection<DocumentItemDto> docItems, double yStartPos)
    ////{
    ////    if (Attributes.ContainsKey("type") && (Attributes["type"].ToLower() == "radio" || Attributes["type"].ToLower() == "checkbox") && Attributes.ContainsKey("checked"))
    ////    {
    ////        Text = "X";
    ////    }

    ////    PptAddText(presentation, TextToWordHtml(), ref slide, ref yCurrentPos, docWidth, FindDocItem(docItems, "Body"));

    ////    foreach (var baseUiComponent in Items)
    ////    {
    ////        if (yCurrentPos > 3000)
    ////        {
    ////            slide = presentation.AddEmptySlide();
    ////            yCurrentPos = yStartPos;
    ////        }

    ////        if (baseUiComponent is Table)
    ////        {
    ////            (baseUiComponent as Table).ToPpt(presentation, ref slide, docWidth, ref yCurrentPos, docItems, yStartPos);
    ////        }
    ////        else if (baseUiComponent is TextBlock)
    ////        {
    ////            var txtBlock = baseUiComponent as TextBlock;
    ////            if (!string.IsNullOrWhiteSpace(txtBlock.CssClass) && txtBlock.CssClass.ToLower().Equals("pagebreak"))
    ////            {
    ////                slide = presentation.AddEmptySlide();
    ////                yCurrentPos = yStartPos;
    ////            }
    ////            else
    ////            {
    ////                txtBlock.ToPpt(presentation, slide, ref yCurrentPos, docWidth, docItems, yStartPos);
    ////            }
    ////        }
    ////        else if (baseUiComponent is Chart)
    ////        {
    ////            (baseUiComponent as Chart).ToPpt(presentation, ref slide, ref yCurrentPos, yStartPos);
    ////        }
    ////        else if (baseUiComponent is CXChart)
    ////        {
    ////            (baseUiComponent as CXChart).ToPpt(presentation, ref slide, ref yCurrentPos, yStartPos);
    ////        }
    ////        else if (baseUiComponent is Image)
    ////        {
    ////            (baseUiComponent as Image).ToPpt(presentation, ref slide, ref yCurrentPos, docWidth, yStartPos);
    ////        }
    ////    }
    ////}

    /// <summary>
    /// Removes the js event.
    /// </summary>
    public override void RemoveJsEvent()
    {
        if (Text.IndexOf("href") > -1)
        {
            const string HRefPattern = "href\\s*=\\s*(?:[\"'](?<1>[^\"']*)[\"']|(?<1>\\S+))";
            Text = System.Text.RegularExpressions.Regex.Replace(Text, HRefPattern, "");
        }

        foreach (var item in Items)
        {
            item.RemoveJsEvent();
        }
    }
    #endregion
}
