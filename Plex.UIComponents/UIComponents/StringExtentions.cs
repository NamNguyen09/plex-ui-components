using System.Drawing;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Plex.UIComponents.UIComponents;

public static class StringExtentions
{
    public static string RemoveHtmlTags(this string value)
    {
        string functionReturnValue;
        value = System.Net.WebUtility.HtmlDecode(value);

        if (!string.IsNullOrEmpty(value) && (value.Contains("<") || value.Contains("&")))
        {
            var objRegExp = new Regex("<br\\s{0,1}/{0,1}>", RegexOptions.IgnoreCase);
            value = objRegExp.Replace(value, Environment.NewLine);

            objRegExp = new Regex("<style([\\s\\S]+?)</style>", RegexOptions.IgnoreCase);
            value = objRegExp.Replace(value, "");

            objRegExp = new Regex("(<(.|\\n)+?>|</span>)", RegexOptions.IgnoreCase);
            value = objRegExp.Replace(value, "");

            value = value.Replace("&lt;", "<");
            value = value.Replace("&nbsp;", " ");
            value = value.Replace("&quot;", "\"");
            functionReturnValue = value;
        }
        else
        {
            functionReturnValue = value;
        }

        return functionReturnValue;
    }
    public static string RemoveAllTags(this string sInput)
    {
        sInput = Regex.Replace(sInput, "<style>(.|\n)*?</style>", string.Empty);
        sInput = Regex.Replace(sInput, @"(title=[^""'][^>]*)|(title=""[^""]*"")|(title='[^']*')", string.Empty);
        sInput = Regex.Replace(sInput, @"<xml>(.|\n)*?</xml>", string.Empty); // remove all <xml></xml> tags and anything inbetween.
        return Regex.Replace(sInput, @"<[^>]*>", string.Empty);
    }

    /// <summary>
    /// Remove html tags after replace new line tag
    /// </summary>
    /// <param name="sInput"></param>
    /// <returns></returns>
    public static string RemoveTags(this string sInput)
    {
        if (string.IsNullOrEmpty(sInput))
        {
            return string.Empty;
        }
        var sOutput = sInput;
        var objRegExp = new Regex("</{0,1}b>", RegexOptions.IgnoreCase);
        sOutput = objRegExp.Replace(sOutput, string.Empty);

        objRegExp = new Regex("</{0,1}br\\s{0,1}/{0,1}>", RegexOptions.IgnoreCase);
        sOutput = objRegExp.Replace(sOutput, " ");

        return sOutput.RemoveHtmlTags();
    }

