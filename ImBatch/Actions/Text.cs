// Text.cs
// 
//

namespace ImBatch.Actions
{
    using System.Drawing;
    using System.Xml;

    internal class Text : Action
    {
        private readonly string fontName;
        private readonly float fontSize;
        private readonly FontStyle fontStyle;
        private readonly Color fromColor;
        private readonly string text;
        private readonly Color toColor;
        private readonly int xPosition;
        private readonly int yPosition;

        public Text(XmlElement e)
        {
            this.text = Parsing.ParseString(e, "text");
            this.xPosition = Parsing.ParseInt(e, "x");
            this.yPosition = Parsing.ParseInt(e, "y");
            this.fontName = Parsing.ParseString(e, "font");
            this.fontSize = (float)Parsing.ParseDouble(e, "size");
            this.fontStyle = Parsing.ParseEnum<FontStyle>(e, "style");
            this.fromColor = Parsing.ParseColor(e, "fromcolor");
            this.toColor = Parsing.ParseColor(e, "tocolor");
        }

        public override void Apply(ImageData img)
        {
            img.Image.InsertText(this.text, this.xPosition, this.yPosition, this.fontName, this.fontSize, this.fontStyle, this.fromColor, this.toColor);
        }
    }
}
