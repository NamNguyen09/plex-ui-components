using System.Drawing;
using System.Text;
using Aspose.Words;
using Aspose.Words.Tables;
using Plex.UIComponents.Dtos;
using Comment = Aspose.Words.Comment;
using LineStyle = Aspose.Words.LineStyle;
using Paragraph = Aspose.Words.Paragraph;

namespace Plex.UIComponents.UIComponents;


[Serializable]
public class Cell : BaseUIComponent
{
    public AsposeCellAlign Align = AsposeCellAlign.Undefined;
    public Dictionary<string, string> Attributes = new Dictionary<string, string>();
    public IList<BaseUIComponent> Items = new List<BaseUIComponent>();
    public AsposeCellVerticalAlign VerticalAlign = AsposeCellVerticalAlign.Middle;
    public bool Calculated;
    public bool Navigation;
    public bool RenderAsTableHead;
    public Row? Row { get; set; }
    public bool IsBold { get; set; }
    public int ColIndex { get; set; }
    public int Colspan { get; set; }
    public int Rowspan { get; set; }
    public string CssClass { get; set; }
    public string DocItemName { get; set; }
    public string Text { get; set; }
    public string Title { get; set; }
    public string BackColor { get; set; }
    public string ForeColor { get; set; }
    public string ForeColorClass { get; set; }
    public bool IsCalculated { get; set; }
    public bool IsNavigation { get; set; }
    public string NavigateOnclick { get; set; }
    public bool IsRenderAsTableHead { get; set; }
    public int SortNo { get; set; }
    public bool SortDirection { get; set; }
    public bool Sortable { get; set; }
    public Color BorderButtomColor { get; set; }
    public Color BorderTopColor { get; set; }
    public Color BorderRightColor { get; set; }
    public Color BorderLeftColor { get; set; }
    public Color FontColor { get; set; }
    public double TopPadding { get; set; }
    public double BottomPadding { get; set; }
    public double Padding { get; set; }
    public double LeftPadding { get; set; }
    public double RightPadding { get; set; }
    public LineSpacing LineSpacing { get; set; }

    #region Constructors
    public Cell()
    {
        ColIndex = 0;
        Colspan = 1;
        Rowspan = 1;
        CssClass = "";
        DocItemName = "";
        Text = "";
        Title = "";
        BackColor = "";
        ForeColor = "";
        ForeColorClass = "";
        IsCalculated = false;
        IsNavigation = false;
        NavigateOnclick = "";
        IsRenderAsTableHead = false;
        SortNo = -1;
        SortDirection = false;
        Sortable = false;
        BorderBottom = LineStyle.Single;
        BorderTop = LineStyle.Single;
        BorderLeft = LineStyle.Single;
        BorderRight = LineStyle.Single;
        FontColor = Color.Black;
    }
    public Cell(string text, string title = "", AsposeCellAlign align = AsposeCellAlign.Undefined, string backColor = "",
                string foreColor = "", string cssClass = "", int colspan = 1, int rowspan = 1,
                string docItemName = "", bool bRenderAsTableHead = false, bool bCalculated = false, int sortNo = -1,
        bool sortDirection = false, bool sortable = false, LineStyle borderBottom = LineStyle.Single,
        LineStyle borderTop = LineStyle.Single,
        LineStyle borderLeft = LineStyle.Single, LineStyle bordeRight = LineStyle.Single,
        double topPadding = 0.0, double bottomPadding = 0.0)
    {
        BorderBottom = borderBottom;
        BorderTop = borderTop;
        BorderLeft = borderLeft;
        BorderRight = bordeRight;

        Text = text;
        Title = title;
        Align = align;
        BackColor = backColor;
        ForeColor = foreColor;
        CssClass = cssClass;
        Colspan = colspan;
        Rowspan = rowspan;
        DocItemName = string.IsNullOrEmpty(docItemName) ? "Row" : docItemName;
        IsRenderAsTableHead = bRenderAsTableHead;
        IsCalculated = bCalculated;
        SortNo = sortNo;
        SortDirection = sortDirection;
        Sortable = sortable;

        ColIndex = 0;
        //Colspan = 1;
        //DocItemName = "";
        //IsCalculated = false;
        IsNavigation = false;
        NavigateOnclick = "";

        ProcessBgAndForeColor();
        ForeColorClass = ForeColorClass;
        BorderBottom = borderBottom;

        TopPadding = topPadding;
        BottomPadding = bottomPadding;
    }
    #endregion