    /// <summary>
    /// Removes the HTML tags PPT.
    /// </summary>
    /// <param name="strHtml">The STR HTML.</param>
    /// <returns></returns>
    public static string RemoveHtmlTagsPpt(this string strHtml)
    {
        string functionReturnValue;
        if (string.IsNullOrEmpty(strHtml))
        {
            return string.Empty;
        }

        strHtml = Regex.Replace(strHtml, @"(title=[^""'][^>]*)|(title=""[^""]*"")|(title='[^']*')", string.Empty);
        strHtml = System.Net.WebUtility.HtmlDecode(strHtml);
        if (strHtml.Length > 0 && (strHtml.Contains("<") || strHtml.Contains("&")))
        {
            /*
             * vbCr = "\r"
             * vbLf = "\n"
             * vbCrLf = "\r\n"
             */
            const string vbLf = "\n";
            strHtml = strHtml.Replace("\r\n", vbLf);
            var index1 = strHtml.IndexOf("<style>");
            var index2 = strHtml.IndexOf("</style>");
            if (index1 >= 0 && index2 >= 0)
                strHtml = strHtml.Remove(index1, index2 - index1 + 8);
            strHtml = strHtml.Replace("<BR>", vbLf);
            strHtml = strHtml.Replace("<br>", vbLf);
            strHtml = strHtml.Replace("<BR>", vbLf);

            strHtml = strHtml.Replace("<br/>", vbLf);
            strHtml = strHtml.Replace("<BR/>", vbLf);
            strHtml = strHtml.Replace("<br ", vbLf + "<br ");
            strHtml = strHtml.Replace("<BR ", vbLf + "<BR ");

            strHtml = strHtml.Replace("<li>", vbLf + "* <li>");
            strHtml = strHtml.Replace("<LI>", vbLf + "* <LI>");
            strHtml = strHtml.Replace("<li ", vbLf + "* <li ");
            strHtml = strHtml.Replace("<LI ", vbLf + "* <LI ");

            strHtml = strHtml.Replace("<ul>", vbLf + "<ul>");
            strHtml = strHtml.Replace("<UL>", vbLf + "<UL>");
            strHtml = strHtml.Replace("<ul ", vbLf + "<ul ");
            strHtml = strHtml.Replace("<UL ", vbLf + "<UL ");
            strHtml = strHtml.Replace("</ul>", " </ul>" + vbLf + vbLf);
            strHtml = strHtml.Replace("</UL>", " </UL>" + vbLf + vbLf);

            strHtml = strHtml.Replace("<ol>", vbLf + "<ol>");
            strHtml = strHtml.Replace("<OL>", vbLf + "<OL>");
            strHtml = strHtml.Replace("<ol ", vbLf + "<ol ");
            strHtml = strHtml.Replace("<OL ", vbLf + "<OL ");
            strHtml = strHtml.Replace("</ol>", " </ol>" + vbLf + vbLf);
            strHtml = strHtml.Replace("</OL>", " </OL>" + vbLf + vbLf);

            strHtml = strHtml.Replace("<hr>", vbLf + "----------------------------------------------------------------------" + vbLf);
            strHtml = strHtml.Replace("<HR>", vbLf + "----------------------------------------------------------------------" + vbLf);
            strHtml = strHtml.Replace("<hr/>", vbLf);
            strHtml = strHtml.Replace("<HR/>", vbLf);
            strHtml = strHtml.Replace("<hr ", vbLf + "<hr ");
            strHtml = strHtml.Replace("<HR ", vbLf + "<HR ");

            strHtml = strHtml.Replace("<table>", vbLf);
            strHtml = strHtml.Replace("<TABLE>", vbLf);
            strHtml = strHtml.Replace("<table ", vbLf + "<table ");
            strHtml = strHtml.Replace("<TABLE ", vbLf + "<TABLE ");

            strHtml = strHtml.Replace("<tr>", vbLf);
            strHtml = strHtml.Replace("<TR>", vbLf);
            strHtml = strHtml.Replace("<tr ", vbLf + "<tr ");
            strHtml = strHtml.Replace("<TR ", vbLf + "<TR ");

            strHtml = strHtml.Replace("</table>", vbLf + vbLf);
            strHtml = strHtml.Replace("</TABLE>", vbLf + vbLf);

            strHtml = strHtml.Replace("<div>", vbLf + vbLf + "<div>");
            strHtml = strHtml.Replace("<DIV>", vbLf + vbLf + "<DIV>");
            strHtml = strHtml.Replace("<div ", vbLf + vbLf + "<div ");
            strHtml = strHtml.Replace("<DIV ", vbLf + vbLf + "<DIV ");

            strHtml = strHtml.Replace("</div>", " </div>" + vbLf + vbLf);
            strHtml = strHtml.Replace("</DIV>", " </DIV>" + vbLf + vbLf);

            strHtml = strHtml.Replace("<p>", vbLf + vbLf + "<p>");
            strHtml = strHtml.Replace("<P>", vbLf + vbLf + "<P>");
            strHtml = strHtml.Replace("<p ", vbLf + vbLf + "<p ");
            strHtml = strHtml.Replace("<P ", vbLf + vbLf + "<P ");

            strHtml = strHtml.Replace("</p>", " </p>" + vbLf + vbLf);
            strHtml = strHtml.Replace("</P>", " </P>" + vbLf + vbLf);

            //Replace all HTML tag matches with the empty string
            var objRegExp = new Regex("<(.|\\n)+?>", RegexOptions.IgnoreCase);
            var strOutput = objRegExp.Replace(strHtml, string.Empty);
            //var strOutput = strHtml;

            strOutput = strOutput.Replace("</span>", string.Empty);
            strOutput = strOutput.Replace("&nbsp;", " ");
            strOutput = strOutput.Replace("&gt;", ">");
            strOutput = strOutput.Replace("&lt;", "<");
            strOutput = strOutput.Replace("&amp;", "&");
            strOutput = strOutput.Replace("&quot;", "\"");

            var objRegExpLeadingLfs = new Regex("^\\s+", RegexOptions.None);
            strOutput = objRegExpLeadingLfs.Replace(strOutput, string.Empty);

            var objRegExpEndingLfs = new Regex("\\s+$", RegexOptions.None);
            strOutput = objRegExpEndingLfs.Replace(strOutput, string.Empty);

            var objRegExpLineLeadingWhitepace = new Regex("^[\\t ]+", RegexOptions.Multiline);
            strOutput = objRegExpLineLeadingWhitepace.Replace(strOutput, string.Empty);

            var objRegExpMiddleWhitepace = new Regex("[\\t ][\\t ]+", RegexOptions.None);
            strOutput = objRegExpMiddleWhitepace.Replace(strOutput, " ");

            var objRegExpMiddleLfs = new Regex("\\n\\n+", RegexOptions.None);
            strOutput = objRegExpMiddleLfs.Replace(strOutput, vbLf + vbLf);

            functionReturnValue = strOutput;
        }
        else
        {
            functionReturnValue = strHtml;
        }

        return functionReturnValue;
    }

