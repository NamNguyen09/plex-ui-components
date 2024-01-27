using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Web;
using Aspose.Slides;
using Aspose.Words;
using cxPlatform.Crosscutting.Common;
using cxPlatform.Data.Reporting.Documents;
using Plex.UIComponents.Dtos;

namespace Plex.UIComponents.UIComponents
{
    /// <summary>
    /// Class Image.
    /// </summary>
    [Serializable]
    public class Image : BaseUIComponent
    {
        #region Properties
        /// <summary>
        /// The URL
        /// </summary>
        public string Url = string.Empty;
        /// <summary>
        /// The alt
        /// </summary>
        public string Alt = string.Empty;
        /// <summary>
        /// The width
        /// </summary>
        public int Width;
        /// <summary>
        /// The height
        /// </summary>
        public new int Height;
        /// <summary>
        /// The span tag
        /// </summary>
        public bool SpanTag;
        /// <summary>
        /// The span class
        /// </summary>
        public string SpanClass = string.Empty;
        /// <summary>
        /// The bitmap
        /// </summary>
        public Bitmap Bitmap;
        /// <summary>
        /// The attributes
        /// </summary>
        public Dictionary<string, string> Attributes = new Dictionary<string, string>();
        #endregion


        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Image"/> class.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="alt">The alt.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="bitmap">The bitmap.</param>
        public Image(string url = "", string alt = "", int width = 0, int height = 0, Bitmap bitmap = null)
        {
            Url = url;
            Alt = alt;
            Width = width;
            Height = height;
            Bitmap = bitmap;
        }
        #endregion


        #region Methods
        /// <summary>
        /// To the word.
        /// </summary>
        /// <param name="documentBuilder">The document builder.</param>
        /// <param name="pageWidth">Width of the page.</param>
        /// <param name="docItems">The document items.</param>
        public void ToWord(DocumentBuilder documentBuilder, double pageWidth, ICollection<DocumentItemDto> docItems)
        {
            //Todo: Choose correct image folders
            if (Bitmap != null)
            {
                documentBuilder.InsertImage(Bitmap);
            }
            else
            {
                var slPath = Utilities.GetBaseUrl();
                documentBuilder.InsertHtml("<base href='" + slPath + "'/>" + ImageToHTML());
            }


        }
        /// <summary>
        /// Images to HTML.
        /// </summary>
        /// <returns>System.String.</returns>
        public string ImageToHTML()
        {
            var oSb = new System.Text.StringBuilder();
            if (SpanTag)
                oSb.Append("<span " + (!string.IsNullOrEmpty(SpanClass) ? "class='" + SpanClass + "'" : "" + " >"));
            oSb.Append("<img src='" + Url + "'");
            if (Alt.Length > 0)
                oSb.Append(" alt='" + Alt.Replace("'", "&apos;") + "' title='" + Alt.Replace("'", "&apos;") + "'");
            if (Width > 0)
                oSb.Append(" width='" + Width + "'");
            if (Height > 0)
                oSb.Append(" height='" + Height + "'");

            foreach (var item in Attributes)
            {
                oSb.Append(" " + item.Key + "='" + item.Value.Replace("'", "&apos;") + "'");
            }
            oSb.Append(" />");

            if (SpanTag)
                oSb.Append("</span>");
            return oSb.ToString();
        }

        /// <summary>
        /// To the PPT.
        /// </summary>
        /// <param name="presentation">The presentation.</param>
        /// <param name="slide">The slide.</param>
        /// <param name="yPos">The y position.</param>
        /// <param name="pageWidth">Width of the page.</param>
        /// <param name="yStartPosition">The y start position.</param>
        public void ToPpt(Presentation presentation, ref Slide slide, ref double yPos, double pageWidth, double yStartPosition)
        {
            if (Bitmap == null)
            {
                Bitmap = LoadImageFromURL(Url);
            }
            if (Bitmap == null) return;

            //var stream = new System.IO.MemoryStream();
            var pic = new Picture(presentation, Bitmap);
            var picId = presentation.Pictures.Add(pic);
            var pictureFrameWidth = Width;
            var pictureFrameHeight = Height;

            if (yPos + Height > 3300)
            {
                yPos = yStartPosition;
                slide = presentation.AddEmptySlide();
            }

            slide.Shapes.AddPictureFrame(picId, pictureFrameWidth, (int)yStartPosition, pictureFrameWidth, pictureFrameHeight);
            yPos += pictureFrameHeight;
            yPos += 50;
        }

        /// <summary>
        /// To the PPT cell.
        /// </summary>
        /// <param name="presentation">The presentation.</param>
        /// <param name="slide">The slide.</param>
        /// <param name="pptTable">The PPT table.</param>
        /// <param name="rowIndex">Index of the row.</param>
        /// <param name="columnIndex">Index of the column.</param>
        public void ToPptCell(Presentation presentation, Slide slide, Aspose.Slides.Table pptTable, int rowIndex, int columnIndex)
        {
            var filePath = HttpContext.Current.Server.MapPath(Url.Replace(Utilities.GetBaseUrl(), "~"));
            if (!File.Exists(filePath)) return;

            var cell = pptTable.GetCell(columnIndex, rowIndex);
            if (cell == null || cell.TextFrame == null) return;

            cell.TextFrame.Width = 160;
            cell.TextFrame.Height = 160;
            cell.TextFrame.FillFormat.Type = FillType.Picture;
            cell.TextFrame.FillFormat.PictureId = presentation.Pictures.Add(new Picture(presentation, filePath));
        }

        /// <summary>
        /// Removes the js event.
        /// </summary>
        public override void RemoveJsEvent()
        {
            if (Attributes.ContainsKey("onclick"))
                Attributes.Remove("onclick");
        }

        /// <summary>
        /// Removes the tooltip.
        /// </summary>
        public override void RemoveTooltip()
        {
            if (Attributes.ContainsKey("title"))
            {
                Attributes.Remove("title");
            }
        }
        #endregion


        #region Helpers
        /// <summary>
        /// Loads the image from URL.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns>Bitmap.</returns>
        private Bitmap LoadImageFromURL(string url)
        {
            var filePath = HttpContext.Current.Server.MapPath(url.Replace(Utilities.GetBaseUrl(), "~"));
            if (!File.Exists(filePath)) return null;

            const int bytestoread = 10000;
            var myRequest = System.Net.WebRequest.Create(filePath);
            var myResponse = myRequest.GetResponse();
            var receiveStream = myResponse.GetResponseStream();
            if (receiveStream != null)
            {
                var br = new BinaryReader(receiveStream);
                var memstream = new MemoryStream();
                var bytebuffer = new byte[bytestoread];
                var bytesRead = br.Read(bytebuffer, 0, bytestoread);
                while (bytesRead > 0)
                {
                    memstream.Write(bytebuffer, 0, bytesRead);
                    bytesRead = br.Read(bytebuffer, 0, bytestoread);
                }
                var img = new Bitmap(memstream);
                return img;
            }
            return null;
        }
        #endregion
    }
}