    #region Methods
    public override string ToHtml()
    {
        string sRet;
        var sbCellContent = new StringBuilder();
        var isTitleTd = true;

        if (Calculated)
        {
            //If BackColor.Length = 0 Then BackColor = "#FFFFFF"

            if (ForeColor.Length == 0 & BackColor.Length > 0)
            {
                ForeColor = BackColor.GetForeColor();
                ForeColorClass = BackColor.GetForeColorClass();
            }

            sbCellContent.Append(RenderCalcCell());
            isTitleTd = false;
        }
        else
        {
            if (BackColor.Length == 0 && ForeColor.Length == 0)
            {
                sbCellContent.Append(RenderCell());
            }
            else
            {
                if (BackColor.Length > 0 && ForeColor.Length == 0)
                {
                    ForeColor = BackColor.GetForeColor();
                    ForeColorClass = BackColor.GetForeColorClass();
                    //Moved inside if so that if only foregroundcolor is set the title is shown
                    isTitleTd = false;
                }
                sbCellContent.Append(RenderCell());
                // Endret fra denne sbCellContent.Append(RenderCalcCell())
            }
        }
        if (isTitleTd)
        {
            sRet = RenderTd(sbCellContent.ToString(), Title);
        }
        else
        {
            sRet = RenderTd(sbCellContent.ToString());
        }

        return sRet;
    }

 
    ////public void ToWord(DocumentBuilder documentBuilder, double pageWidth, ICollection<DocumentItemDto> docItems, double[] widths, int index, int format)
    ////{
    ////    double dWidth;
    ////    var documentItem = FindDocItem(docItems, DocItemName);
    ////    if (Colspan == 1)
    ////    {
    ////        dWidth = widths[index];
    ////    }
    ////    else
    ////    {
    ////        dWidth = 0;
    ////        for (var j = index; j <= index + Colspan - 1; j++)
    ////        {
    ////            if (widths.Length > j)
    ////            {
    ////                dWidth += widths[j];
    ////            }
    ////        }
    ////    }
    ////    var pptCellAlign = (int)(Align == AsposeCellAlign.Right ? ParagraphAlignment.Right : Align == AsposeCellAlign.Center ? ParagraphAlignment.Center : ParagraphAlignment.Left);
    ////    var iVAlign = (int)(VerticalAlign == AsposeCellVerticalAlign.Undefined ? AsposeCellVerticalAlign.Middle : VerticalAlign);
    ////    AddWordCell(documentBuilder, dWidth, (ParagraphAlignment)pptCellAlign, Text, documentItem, BackColor, ForeColor, vAlign: (ParagraphAlignment)iVAlign);
    ////    foreach (var item in Items)
    ////    {
    ////        if (item == null) continue;
    ////        if (item is TextBlock)
    ////        {
    ////            (item as TextBlock)?.ToWord(documentBuilder, pageWidth, docItems, format);
    ////        }
    ////        else if (item is Chart)
    ////        {
    ////            (item as Chart)?.ToWord(documentBuilder, pageWidth, docItems);
    ////        }
    ////        else if (item is Image)
    ////        {
    ////            (item as Image)?.ToWord(documentBuilder, pageWidth, docItems);
    ////        }
    ////        else if (item is Table)
    ////        {
    ////            (item as Table)?.ToWord(documentBuilder, dWidth, docItems, format);
    ////        }
    ////    }
    ////}      

    ////public void ToPpt(Presentation presentation, Slide slide, Aspose.Slides.Table pptTable, int rowIndex, ref int columnIndex, ICollection<DocumentItemDto> docItems, double[] widths)
    ////{
    ////    double width;
    ////    var documentItem = FindDocItem(docItems, DocItemName);
    ////    if (Colspan == 1)
    ////    {
    ////        width = widths[columnIndex];
    ////    }
    ////    else
    ////    {
    ////        width = 0;
    ////        for (var j = columnIndex; j <= columnIndex + Colspan - 1; j++)
    ////        {
    ////            if (widths.Length > j)
    ////            {
    ////                width += widths[j];
    ////            }
    ////        }
    ////    }
    ////    PptAddCell(presentation, pptTable, rowIndex, columnIndex, Text, width, documentItem, BackColor, (TextAlignment)Align, Colspan, "", Title, ForeColor, (AnchorText)VerticalAlign);
    ////    foreach (var item in Items)
    ////    {
    ////        //Tekst
    ////        if (item is TextBlock)
    ////        {
    ////            (item as TextBlock)?.ToPpt(presentation, pptTable, rowIndex, columnIndex, width, documentItem, BackColor);
    ////        }
    ////        else if (item is Image)
    ////        {
    ////            (item as Image)?.ToPptCell(presentation, slide, pptTable, rowIndex, columnIndex);
    ////        }
    ////    }
    ////    columnIndex += Colspan;
    ////}

