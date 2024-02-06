using Plex.UIComponents.Dtos;

namespace Plex.UIComponents.UIComponents;

[Serializable]
public class BaseUIComponent
{
    public string Type => this.GetType().Name;
    public virtual string ToHtml()
    {
        return string.Empty;
    }
    public DocumentItemDto? FindDocItem(IEnumerable<DocumentItemDto> aDocItems, string docItemName)
    {
        if (aDocItems != null)
        {
            return aDocItems.First(oItem => oItem.DocumentItemType?.Name?.ToLower() == docItemName.ToLower());
        }
        return null;
    }
    ////public void PptAddCell(Presentation presentation, Aspose.Slides.Table pptTable, int rowIndex, int colIndex, string text, double width,
    ////                        DocumentItemDto documentItem, string backGroundColor, TextAlignment align = TextAlignment.Left, int colspan = 0, string trend = "",
    ////                        string note = "", string foreColor = "", TextAnchorType verticalAlign = TextAnchorType.Center)
    ////{
    ////    Portion portion;
    ////    var spliter = new Regex(Environment.NewLine);
    ////    if (text == null)
    ////        text = string.Empty;
    ////    var texts = spliter.Split(text.RemoveHtmlTags());
    ////    var cell = pptTable[colIndex, rowIndex];

    ////    var mergeColIndex = colIndex + 1;
    ////    var tempColSpan = colspan;
    ////    while (tempColSpan > 1 && mergeColIndex < pptTable.Columns.Count)
    ////    {
    ////        pptTable.MergeCells(cell, pptTable[mergeColIndex, rowIndex], true);
    ////        --tempColSpan;
    ////        ++mergeColIndex;
    ////    }

    ////    cell.FillFormat.FillType = FillType.Solid;
    ////    if (documentItem != null)
    ////    {
    ////        cell.FillFormat.Set.C = ColorTranslator.FromHtml(documentItem.BGColor ?? "");
    ////        cell.TextFrame.FillFormat.ForeColor = ColorTranslator.FromHtml(documentItem.BGColor ?? "");
    ////    }
    ////    if (!string.IsNullOrEmpty(backGroundColor))
    ////    {
    ////        cell.TextFrame.FillFormat.BackColor = ColorTranslator.FromHtml(backGroundColor);
    ////        cell.TextFrame.FillFormat.ForeColor = ColorTranslator.FromHtml(backGroundColor);
    ////    }
    ////    cell.TextFrame.LineFormat.Style = LineStyle.ThinThin;
    ////    cell.TextFrame.FitTextToShape();
    ////    if (colspan == 1)
    ////    {
    ////        pptTable.SeCotColumnWidth(colIndex, (int)width);
    ////    }

    ////    pptTable.SetRowHeight(rowIndex, 18);
    ////    if (documentItem != null)
    ////    {
    ////        portion = cell.TextFrame.Paragraphs[0].Portions[0];
    ////        SetPptFont(presentation, portion, documentItem.FontName ?? "");
    ////        portion.FontBold = documentItem.FontIsBold != 0;
    ////        portion.FontHeight = (short)documentItem.FontSize;
    ////        portion.FontColor = ColorTranslator.FromHtml(documentItem.FGColor ?? "");
    ////        if (!string.IsNullOrEmpty(foreColor))
    ////        {
    ////            portion.FontColor = ColorTranslator.FromHtml(foreColor);
    ////        }
    ////    }
    ////    cell.TextFrame.Paragraphs[0].Portions[0].Text = texts[0];
    ////    cell.TextFrame.Paragraphs[0].Alignment = align;
    ////    cell.TextFrame.Paragraphs[0].RawAlignment = align;
    ////    cell.TextFrame.AnchorText = verticalAlign;
    ////    while (cell.TextFrame.Paragraphs[0].Portions.Count > 1)
    ////    {
    ////        cell.TextFrame.Paragraphs[0].Portions.RemoveAt(1);
    ////    }
    ////    if (texts.Length > 1)
    ////    {
    ////        var index = 1;
    ////        while (index < texts.Length)
    ////        {
    ////            portion = new Portion();