    /// <summary>
    /// Removes the text area.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns></returns>
    public static string RemoveTextArea(this string value)
    {
        if (value.Contains("<textarea"))
        {
            value = Regex.Replace(value, "<textarea(.|\n)*?>", "");
            value = Regex.Replace(value, "</textarea>", "");
        }

        return value;
    }

    /// <summary>
    /// Removes the content of the HTML.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns></returns>
    public static string RemoveHtmlContent(this string value)
    {
        if (value.Contains("<") && value.Contains(">"))
        {
            return Regex.Replace(value, @"<[^>]*>", String.Empty);
        }

        return value;
    }

    /// <summary>
    /// Returns a string containing a specified number of characters from the left side of a string.
    /// </summary>
    /// <param name="value">original string</param>
    /// <param name="length">number of character</param>
    /// <returns></returns>
    public static string Left(this string value, int length)
    {
        return (string.IsNullOrEmpty(value) || length < 0 || length > value.Length)
            ? value
            : value.Substring(0, length);
    }


    public static string RemoveHtmlTitile(this string strHtml)
    {
        if (string.IsNullOrEmpty(strHtml))
        {
            return string.Empty;
        }

        strHtml = Regex.Replace(strHtml, @"(title=[^""'][^>]*)|(title=""[^""]*"")|(title='[^']*')", string.Empty);
        strHtml = System.Net.WebUtility.HtmlDecode(strHtml);
        return strHtml;
    }
    public static string ReplaceSpecialHtmlCharacter(this string value, bool pIsReverse = false)
    {
        var pHtml = new StringBuilder();
        pHtml.Append(value);

        if (pIsReverse == false)
        {
            pHtml.Replace("\n", string.Empty);
            pHtml.Replace("\r", string.Empty);
            pHtml.Replace("\t", string.Empty);
            pHtml.Replace("&", "&amp;");
            pHtml.Replace("  ", " &nbsp;");
            pHtml.Replace("\"", "&quot;");
            pHtml.Replace("`", "&lsquo;");
            pHtml.Replace("'", "&rsquo;");
            pHtml.Replace("©", "&copy;");
            pHtml.Replace(">", "&gt;");
            pHtml.Replace("<", "&lt;");
            pHtml.Replace("®", "&reg;");
        }
        else
        {
            pHtml.Replace("&amp;", "&");
            pHtml.Replace("&nbsp;", " ");
            pHtml.Replace("&quot;", "\"");
            pHtml.Replace("&lsquo;", "`");
            pHtml.Replace("&rsquo;", "'");
            pHtml.Replace("&copy;", "©");
            pHtml.Replace("&gt;", ">");
            pHtml.Replace("&lt;", "<");
            pHtml.Replace("&reg;", "®");

            pHtml.Replace("&#10;", "<br />");
            pHtml.Replace(Environment.NewLine, "<br />");
            pHtml.Replace("\r\n", "<br />");
            pHtml.Replace("\n", "<br />");

        }
        var result = pHtml.ToString();
        if (value.Contains("javascript:void"))
        {
            result = result.RemoveHtmlTagAndAttribute("a");
        }
        return result;
    }

