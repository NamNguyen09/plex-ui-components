using System.Drawing;
using System.Drawing.Imaging;
using System.Text.RegularExpressions;
using CXChart;
using BaseCXChart = CXChart;
using Color = System.Drawing.Color;
using Font = System.Drawing.Font;
using Pen = System.Drawing.Pen;

namespace Plex.UIComponents.UIComponents;

/// <summary>
/// CXChart
/// </summary>
[Serializable]
public class CXChart : BaseUIComponent
{
    #region Properties
    /// <summary>
    /// The unique identifier
    /// </summary>
    public string Guid = "";
    /// <summary>
    /// The alt
    /// </summary>
    public string Alt = "";
    /// <summary>
    /// The chart
    /// </summary>
    public BaseChart Chart;
    /// <summary>
    /// The use image map
    /// </summary>
    public bool UseImageMap;
    /// <summary>
    /// The item click type
    /// </summary>
    public int ItemClickType;
    /// <summary>
    /// The attributes
    /// </summary>
    public Dictionary<string, string> Attributes = new Dictionary<string, string>();
    /// <summary>
    /// The use request image type
    /// </summary>
    public bool UseRequestImageType;
    /// <summary>
    /// The ex scene item for human chart
    /// </summary>
    public List<SceneItem> ExSceneItemForHumanChart;
    /// <summary>
    /// The use fill color
    /// </summary>
    public bool UseFillColor;
    /// <summary>
    /// The show percentage
    /// </summary>
    public bool ShowPercentage;
    /// <summary>
    /// The use short name
    /// </summary>
    public bool UseShortName;
    /// <summary>
    /// The view type identifier
    /// </summary>
    public int ViewTypeId;
    /// <summary>
    /// The is export
    /// </summary>
    public bool IsExport;
    /// <summary>
    /// The is readonly
    /// </summary>
    public bool IsReadonly;
    /// <summary>
    /// bold letters
    /// </summary>
    public string BoldLetters;
    /// <summary>
    /// Gets the width.
    /// </summary>
    /// <value>The width.</value>
    public override int BaseWidth
    {
        get
        {
            return Chart == null ? base.BaseWidth : Chart.Dimentions.Width;
        }
    }
    /// <summary>
    /// Use serie point color when rendering
    /// Use for Api Report
    /// </summary>
    public bool UseSeriePointColor;

    /// <summary>
    /// Get prefix for the chart point  label value
    /// </summary>
    public string[] PointValuePrefix { get; set; }
    /// <summary>
    /// Get prefix for the chart point  label value 
    /// </summary>
    public string[] PointValueSuffix { get; set; }
    /// <summary>
    /// indicate an empty chart
    /// </summary>
    public bool IsEmpty { get; set; }
    /// <summary>
    /// Render chart with real value instead of percentage
    /// </summary>
    public bool RenderRealValue { get; set; }

    /// <summary>
    /// Gets the height.
    /// </summary>
    /// <value>The height.</value>
    public override int Height
    {
        get
        {
            return Chart == null ? base.Height : Chart.Dimentions.Height;
        }
    }
    /// <summary>
    /// Gets the content.
    /// </summary>
    /// <value>The content.</value>
    public override Stream Content
    {
        get
        {
            Stream stream = new MemoryStream();
            if (Chart == null) return stream;

            // Save to stream
            Chart.SaveImage(ref stream, ImageFormat.Png);

            return stream;
        }
    }
    #endregion


    #region Constructors
    /// <summary>
    /// Initializes a new instance of the <see cref="CXChart"/> class.
    /// </summary>
    /// <param name="guid">The unique identifier.</param>
    /// <param name="alt">The alt.</param>
    /// <param name="chart">The chart.</param>
    public CXChart(string guid = "", string alt = "", BaseChart chart = null)
    {
        Guid = guid;
        Chart = chart;
        Alt = alt;
        IsExport = false;
        IsReadonly = false;
        UseSeriePointColor = false;
        BoldLetters = string.Empty;
    }
    #endregion


