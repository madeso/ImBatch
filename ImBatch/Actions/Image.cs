// Image.cs
// 
//

namespace ImBatch.Actions
{
    using System.Xml;

    internal class Image : Action
    {
        private readonly int height;
        private readonly string imagePath;
        private readonly int width;
        private readonly int xPosition;
        private readonly int yPosition;

        public Image(XmlElement element)
        {
            this.imagePath = Parsing.ParseString(element, "image");
            this.xPosition = Parsing.ParseInt(element, "x");
            this.yPosition = Parsing.ParseInt(element, "y");
            this.width = Parsing.ParseInt(element, "width");
            this.height = Parsing.ParseInt(element, "height");
        }

        public override void Apply(ImageData img)
        {
            img.Image.InsertImage(this.imagePath, this.xPosition, this.yPosition, this.width, this.height);
        }
    }
}