    public static string RemoveHtmlTagAndAttribute(this string html, string tagName)
    {
        var validAttributeOrTagNameRegEx =
                   new Regex(@"^\w+$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        const string strRemoveHtmlTagRegex = "</?{0}[^<]*?>";
        if (validAttributeOrTagNameRegEx.IsMatch(tagName))
        {
            var reg = new Regex(string.Format(strRemoveHtmlTagRegex, tagName),
               RegexOptions.IgnoreCase);
            return reg.Replace(html, "");
        }
        throw new ArgumentException("Not a valid HTML tag name", tagName);
    }

    /// <summary>
    /// Remove all the extra white spacing or comment blocks in your HTML string
    /// </summary>
    /// <param name="pHtml"></param>
    /// <returns></returns>
    public static string OptimizerHtml(this string pHtml)
    {
        pHtml = pHtml.Trim();

        // from ==> hello /* world */ oh " '\" // ha/*i*/" and // bai
        // to ==> hello  oh " '\" // ha/*i*/" and
        //const string stripCommentRegex = @"(@(?:""[^""]*"")+|""(?:[^""\n\\]+|\\.)*""|'(?:[^'\n\\]+|\\.)*')|//.*|/\*(?s:.*?)\*/";
        //pHtml = Regex.Replace(pHtml, stripCommentRegex, "$1");

        //Remove comment // in javascript
        const string stripCommentRegex = @"\r*\n\t*\s*//.+\r*\n";
        pHtml = Regex.Replace(pHtml, stripCommentRegex, string.Empty);

        //Newline or tab character
        pHtml = Regex.Replace(pHtml, @"\n|\t", " ");

        pHtml = Regex.Replace(pHtml, @">\s+<", "> <");

        //Whitespace, at least 2 repetitions
        pHtml = Regex.Replace(pHtml, @"\s{2,}", " ");

        //Remove comments
        pHtml = Regex.Replace(pHtml, @"<!--([a-z\s0-9]+)-->", "");

        return pHtml.RemoveAllCommentBlock("<%--", "--%>");
    }

    /// <summary>
    /// Remove all comment block 
    /// </summary>
    /// <param name="pHtml"></param>
    /// <param name="startBlock"></param>
    /// <param name="endBlock"></param>
    /// <returns></returns>
    private static string RemoveAllCommentBlock(this string pHtml, string startBlock, string endBlock)
    {
        var startIndex = 0;
        var startBlockLen = startBlock.Length;
        var endBlockLen = endBlock.Length;
        while ((startIndex = pHtml.IndexOf(startBlock, startIndex, StringComparison.Ordinal)) >= 0)
        {
            //preserve = p_HTML.Length > startIndex + 4 && p_HTML[startIndex + 4] == '!';
            var endIndex = pHtml.IndexOf(endBlock, startIndex + startBlockLen, StringComparison.Ordinal) + endBlockLen;
            if (endIndex < endBlockLen)
            {
                pHtml = pHtml.Substring(0, startIndex);
            }
            else
                if (endIndex >= startIndex + startBlockLen + endBlockLen)
            {
                pHtml = pHtml.Substring(0, startIndex) + pHtml.Substring(endIndex);
            }
        }
        return pHtml;
    }
    public static bool IsDate(this string dateString, ref DateTime date, CultureInfo culture)
    {
        return DateTime.TryParse(dateString, culture, DateTimeStyles.None, out date) && date.Year >= 1900;
    }
    public static string FormatNumber(this string value, string format, CultureInfo culture)
    {
        double numValue;
        try
        {
            numValue = double.Parse(value, NumberStyles.AllowDecimalPoint, culture);
        }
        catch (Exception)
        {
            return string.Empty;
        }
        return numValue.ToString(format, culture);
    }
    public static bool CheckNumberBound(this string value, string format, CultureInfo culture, string min, string max)
    {
        var result = false;
        try
        {
            var numValue = double.Parse(value, NumberStyles.AllowDecimalPoint, culture);
            //Minimum
            if (!string.IsNullOrEmpty(min))
            {
                var minValue = double.Parse(min, NumberStyles.AllowDecimalPoint, culture);
                result = numValue >= minValue;
            }

            if (string.IsNullOrEmpty(min) || (!string.IsNullOrEmpty(min) && result))
            {
                //Maximum
                if (!string.IsNullOrEmpty(max))
                {
                    var maxValue = double.Parse(max, NumberStyles.AllowDecimalPoint, culture);
                    result = numValue <= maxValue;
                }
            }
            return result;
        }
        catch (Exception)
        {
            return false;
        }
    }

    /// <summary>
    /// Try to use tranparent color if possible from color code, if not - return original color.
    /// </summary>
    /// <param name="inColor"></param>
    /// <param name="backColor"></param>
    /// <returns></returns>
    public static string TryGetARGBtoRGB(string inColor, string backColor = "#FFFFFF")
    {
        if (inColor.StartsWith("#") && inColor.Length == 9)
        {
            Color bgColor = ColorTranslator.FromHtml(backColor);
            Color color = ColorTranslator.FromHtml(inColor);

            var a = color.A / 255.0;

            var r1 = color.R / 255.0;
            var g1 = color.G / 255.0;
            var b1 = color.B / 255.0;

            var r2 = bgColor.R / 255.0;
            var g2 = bgColor.G / 255.0;
            var b2 = bgColor.B / 255.0;

            var r3 = ((1 - a) * r2) + (a * r1);
            var g3 = ((1 - a) * g2) + (a * g1);
            var b3 = ((1 - a) * b2) + (a * b1);

            var result = Color.FromArgb(Convert.ToInt32(r3 * 255), Convert.ToInt32(g3 * 255),
                                                                        Convert.ToInt32(b3 * 255));
            return ColorTranslator.ToHtml(result);
        }
        return inColor;
    }

    /// <summary>
    /// Calculates the width of text.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <param name="isBreakLine">if set to <c>true</c> [is break line].</param>
    /// <param name="minWidth">The minimum width.</param>
    /// <returns>
    /// Width of the text
    /// </returns>
    public static int CalcWidth(this string text, bool isBreakLine = true, int minWidth = 32)
    {
        if (isBreakLine)
        {
            return GetMaxWidth(text, minWidth);
        }

        const int measurementUncertainty = 13;
        using (Graphics graphics = Graphics.FromImage(new Bitmap(1, 1)))
        {
            var size = graphics.MeasureString(text, new Font("Tahoma", 11, FontStyle.Bold, GraphicsUnit.Pixel));
            var result = (int)size.Width + measurementUncertainty;
            return result < minWidth ? minWidth : result;
        }
    }

    private static int GetMaxWidth(string text, int minWidth)
    {
        var words = text.Split(' ');
        var tmpWidth = 0;
        var actualWidth = 0;

        foreach (var word in words)
        {
            var width = CalcWidth(word, false, minWidth);
            actualWidth = width > tmpWidth ? width : tmpWidth;

            tmpWidth = width;
        }

        return actualWidth;
    }
    public static Dictionary<int, string> StoreStringParamToDict(string param)
    {
        var result = new Dictionary<int, string>();
        var values = param.Split(',');
        foreach (var item in values)
        {
            var info = item.Split('_');
            int key = 0;
            if (info.Length > 1)
            {
                int.TryParse(info[0], out key);
                if (!result.ContainsKey(key))
                    result.Add(key, info[1]);
            }
        }
        return result;
    }
    public static Dictionary<string, string> StoreStringParamToStringDict(string param)
    {
        var result = new Dictionary<string, string>();
        var values = param.Split(',');
        foreach (var item in values)
        {
            var info = item.Split('_');
            if (info.Length > 1)
            {
                if (!result.ContainsKey(info[0]))
                    result.Add(info[0], info[1]);
            }
        }
        return result;
    }
    public static string ToUppercaseFirst(this string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return string.Empty;
        }

        return char.ToUpper(input[0]) + input.Substring(1);
    }