    /// <summary>
    /// Removes the js event.
    /// </summary>
    public override void RemoveJsEvent()
    {
        if (Attributes.ContainsKey("onclick"))
            Attributes.Remove("onclick");

        if (Attributes.ContainsKey("href"))
            Attributes.Remove("href");

        foreach (var item in Items)
        {
            item.RemoveJsEvent();
        }
    }

    /// <summary>
    /// Removes the tooltip.
    /// </summary>
    public override void RemoveTooltip()
    {
        Title = string.Empty;
        foreach (var item in Items)
        {
            item.RemoveTooltip();
        }
    }
    #endregion


    #region Helpers
    /// <summary>
    /// Processes the color of the bg and fore.
    /// </summary>
    private void ProcessBgAndForeColor()
    {
        //if (IsCalculated && BackColor.Length == 0)
        //{
        //BackColor = "#FFFFFF";
        //}

        //if (BackColor.Length <= 0 || ForeColor.Length != 0) return;
        //ForeColor = GetForeColor(BackColor);
        //ForeColorClass = GetForeColorClass(BackColor);

        if (!string.IsNullOrEmpty(ForeColor) || string.IsNullOrEmpty(BackColor)) return;
        ForeColor = BackColor.GetForeColor();
        ForeColorClass = BackColor.GetForeColorClass();
    }

    /// <summary>
    /// Renders the td.
    /// </summary>
    /// <param name="cellContent">Content of the cell.</param>
    /// <param name="sTitle">The s title.</param>
    /// <returns>System.String.</returns>
    private string RenderTd(string cellContent, string sTitle = "")
    {
        var stringBuilder = new StringBuilder();

        string sTag = RenderAsTableHead ? "th" : "td";
        stringBuilder.Append("<" + sTag);
        if (CssClass.Length > 0 | Row.CellClass.Length > 0)
        {
            stringBuilder.Append(" class='" + (Row.CellClass.Length > 0 ? CssClass.Length > 0 ? Row.CellClass + " " + CssClass : Row.CellClass : CssClass) + "'");
        }
        if (Colspan > 1)
            stringBuilder.Append(" colspan='" + Colspan + "'");
        if (Rowspan > 1)
            stringBuilder.Append(" rowspan='" + Rowspan + "'");
        if (sTitle.Length > 0)
            stringBuilder.Append(" title='" + sTitle.Replace("'", "&apos;") + "'");
        if (!Calculated && BackColor.Length > 0 | ForeColor.Length > 0)
        {
            stringBuilder.Append(" style='"
                + (BackColor.Length > 0 ? "background-color:" + BackColor + "!important;" : "")
                + (ForeColor.Length > 0 ? "color:" + ForeColor + "!important;" : "" + "'"));
        }
        if ((int)Align > -1)
        {
            switch (Align)
            {
                case AsposeCellAlign.Left:
                    stringBuilder.Append(" align='left' ");
                    break;
                case AsposeCellAlign.Center:
                    stringBuilder.Append(" align='center' ");
                    break;
                case AsposeCellAlign.Right:
                    stringBuilder.Append(" align='right' ");
                    break;
            }
        }

        if ((int)VerticalAlign > -1)
        {
            switch (VerticalAlign)
            {
                case AsposeCellVerticalAlign.Top:
                    stringBuilder.Append(" valign='top' ");
                    break;
                case AsposeCellVerticalAlign.Middle:
                    stringBuilder.Append(" valign='middle' ");
                    break;
                case AsposeCellVerticalAlign.Bottom:
                    stringBuilder.Append(" valign='bottom' ");
                    break;
            }
        }

        foreach (var attr in Attributes)
        {
            stringBuilder.Append(" " + attr.Key + "=\"" + attr.Value.Replace("\"", "&quot;") + "\"");
        }
        stringBuilder.Append(">");

        stringBuilder.Append(cellContent);

        stringBuilder.Append("</" + sTag + ">");

        return stringBuilder.ToString();
    }

