using System.Text;

namespace Plex.UIComponents.UIComponents;

[Serializable]
public class Table : BaseUIComponent
{
    public string CssClass = "";
    public List<Column> Columns = [];
    public List<Row> Rows = [];
    public Dictionary<string, string> Attributes = [];
    public string PreTableHtml = "";
    public string PostTableHtml = "";
    public bool InsertLineBreak = true;
    public int LeftIndent = 0;
    public double PreferredWidthPx = -1;
    public bool IsKeepTableFromBreakingAcrossPages { get; set; }

    public override string ToHtml()
    {
        var stringBuilder = new StringBuilder();
        if (!string.IsNullOrWhiteSpace(PreTableHtml))
        {
            stringBuilder.Append(PreTableHtml);
        }
        stringBuilder.Append("<table" + (CssClass.Length > 0 ? " class='" + CssClass + "'" : ""));
        if (!Attributes.ContainsKey("cellspacing"))
        {
            stringBuilder.Append(" cellspacing='0'");
        }
        if (!Attributes.ContainsKey("cellpadding"))
        {
            stringBuilder.Append(" cellpadding='0'");
        }
        foreach (var attr in Attributes)
        {
            stringBuilder.Append(" " + attr.Key + "='" + attr.Value.Replace("'", "&apos;") + "'");
        }
        stringBuilder.Append(">");
        if (Columns.Count > 0)
        {
            stringBuilder.Append("<colgroup>");
            foreach (var column in Columns)
            {
                stringBuilder.Append("<col"
                    + (column.Width > 0 ? " style='width:" + column.Width + "px;'" : "")
                    + (column.CssClass.Length > 0 ? " class='" + column.CssClass + "'" : "")
                    + " />");
            }
            stringBuilder.Append("</colgroup>");
            foreach (var row in Rows)
            {
                stringBuilder.Append(row.ToHtml());
            }
        }
        stringBuilder.Append("</table>");
        if (!string.IsNullOrWhiteSpace(PostTableHtml))
        {
            stringBuilder.Append(PostTableHtml);
        }
        return stringBuilder.ToString();
    }
    public Row NewRow(bool createCells = false)
    {
        var oRet = new Row { Table = this, RowIndex = Rows.Count };
        if (createCells)
        {
            if (Columns.Count > 0)
            {
                for (var i = 1; i <= Columns.Count; i++)
                {
                    oRet.Cells.Add(oRet.NewCell());
                }
            }
        }
        return oRet;
    }
    ////public void ToWord(DocumentBuilder documentBuilder, double pageWidth, ICollection<DocumentItemDto> docItems, int format)
    ////{
    ////    var defaultColWidth = 80;
    ////    const int minColWidth = 50;
    ////    int index;
    ////    //Calculate auto-width columns without specific width
    ////    var autoColumns = 0;
    ////    var width = pageWidth;
    ////    var columnWidths = new double[Columns.Count];
    ////    foreach (var col in Columns)
    ////    {
    ////        if (col.Width == 0)
    ////            autoColumns += 1;
    ////        else
    ////            width -= col.Width;
    ////    }
    ////    if (Columns.Count < 3)
    ////    {
    ////        defaultColWidth = 120;
    ////    }
    ////    if (width < Convert.ToDouble(autoColumns * minColWidth) && autoColumns > 0)
    ////    {
    ////        for (index = 0; index <= Columns.Count - 1; index++)
    ////        {
    ////            if (Columns[index].Width != 0) continue;
    ////            columnWidths[index] = defaultColWidth;
    ////            width -= defaultColWidth;
    ////            Columns[index].Width = defaultColWidth;
    ////            if (!(index == 0 & format == 1)) continue;
    ////            columnWidths[index] += 10;
    ////            width -= 10;
    ////            Columns[index].Width += 10;
    ////        }
    ////    }
    ////    if (width < 0)
    ////    {
    ////        var factor = pageWidth / (pageWidth + Math.Abs(width));
    ////        for (index = 0; index <= Columns.Count - 1; index++)
    ////        {
    ////            Columns[index].Width = Convert.ToInt32(Columns[index].Width * factor);
    ////        }
    ////    }
    ////    for (index = 0; index <= Columns.Count - 1; index++)
    ////    {
    ////        if (Columns[index].Width == 0)
    ////            columnWidths[index] = width / autoColumns;
    ////        else
    ////            columnWidths[index] = Columns[index].Width;
    ////    }
    ////    documentBuilder.StartTable();
    ////    foreach (var oRow in Rows)
    ////    {
    ////        oRow.ToWord(documentBuilder, pageWidth, docItems, columnWidths, format);
    ////    }
    ////    documentBuilder.EndTable();
    ////    documentBuilder.InsertBreak(BreakType.LineBreak);
    ////}
    ////public void ToPpt(Presentation presentation, ref Slide slide, double pageWidth, ref double yPos,
    ////                  ICollection<DocumentItemDto> docItems, double yStartPosition)
    ////{
    ////    int index;
    ////    var rowIndex = 0;
    ////    foreach (var row in Rows)
    ////    {
    ////        row.RowIndex = rowIndex;
    ////        rowIndex += 1;
    ////    }
    ////    var autoColumns = 0;
    ////    var width = pageWidth;
    ////    var columnwidths = new double[Columns.Count];
    ////    foreach (var item in Columns)
    ////    {
    ////        if (item.Width == 0)
    ////        {
    ////            autoColumns += 1;
    ////        }
    ////        else
    ////        {
    ////            item.Width = Convert.ToInt32(item.Width * 8);
    ////            width -= item.Width;
    ////        }
    ////    }
    ////    if (width < 800 && autoColumns > 0)
    ////    {
    ////        for (index = 0; index <= Columns.Count - 1; index++)
    ////        {
    ////            if (Columns[index].Width != 0) continue;
    ////            columnwidths[index] = 800;
    ////            width -= 800;
    ////            Columns[index].Width = 800;
    ////        }
    ////    }
    ////    if (width < 0)
    ////    {
    ////        var factor = pageWidth / (pageWidth + Math.Abs(width));
    ////        for (index = 0; index <= Columns.Count - 1; index++)
    ////        {
    ////            Columns[index].Width = Convert.ToInt32(Columns[index].Width * factor);
    ////        }
    ////    }
    ////    for (index = 0; index <= Columns.Count - 1; index++)
    ////    {
    ////        if (Columns[index].Width == 0)
    ////            columnwidths[index] = width / autoColumns;
    ////        else
    ////            columnwidths[index] = Columns[index].Width;
    ////    }
    ////    var documentItem = FindDocItem(docItems, "TableHeader");
    ////    var pptTable = slide.Shapes.AddTable(200, (int)yPos, (int)pageWidth, 200, Columns.Count, 1, 0.5, ColorTranslator.FromHtml(documentItem.BorderColor));
    ////    //var pptTable = slide.Shapes.AddTable(200, (int)yPos, (int)pageWidth, 200, Columns.Count, 2, 0.5, ColorTranslator.FromHtml(documentItem.BorderColor));
    ////    var tableRowIndex = 0;
    ////    var rowRemainCount = Rows.Count;
    ////    foreach (var row in Rows)
    ////    {
    ////        //row to ppt
    ////        row.ToPpt(presentation, slide, pptTable, tableRowIndex, docItems, columnwidths);
    ////        if (tableRowIndex < Rows.Count - 1)
    ////        {
    ////            CheckTableHeightPpt(presentation, ref slide, ref pptTable, ref yPos, pageWidth, ref tableRowIndex, row, docItems, columnwidths, documentItem, yStartPosition);
    ////        }
    ////        rowRemainCount--;
    ////        tableRowIndex += 1;
    ////        if (rowRemainCount > 0)
    ////            pptTable.AddRow();
    ////    }
    ////    pptTable.Width = (int)pageWidth;
    ////    yPos += pptTable.Height + 100;
    ////}