    ////            if (documentItem != null)
    ////            {
    ////                SetPptFont(presentation, portion, documentItem.FontName ?? "");
    ////                portion.FontBold = documentItem.FontIsBold != 0;
    ////                portion.FontHeight = (short)documentItem.FontSize;
    ////            }
    ////            portion.Text = texts[index];
    ////            if (documentItem != null)
    ////            {
    ////                portion.FontColor = ColorTranslator.FromHtml(documentItem.FGColor);
    ////            }
    ////            if (!string.IsNullOrEmpty(foreColor))
    ////            {
    ////                portion.FontColor = ColorTranslator.FromHtml(foreColor);
    ////            }
    ////            cell.TextFrame.Paragraphs[0].Portions.Add(portion);
    ////            index += 1;
    ////        }

    ////    }
    ////    if (!string.IsNullOrEmpty(note))
    ////    {
    ////        var nt = pptTable.Parent.AddNotes();
    ////        var para = new Paragraph();
    ////        var port = new Portion("Rad:" + (rowIndex + 1).ToString(CultureInfo.InvariantCulture)
    ////                                          + " Kollone:" + (colIndex + 1).ToString(CultureInfo.InvariantCulture)
    ////                                          + Environment.NewLine + note + Environment.NewLine);
    ////        para.BulletType = BulletType.Numbered;
    ////        para.Portions.Add(port);
    ////        if (!string.IsNullOrEmpty(backGroundColor))
    ////        {
    ////            para.Portions[0].FontColor = ColorTranslator.FromHtml(backGroundColor);
    ////        }
    ////        para.SpaceAfter = 10;
    ////        nt.Paragraphs.Add(para);

    ////    }
    ////    if (!string.IsNullOrEmpty(trend))
    ////    {
    ////        AddTrendImg(presentation, cell, trend);
    ////    }
    ////}

    ////public void SetPptFont(Presentation presentation, Portion portion, string fontName)
    ////{
    ////    var index = 0;

    ////    while (index < presentation.Fonts.Count)
    ////    {
    ////        if (presentation.Fonts[index].FontName == fontName)
    ////        {
    ////            portion.FontIndex = presentation.Fonts[index].FontId;
    ////            return;
    ////        }
    ////        index += 1;
    ////    }
    ////    var font = new FontEntity(presentation, presentation.Fonts[0])
    ////    {
    ////        FontName = fontName,
    ////        CharSet = FontCharSet.ANSI_CHARSET,
    ////        Family = Aspose.Slides.FontFamily.DONTCARE,
    ////        Pitch = FontPitch.DEFAULT_PITCH,
    ////        Quality = FontQuality.PROOF_QUALITY
    ////    };
    ////    var fontIndex = presentation.Fonts.Add(font);
    ////    portion.FontIndex = fontIndex;
    ////}

    ////public void AddTrendImg(Presentation presentation, Aspose.Slides.Cell cell, string trend)
    ////{
    ////    var pic = new PictureBullet(presentation, trend);
    ////    object picId = presentation.PictureBullets.Add(pic);
    ////    cell.TextFrame.Paragraphs[0].HasBullet = true;
    ////    cell.TextFrame.Paragraphs[0].BulletType = BulletType.Picture;
    ////    cell.TextFrame.Paragraphs[0].PictureBulletId = (short)picId;
    ////}
    
    public virtual void RemoveJsEvent()
    {
        // Do nothing
    }
    public virtual void RemoveTooltip() { }
    public virtual int BaseWidth
    {
        get { return 0; }
    }
    public virtual int Height
    {
        get { return 0; }
    }
    public virtual Stream? Content
    {
        get { return null; }
    }
    public int ItemId { get; set; }
}