    /// <summary>
    /// Renders the calculate cell.
    /// </summary>
    /// <returns>System.String.</returns>
    private string RenderCalcCell()
    {
        var strBuilder = new StringBuilder();

        //Start Div
        strBuilder.Append(string.Format("<div class='divCalcCell' style='background-color:{0};border-color:{1};'>",
            BackColor, BackColor.GetColorAdjustBrightness(0.9)));

        //Left span
        if (ForeColorClass.Length > 0)
        {
            strBuilder.Append(string.Format("<span class='calcCellText {0}' title='{1}'>",
                ForeColorClass, Title.Replace("'", "&apos;")));
        }
        else if (ForeColor.Length > 0)
        {
            strBuilder.Append(string.Format("<span class='calcCellText' style='color:{0}' title='{1}'>",
                ForeColor, Title.Replace("'", "&apos;")));
        }
        else
        {
            strBuilder.Append("<span class='calcCellText'>");
        }

        strBuilder = new StringBuilder();
        strBuilder.Append(Text);

        var isCalcCellN = false;
        foreach (var item in Items)
        {
            if (item.ToHtml().IndexOf("calcCell_") > -1)
            {
                strBuilder.Append(item.ToHtml());
                isCalcCellN = true;
            }
        }

        if (strBuilder.Length == 0)
        {
            strBuilder.Append("&nbsp;");
            //So the span will not collapse the design
        }
        strBuilder.Append(strBuilder.ToString());
        strBuilder.Append("</span>");
        //Right Span/Elements
        var iElementCount = 0;
        var sDivEditIcon = "";

        if (Items.Count > 0)
        {
            var sInlineStyleBg = string.Format(" style='background-color:{0};'",
                BackColor.GetColorAdjustBrightness(0.8));
            const string classCheckTrend = "'calcCellTrend'";
            const string classCheckSettings = "'editIcon'";
            const string classCheckN = "'calcCell_";

            foreach (var item in Items)
            {
                strBuilder = new StringBuilder();
                strBuilder.Append(item.ToHtml());

                int indexTrend = strBuilder.ToString().IndexOf(classCheckTrend);

                if (indexTrend > 0)
                {
                    strBuilder.Insert(indexTrend + classCheckTrend.Length, sInlineStyleBg);

                    strBuilder.Append(strBuilder.ToString());
                    iElementCount += 1;
                }
                else
                {
                    var indexEditIcon = strBuilder.ToString().IndexOf(classCheckSettings);
                    if (indexEditIcon > 0)
                    {
                        sDivEditIcon = strBuilder.ToString();
                    }
                    else
                    {
                        var indexN = strBuilder.ToString().IndexOf(classCheckN);
                        //Do not add 'calcCell_N' or 'calcCell_V' here. It have to be rendered inside calcCellText
                        if (indexN < 0)
                        {
                            strBuilder.Append(strBuilder.ToString());
                            iElementCount += 1;
                        }
                    }
                }
            }
        }

        if (Navigation)
        {
            iElementCount += 1;
            strBuilder.Append(string.Format("<span class='calcCellNavigate' onclick='{0}'>&nbsp;</span>", NavigateOnclick));
        }
        if (iElementCount == 0)
        {
            //Needs special styling if this span is the only element in the div.
            strBuilder.Replace("calcCellText", "calcCellTextStandalone");
        }
        if (isCalcCellN)
        {
            //If cell renders N= or V=, then the div needs to be styled specific and need to change class
            strBuilder.Replace("divCalcCell", "divCalcCell_DualLine");
        }
        strBuilder.Append("</div>");
        if (sDivEditIcon.Length > 0)
        {
            //This icon needs to be renderer outside the divCalcCell but inside the td
            strBuilder.Append(sDivEditIcon);
        }
        return strBuilder.ToString();
    }

    private string RenderCell()
    {
        var stringBuilder = new StringBuilder();
        stringBuilder.Append(Navigation
                                 ? string.Format("<a href='#' onclick='{0}'>{1}</a>", NavigateOnclick, Text)
                                 : Text);
        foreach (var item in Items)
        {
            stringBuilder.Append(item.ToHtml());
        }
        return stringBuilder.ToString();
    }

