namespace Plex.UIComponents.UIComponents;


[Serializable]
public class Button : BaseUIComponent
{
    public string Text = "";
    public string Onclick = "";
    public string CssClass = "";
    public Button(string text, string cssClass = "", string onclick = "")
    {
        Text = text;
        CssClass = cssClass;
        Onclick = onclick;
    }
}