    /// <summary>
    /// Removes the js event.
    /// </summary>
    public override void RemoveJsEvent()
    {
        foreach (var row in Rows)
        {
            row.RemoveJsEvent();
        }
    }

    /// <summary>
    /// Removes the tooltip.
    /// </summary>
    public override void RemoveTooltip()
    {
        foreach (var row in Rows)
        {
            row.RemoveTooltip();
        }
    }
    ////private void CheckTableHeightPpt(Presentation presentation, ref Slide slide, ref Aspose.Slides.Table pptTable, ref double yPos, double pageWidth, ref int tableRowIndex, Row tableUIRow, ICollection<DocumentItemDto> documentItems, double[] widths, DocumentItemDto documentItem, double yStartPosition)
    ////{
    ////    if (yPos + pptTable.Height <= 3600) return;
    ////    //Delete the row cause too height table
    ////    pptTable.DeleteRow(pptTable.RowsNumber - 1);
    ////    //When row cause too height table, change it to the first row of a new table in a new slide
    ////    pptTable.Width = (int)pageWidth;
    ////    slide = presentation.AddEmptySlide();
    ////    var newTable = slide.Shapes.AddTable(200, (int)yStartPosition, (int)pageWidth, 200, Columns.Count, 1, 0.5, ColorTranslator.FromHtml(documentItem.BorderColor));
    ////    tableUIRow.ToPpt(presentation, slide, newTable, 0, documentItems, widths);
    ////    newTable.SetRowHeight(0, 18);
    ////    //Change current table to new table for continue adding remaining rows.
    ////    pptTable = newTable;
    ////    tableRowIndex = 0;
    ////    yPos = yStartPosition + newTable.Height;
    ////}
}