    public LineStyle BorderBottom;
    public LineStyle BorderTop;
    public LineStyle BorderLeft;
    public LineStyle BorderRight;
    private void AddWordCell(DocumentBuilder documentBuilder, double width, ParagraphAlignment align, string text, DocumentItemDto documentItem, string backColor = "",
                            string foreColor = "", bool isTop = true, bool isBottom = true, string imgFile = "",
                            bool isUseBorderColor = false, ParagraphAlignment vAlign = (ParagraphAlignment)CellVerticalAlignment.Center, string borderStyle = "")
    {
        documentBuilder.InsertCell();
        documentBuilder.CellFormat.PreferredWidth = PreferredWidth.FromPoints(width);
        documentBuilder.CellFormat.Width = width;
        documentBuilder.CellFormat.VerticalAlignment = (CellVerticalAlignment)vAlign;
        documentBuilder.ParagraphFormat.Alignment = align;
        documentBuilder.CellFormat.Borders.ClearFormatting();
        //if (isBottom)
        documentBuilder.CellFormat.Borders[BorderType.Bottom].LineStyle = BorderBottom;
        documentBuilder.CellFormat.Borders[BorderType.Top].LineStyle = BorderTop; //LineStyle.Single;
        documentBuilder.CellFormat.Borders[BorderType.Left].LineStyle = BorderLeft;// LineStyle.Single;
        documentBuilder.CellFormat.Borders[BorderType.Right].LineStyle = BorderRight;// LineStyle.Single;

        documentBuilder.CellFormat.Shading.BackgroundPatternColor = string.IsNullOrEmpty(backColor) ? Color.White : ColorTranslator.FromHtml(backColor);
        documentBuilder.Font.Color = string.IsNullOrEmpty(foreColor) ? Color.Black : ColorTranslator.FromHtml(foreColor);
        documentBuilder.CellFormat.BottomPadding = 2;
        documentBuilder.CellFormat.TopPadding = 2;
        documentBuilder.Font.Bold = false;
        if (documentItem != null)
        {
            if (!string.IsNullOrEmpty(documentItem.BGColor))
            {
                documentBuilder.CellFormat.Shading.BackgroundPatternColor = ColorTranslator.FromHtml(documentItem.BGColor);
            }
            if (documentItem.FontIsBold != 0)
                documentBuilder.Font.Bold = true;
            if (!string.IsNullOrEmpty(documentItem.FGColor))
                documentBuilder.Font.Color = ColorTranslator.FromHtml(documentItem.FGColor);
            if (!string.IsNullOrEmpty(documentItem.FontName))
            {
                documentBuilder.Font.Name = documentItem.FontName;
            }
            if (documentItem.FontSize > 0)
                documentBuilder.Font.Size = documentItem.FontSize;
            documentItem.BorderColor = "#FFFFF";
            if (documentItem.BorderColor != "#000000")
            {
                documentBuilder.CellFormat.Borders.Color = ColorTranslator.FromHtml(documentItem.BorderColor);
                documentBuilder.CellFormat.Borders.LineStyle = LineStyle.Single;
                documentBuilder.CellFormat.Borders.DistanceFromText = 2;
            }
        }
        if (!string.IsNullOrEmpty(backColor))
        {
            documentBuilder.CellFormat.Shading.BackgroundPatternColor = ColorTranslator.FromHtml(backColor);
        }
        if (!string.IsNullOrEmpty(foreColor))
        {
            documentBuilder.Font.Color = ColorTranslator.FromHtml(foreColor);
        }
        if (!string.IsNullOrEmpty(Title))
        {
            //oDB.InsertFootnote(Aspose.Words.FootnoteType.Endnote, RemoveHTMLTags(Title))
            var comment = new Comment(documentBuilder.Document) { Author = "CxStudio", DateTime = DateTime.Now, Initial = "CS" };
            documentBuilder.CurrentParagraph.AppendChild(comment);
            comment.Paragraphs.Add(new Paragraph(documentBuilder.Document));
            comment.FirstParagraph.Runs.Add(new Run(documentBuilder.Document, Title.RemoveHtmlTags()));

        }
        if (!string.IsNullOrEmpty(text))
        {
            documentBuilder.InsertHtml(text);
            //if (text.Contains("<") & text.Contains(">"))
            //{
            //    documentBuilder.InsertHtml(text);
            //}
            //else
            //{
            //    documentBuilder.Write(text);
            //}
        }
        if (!string.IsNullOrEmpty(imgFile))
        {
            documentBuilder.InsertImage(imgFile, 16, 16);
        }
    }
    #endregion
}