    #region Methods

    /// <summary>
    /// Render Image: for export
    /// </summary>
    /// <returns>Bitmap.</returns>
    public Bitmap RenderImage()
    {
        Stream memoryStream = new MemoryStream();
        memoryStream = ChartToStream(memoryStream);
        return new Bitmap(memoryStream);
    }

    public Stream ChartToStream(Stream memoryStream)
    {
        if (UseFillColor) //Chart with different inside (using for Competence)
        {
            if (!(Chart.GetType() == typeof(SolarChart) && Chart.Series.Count == 1))
            {
                Chart.Margin = new Margin(15);
            }
            //Not use border in this case
            Chart.UsePointBorders = false;
            //Set color of grid be shown in chart
            //Chart.GridStyle.SeriesSeparatorPen = new Pen(Color.LightGray);
            //Chart.GridStyle.IntervalPen = new Pen(Color.LightGray);
            //Set color of text be shown in chart
            //Chart.LabelStyle.Brush = new SolidBrush(Color.DimGray);
            if (ShowPercentage)
            {
                Chart.LabelStyle.Font = new Font("Tahoma", 11, FontStyle.Bold);
            }
            //Chart.ClientAreaStyle.Brush = new SolidBrush(Color.White);
            var letterNo = 0;
            //Get list color of ChartPoints
            var fillColors =
                Chart.Points.Select(p => p.Style.Pen != null ? p.Style.Pen.Color : Color.White).ToList();
            //Chart.SeriesMargin = new Margin(0, 0, 2, 0);
            var sceneItems = Chart.GenerateSceneGraph();
            var series = Chart.Series.FirstOrDefault();
            var listExtraScene = new List<SceneItem>();
            var listBarSceneItems = new Dictionary<int, SceneItem>();
            var totalLetters = Chart.Points.Count;
            var chartPointIndex = 0;
            for (var i = 0; i < sceneItems.Count; i++)
            {
                var sceneItem = sceneItems[i];
                if (sceneItem.Tag.StartsWith("Chartpoint"))
                {
                    //Get color index in array
                    int colorNo;
                    if (UseSeriePointColor)
                    {
                        var seriePointArray = Regex.Matches(sceneItem.Tag, @"\d+");
                        int serieNo;
                        int seriepointNo = 0;
                        if (seriePointArray.Count > 1)
                        {
                            var canProcess = int.TryParse(seriePointArray[0].Value, out serieNo) && int.TryParse(seriePointArray[1].Value, out seriepointNo);
                            if (canProcess)
                            {
                                var serie = Chart.Series[serieNo];
                                var seriePoint = serie.Points[seriepointNo];
                                var scolor = seriePoint.Style.Brush != null
                                    ? seriePoint.Style.Brush
                                    : new SolidBrush(fillColors[seriepointNo]);
                                sceneItem.Style.Brush = scolor;
                                sceneItem.Style.Pen = null;
                            }
                        }

                    }
                    else
                    {
                        int.TryParse(sceneItem.Tag.Substring(sceneItem.Tag.LastIndexOf(':') + 1),
                                 out colorNo);
                        var sColor = fillColors[colorNo];
                        // FillCols is an array of strings containing series colors
                        sceneItem.Style.Brush = new SolidBrush(sColor);
                        sceneItem.Style.Pen = null;
                    }

                    listBarSceneItems.Add(i, sceneItem);
                }
                else if (RenderRealValue && sceneItem.Tag.StartsWith("Template Series"))
                {
                    int pointIndex;
                    int.TryParse(sceneItem.Tag.Substring(sceneItem.Tag.LastIndexOf(':') + 1), out pointIndex);
                    var sceneItemChartPoint = sceneItems.FirstOrDefault(p => p.Tag.StartsWith("Chartpoint") && p.Tag.EndsWith("Point:" + pointIndex));
                    var scoreTemplateF0 = new PointF(sceneItem.Points[0].X,
                                                        sceneItemChartPoint.Points[0].Y);
                    var scoreTemplateF1 = new PointF(sceneItem.Points[1].X,
                                                        sceneItemChartPoint.Points[1].Y);
                    sceneItem.Points = new[] { scoreTemplateF0, scoreTemplateF1 };

                    var scoreTemplateBorderF0 = new PointF(scoreTemplateF1.X + Chart.PlotRect.Left, scoreTemplateF0.Y);
                    var scoreTemplateBorderF1 = new PointF(scoreTemplateF1.X + Chart.PlotRect.Left, scoreTemplateF0.Y + scoreTemplateF1.Y);
                    var points = new[] { scoreTemplateBorderF0, scoreTemplateBorderF1 };
                    //Render Childrensgrade or Youthgrade
                    var sceneItemScoreTemplate = new SceneItem(SceneItem.ItemType.Line, new BaseCXChart.Style(new Pen(sceneItem.Style.Pen.Color, 4), null, null), points, null, "TemplateBorderRight");
                    listExtraScene.Add(sceneItemScoreTemplate);
                    sceneItem.Style.Pen = new Pen(sceneItem.Style.Brush);
                }
                else if (RenderRealValue && sceneItem.Tag.StartsWith("R_") && sceneItem.Type == SceneItem.ItemType.Text)
                {
                    //Take the actual value from Chart.Points
                    sceneItem.Text = Chart.Points[chartPointIndex].Value.ToString();
                    sceneItem.LabelStyle.Brush = new SolidBrush(Color.Black);
                    //If it is 100% so set the X-Axis maximum so that do not need add extra margin at the code below
                    if (Chart.Points[chartPointIndex].Value == Chart.GridStyle.MaxValue)
                    {
                        sceneItem.Points[0].X = Chart.PlotRect.Right;
                    }
                    chartPointIndex++;
                }
                //else if (sceneItem.Tag.StartsWith("Indicator"))
                //{
                //    sceneItem.Style.Brush = new SolidBrush(Color.White);
                //    sceneItem.Style.Pen = new Pen(Color.DimGray, 1);
                //}
                //Render percent
                if (Chart.GetType() == typeof(SolarChart) && ShowPercentage &&
                    !string.IsNullOrEmpty(sceneItem.Tag) &&
                    sceneItem.Type == SceneItem.ItemType.Text)
                {
                    //Percent value
                    var sceneItemTextPercent = new SceneItem();
                    sceneItemTextPercent.Type = SceneItem.ItemType.Text;
                    //sceneItemTextPercent.LabelStyle.Brush = new SolidBrush(Color.DimGray);
                    sceneItemTextPercent.LabelStyle.Font = new Font("Tahoma", 11);
                    var pointTextPercentF0 = new PointF(sceneItem.Points[0].X,
                                                        sceneItem.Points[0].Y + 15);
                    if (letterNo >= totalLetters * 1 / 4 && letterNo < totalLetters * 3 / 4)
                    {
                        pointTextPercentF0 = new PointF(sceneItem.Points[0].X - 10,
                                                        sceneItem.Points[0].Y + 15);
                    }
                    else if (letterNo >= totalLetters * 3 / 4)
                    {
                        pointTextPercentF0 = new PointF(sceneItem.Points[0].X - 22,
                                                        sceneItem.Points[0].Y + 15);
                    }
                    var pointTextPercentF1 = new PointF(sceneItem.Points[1].X + 50,
                                                        sceneItem.Points[1].Y);
                    sceneItemTextPercent.Points = new[] { pointTextPercentF0, pointTextPercentF1 };
                    sceneItemTextPercent.Text = ((int)series.Points[letterNo].Value).ToString() + "%";
                    listExtraScene.Add(sceneItemTextPercent);
                    letterNo++;
                }
                else if (Chart.GetType() == typeof(BarChart) && ShowPercentage &&
                         !string.IsNullOrEmpty(sceneItem.Tag) &&
                         sceneItem.Type == SceneItem.ItemType.Text)
                {
                    //Percent value
                    var sceneItemTextPercent = new SceneItem();
                    sceneItemTextPercent.Type = SceneItem.ItemType.Text;
                    sceneItemTextPercent.LabelStyle.Brush = new SolidBrush(Color.DimGray);
                    sceneItemTextPercent.LabelStyle.Font = new Font("Tahoma", 11);
                    var pointTextPercentF0 = new PointF(sceneItem.Points[0].X - 17,
                                                        sceneItem.Points[0].Y + 15);
                    var pointTextPercentF1 = new PointF(sceneItem.Points[1].X + 50,
                                                        sceneItem.Points[1].Y);
                    sceneItemTextPercent.Points = new[] { pointTextPercentF0, pointTextPercentF1 };
                    sceneItemTextPercent.Text = ((int)series.Points[letterNo].Value).ToString() +
                                                "%";
                    listExtraScene.Add(sceneItemTextPercent);
                    letterNo++;
                }
            }
            //Custom x-axis % label
            var customSceneLabels = sceneItems.Where(sceneItem => sceneItem.Type == SceneItem.ItemType.Text && (RenderRealValue && sceneItem.Tag.StartsWith("R_") || sceneItem.LabelStyle.FormatString == "0\\%")).ToList();
            //Get color index in array
            int pointNo = 0;
            if (Chart.GetType() == typeof(BarChart))
            {
                foreach (var sceneItem in customSceneLabels)
                {
                    var sStep = Chart.PlotRect.Height / Chart.Points.Count;
                    var oSize0 = Chart.MeasureString(sceneItem.Text, sceneItem.LabelStyle.Font,
                        Chart.ClientRect.Width / 2, sStep);
                    if (PointValuePrefix != null)
                    {
                        if (pointNo < PointValuePrefix.Length)
                            sceneItem.Text = PointValuePrefix[pointNo] + sceneItem.Text;
                    }
                    if (PointValueSuffix != null)
                    {
                        if (pointNo < PointValueSuffix.Length)
                            sceneItem.Text = sceneItem.Text + PointValueSuffix[pointNo];
                    }
                    var oSize = Chart.MeasureString(sceneItem.Text, sceneItem.LabelStyle.Font,
                        Chart.ClientRect.Width / 2, sStep);
                    var newX = sceneItem.Points[0].X - oSize.Width - 5;
                    if (sceneItem.Points[0].X == Chart.PlotRect.Right)
                    {
                        sceneItem.Points[0].X = Chart.PlotRect.Right - oSize.Width;
                    }
                    else if (sceneItem.Points[0].X + oSize.Width < Chart.PlotRect.Right)
                    {
                        sceneItem.Points[0].X = newX > Chart.PlotRect.Left
                            ? newX
                            : Chart.PlotRect.Left + 5;
                    }
                    else
                    {
                        var sceneItemChartPoint =
                            sceneItems.FirstOrDefault(
                                p =>
                                    p.Tag.StartsWith("Chartpoint") && p.Tag.EndsWith("Point:" + pointNo));
                        if (sceneItemChartPoint.Points[0].X + sceneItemChartPoint.Points[1].X >
                            sceneItem.Points[0].X - 5)
                            sceneItem.Points[0].X = newX + oSize0.Width + 10;
                        else
                            sceneItem.Points[0].X = newX;
                    }

                    if (newX <= Chart.PlotRect.Left)
                        sceneItem.LabelStyle.Brush = new SolidBrush(Color.Black);
                    pointNo++;
                }
            }
            if (PointValuePrefix != null || PointValueSuffix != null)
            {
                var borderLeft1 = new PointF { X = Chart.PlotRect.Left, Y = Chart.PlotRect.Top };
                var borderLeft2 = new PointF { X = Chart.PlotRect.Left, Y = Chart.PlotRect.Bottom };
                PointF[] points = new PointF[2];
                points[0] = borderLeft1;
                points[1] = borderLeft2;
                var sceneItem = new SceneItem(SceneItem.ItemType.Line, new BaseCXChart.Style(new Pen(Color.Gray), null, null), points, null, "ChartAreaBorderLeft");
                listExtraScene.Add(sceneItem);

                var borderBottom1 = new PointF { X = Chart.PlotRect.X, Y = Chart.PlotRect.Bottom };
                var borderBottom2 = new PointF { X = Chart.PlotRect.Right, Y = Chart.PlotRect.Bottom };
                points = new PointF[2];
                points[0] = borderBottom1;
                points[1] = borderBottom2;
                var sceneItem1 = new SceneItem(SceneItem.ItemType.Line, new BaseCXChart.Style(new Pen(Color.Gray), null, null), points, null, "ChartAreaBorderLeft");
                listExtraScene.Add(sceneItem1);

            }
            //Re-Order bar-sceneitems
            //Find all the bar-sceneitems (the ones with a Tag starting with ChartPoint) and move them to the start of the list (or at least before the lines are drawn).
            var sceneItemLine = sceneItems.FirstOrDefault(p => p.Type == SceneItem.ItemType.Line);
            if (sceneItemLine != null)
            {
                foreach (var listBarSceneItem in listBarSceneItems)
                {
                    sceneItems.Remove(listBarSceneItem.Value);
                }
                sceneItems.InsertRange(sceneItems.IndexOf(sceneItemLine), listBarSceneItems.Select(p => p.Value).ToList());
            }
            //Add Extra Scene
            if (listExtraScene.Any())
            {
                listExtraScene.ForEach(sceneItems.Add);
            }

            Chart.RenderImage(sceneItems).Save(memoryStream, ImageFormat.Png);
        }
        else
        {
            Chart.SaveImage(ref memoryStream, ImageFormat.Png);
        }
        return memoryStream;
    }
                 