    public static DateTime ToDateTime(this string value, IFormatProvider provider)
    {
        const DateTimeStyles style = DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeUniversal;

        var result = DateTime.Parse(value, provider, style);
        return result;
    }

    public static double ToDouble(this string value, IFormatProvider provider)
    {
        const NumberStyles style = NumberStyles.Number | NumberStyles.AllowDecimalPoint;

        var result = double.Parse(value, style, provider);
        return result;
    }

    public static string MakeValidFileName(this string fileName, string replacingValue = "-")
    {
        string invalidChars = Regex.Escape(new string(System.IO.Path.GetInvalidFileNameChars()));
        string invalidRegStr = string.Format(@"([{0}]*\.+$)|([{0}]+)", invalidChars);

        return Regex.Replace(fileName, invalidRegStr, replacingValue);
    }

    public static string GetFileExtension(this string fileName)
    {
        string ext = string.Empty;
        int fileExtPos = fileName.LastIndexOf(".", StringComparison.Ordinal);
        if (fileExtPos >= 0)
            ext = fileName.Substring(fileExtPos, fileName.Length - fileExtPos);

        return ext;
    }

    public static string TruncateMiddle(this string value, int maxChars, bool atWord = true, string ellipsis = "...")
    {
        if (value == null || value.Length <= maxChars)
            return value;

        var l1 = maxChars / 2;
        var l2 = maxChars - l1;
        var text1 = value.Substring(0, l1);
        var text2 = value.Substring(value.Length - l2, l2);

        if (atWord)
        {
            var alternativeCutOffs = new List<char> { ' ', ',', '.', '?', '/', ':', ';', '\'', '\"', '\'', '-' };
            var lastSpace = text1.LastIndexOf(' ');
            if (lastSpace != -1 && (value.Length >= maxChars + 1 && !alternativeCutOffs.Contains(value.ToCharArray()[maxChars])))
                text1 = text1.Remove(lastSpace);

            char[] alternativeCutOffs2 = { ' ', '?', '/', ':', ';', '\'', '\"', '\'', '-' };
            text2 = text2.TrimStart(alternativeCutOffs2);
        }

        return text1 + ellipsis + text2;
    }
    public static string GetForeColorClass(this string sHexCode)
    {
        var color = ColorTranslator.FromHtml(sHexCode);
        var resColor = ColorTranslator.FromHtml("#000000");
        AdjustForeColorBrightnessForBackColor(ref resColor, color, 1);
        var cssClass = ColorTranslator.ToHtml(resColor) == "#000000" ? "foreColorDark" : "foreColorBright";

        return cssClass;
    }

