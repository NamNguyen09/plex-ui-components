using System.Text;
using Aspose.Words;
using Plex.UIComponents.Dtos;

namespace Plex.UIComponents.UIComponents;

[Serializable]
public class Row : BaseUIComponent
{
    public Table Table = new();
    public int RowIndex { get; set; }
    public int RowType { get; set; }
    public string CssClass { get; set; }
    public string CellClass { get; set; }
    public string CellDocItemName { get; set; }
    public List<Cell> Cells { get; set; } = [];
    public Dictionary<string, string> Attributes { get; set; } = [];
    public bool IsHeaderRow { get; set; }
    public Row()
    {
        RowIndex = 0;
        RowType = 0;
        CssClass = "";
        CellClass = "";
        CellDocItemName = "";
        IsHeaderRow = false;
    }

    public Cell NewCell(string text = "", string title = "", AsposeCellAlign align = AsposeCellAlign.Undefined, string backColor = "",
        string foreColor = "", string cssClass = "", int colspan = 1, int rowspan = 1, string docItemName = "", bool bCalculated = false)
    {
        var oRet = new Cell(text, title, align, backColor, foreColor, cssClass, colspan, rowspan,
                            string.IsNullOrEmpty(docItemName) ? CellDocItemName : docItemName, IsHeaderRow,
                            bCalculated)
        { Row = this, ColIndex = Cells.Count };
        return oRet;
    }
    public void AddCell(string text = "", string title = "", AsposeCellAlign align = AsposeCellAlign.Undefined, string backColor = "",
        string foreColor = "", string cssClass = "", int colspan = 1, int rowspan = 1, string docItemName = "", bool bCalculated = false,
        int sortNo = -1, bool sortDirection = false, bool sortable = false, bool isBold = false, double topPadding = 0.0, double bottomPadding = 0.0)
    {
        var oCell = new Cell(text, title, align, backColor, foreColor, cssClass, colspan, rowspan, string.IsNullOrEmpty(docItemName) ? CellDocItemName : docItemName,
            IsHeaderRow, bCalculated, sortNo, sortDirection, sortable, topPadding: topPadding, bottomPadding: bottomPadding)
        {
            Row = this,
            ColIndex = Cells.Count,
            IsBold = isBold
        };

        Cells.Add(oCell);
    }
    public void InsertCell(int index, string text = "", string title = "", AsposeCellAlign align = AsposeCellAlign.Undefined, string backColor = "",
        string foreColor = "", string cssClass = "", int colspan = 1, int rowspan = 1, string docItemName = "", bool bCalculated = false, int sortNo = -1, bool sortDirection = false, bool sortable = false, bool isBold = false)
    {
        var oCell = new Cell(text, title, align, backColor, foreColor, cssClass, colspan, rowspan,
                             string.IsNullOrEmpty(docItemName) ? CellDocItemName : docItemName, IsHeaderRow,
                             bCalculated, sortNo, sortDirection, sortable)
        { Row = this, ColIndex = Cells.Count, IsBold = isBold };
        Cells.Insert(index, oCell);
    }
    public override string ToHtml()
    {
        var stringBuilder = new StringBuilder();
        if (IsHeaderRow)
        {
            stringBuilder.Append("<thead>");
        }
        stringBuilder.Append("<tr");
        if (CssClass.Length > 0)
        {
            stringBuilder.Append(" class='" + CssClass + "'");
        }
        foreach (var o in Attributes)
        {
            stringBuilder.Append(" " + o.Key + "='" + o.Value.Replace("'", "&apos;") + "'");
        }
        stringBuilder.Append(">");
        foreach (var cell in Cells)
        {
            stringBuilder.Append(cell.ToHtml());
        }
        stringBuilder.Append("</tr>");
        if (IsHeaderRow)
            stringBuilder.Append("</thead>");
        return stringBuilder.ToString();
    }
    public void ToWord(DocumentBuilder documentBuilder, double pageWidth,
        ICollection<DocumentItemDto> docItems,
        double[] widths, int format)
    {
        var i = 0;
        foreach (var oCell in Cells)
        {
            oCell.ToWord(documentBuilder, pageWidth, docItems, widths, i, format);
            i += oCell.Colspan;
        }
        documentBuilder.EndRow();
    }
    ////public void ToPpt(Presentation presentation, Slide slide, Aspose.Slides.Table pptTable, 
    ////    int rowIndex, ICollection<DocumentItemDto> docItems, double[] widths)
    ////{
    ////    var columnIndex = 0;
    ////    foreach (var oCell in Cells)
    ////    {
    ////        oCell.ToPpt(presentation, slide, pptTable, rowIndex, ref columnIndex, docItems, widths);
    ////    }
    ////}
    public override void RemoveJsEvent()
    {
        if (Attributes.ContainsKey("onclick"))
            Attributes.Remove("onclick");

        if (Attributes.ContainsKey("href"))
            Attributes.Remove("href");

        foreach (var cell in Cells)
        {
            cell.RemoveJsEvent();
        }
    }
    public override void RemoveTooltip()
    {
        foreach (var cell in Cells)
        {
            cell.RemoveTooltip();
        }
    }
}