    ////public void ToWord(DocumentBuilder documentBuilder, double dPw, ICollection<DocumentItemDto> docItems)
    ////{
    ////    var oBitmap = Chart != null ? Chart.RenderImage() : null;
    ////    if (oBitmap == null) return;
    ////    documentBuilder.StartTable();
    ////    documentBuilder.InsertCell();
    ////    documentBuilder.CellFormat.ClearFormatting();
    ////    documentBuilder.CellFormat.Width = dPw;
    ////    documentBuilder.CellFormat.Borders.LineStyle = LineStyle.None;
    ////    var oShape = documentBuilder.InsertImage(oBitmap);
    ////    oShape.WrapType = Aspose.Words.Drawing.WrapType.Inline;
    ////    oShape.BehindText = false;
    ////    documentBuilder.EndRow();
    ////    documentBuilder.EndTable();
    ////    documentBuilder.InsertBreak(BreakType.LineBreak);
    ////}

    ////public void ToPpt(Presentation presentation, ref Slide slide, ref double yPos, double yStartPos)
    ////{
    ////    if (Chart == null) return;
    ////    var chartScale = 6;
    ////    var bmp = Chart.RenderImage();
    ////    var pic = new Picture(presentation, bmp);
    ////    var picId = presentation.Pictures.Add(pic);

    ////    while (pic.Image.Height * chartScale > 3500)
    ////    {
    ////        chartScale -= 1;
    ////    }
    ////    if (yPos + bmp.Height * chartScale > 3500)
    ////    {
    ////        yPos = yStartPos;
    ////        slide = presentation.AddEmptySlide();
    ////    }
    ////    var slideWidth = slide.Background.Width;
    ////    //var slideHeight = slide.Background.Height;
    ////    var pictureWidth = pic.Image.Width * chartScale;
    ////    var pictureHeight = pic.Image.Height * chartScale;
    ////    var pictureFrameWidth = Convert.ToInt32(slideWidth / 2 - pictureWidth / 2);
    ////    //var pictureFrameHeight = Convert.ToInt32(slideHeight / 2 - pictureHeight / 2);

    ////    slide.Shapes.AddPictureFrame(picId, pictureFrameWidth, (int)yPos, pictureWidth, pictureHeight);
    ////    yPos += pictureHeight;
    ////    yPos += 50;
    ////}

    #endregion
}