    public static Color GetForeColorFormColor(Color color)
    {
        var hexColor = ColorTranslator.ToHtml(color);
        var newHexColor = GetForeColor(hexColor);

        return ColorTranslator.FromHtml(newHexColor);
    }
    public static string GetForeColor(this string hexCode)
    {
        Color color;
        try
        {
            color = ColorTranslator.FromHtml(hexCode);
        }
        catch (Exception)
        {
            throw new Exception("The color string is not correct for hex-color format: " + hexCode);
        }
        var resColor = ColorTranslator.FromHtml("#000000");
        AdjustForeColorBrightnessForBackColor(ref resColor, color, 1);
        return ColorTranslator.ToHtml(resColor);
    }
    public static void AdjustForeColorBrightnessForBackColor(ref Color foreColor, Color backColor, float prefContrastLevel)
    {
        var fBrightness = foreColor.GetBrightness();
        var bBrightness = backColor.GetBrightness();
        var curContrast = fBrightness - bBrightness;
        //var delta = prefContrastLevel - Convert.ToSingle(Math.Abs(curContrast));

        if (Convert.ToSingle(Math.Abs(curContrast)) >= prefContrastLevel) return;
        if (bBrightness < 0.52f)
        {
            fBrightness = bBrightness + prefContrastLevel;
            if (fBrightness > 1f)
            {
                fBrightness = 1f;
            }
        }
        else
        {
            fBrightness = bBrightness - prefContrastLevel;
            if (fBrightness < 0f)
            {
                fBrightness = 0f;
            }
        }

        float newr = 0;
        float newg = 0;
        float newb = 0;
        ConvertHsbtoRGB(foreColor.GetHue(), foreColor.GetSaturation(), fBrightness, ref newr, ref newg, ref newb);

        foreColor = Color.FromArgb(foreColor.A, (int)(Math.Floor((newr * 255f))), (int)(Math.Floor((newg * 255f))),
                                   (int)(Math.Floor((newb * 255f))));
    }
    public static string GetColorAdjustBrightness(this string hexCode, double factor)
    {
        var color = ColorTranslator.FromHtml(hexCode);
        var resColor = Color.FromArgb(color.A, Convert.ToInt32(color.R * factor), Convert.ToInt32(color.G * factor),
                                        Convert.ToInt32(color.B * factor));

        return (ColorTranslator.ToHtml(resColor));
    }
    public static void ConvertHsbtoRGB(float h, float s, float v, ref float red, ref float green, ref float blue)
    {
        if (s.Equals(0f))
        {
            red = v;
            green = v;
            blue = v;
        }
        else
        {
            var hue = Convert.ToSingle(h);
            if (h.Equals(360f))
            {
                hue = 0f;
            }

            hue /= 60f;

            var i = (int)(Math.Floor(Convert.ToDouble(hue)));
            var f = hue - i;
            var p = v * (1f - s);
            var q = v * (1f - s * f);
            var t = v * (1f - s * (1 - f));

            switch (i)
            {
                case 0:
                    red = v;
                    green = t;
                    blue = p;
                    break;
                case 1:
                    red = q;
                    green = v;
                    blue = p;
                    break;
                case 2:
                    red = p;
                    green = v;
                    blue = t;
                    break;
                case 3:
                    red = p;
                    green = q;
                    blue = v;
                    break;
                case 4:
                    red = t;
                    green = p;
                    blue = v;
                    break;
                case 5:
                    red = v;
                    green = p;
                    blue = q;
                    break;
                default:
                    red = 0f;
                    green = 0f;
                    blue = 0f;
                    //Trace.Assert(false); 
                    break;
                    // hue out of range 
            }
        }
    }
}
