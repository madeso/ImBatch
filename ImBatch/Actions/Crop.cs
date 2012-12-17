// Crop.cs
// 
//

namespace ImBatch.Actions
{
    using System.Xml;

    internal class Crop : Action
    {
        private readonly int height;
        private readonly int width;
        private readonly int xPosition;
        private readonly int yPosition;

        public Crop(XmlElement element)
        {
            this.xPosition = Parsing.ParseInt(element, "x");
            this.yPosition = Parsing.ParseInt(element, "y");
            this.width = Parsing.ParseInt(element, "width");
            this.height = Parsing.ParseInt(element, "height");
        }

        public override void Apply(ImageData img)
        {
            img.Image.Crop(this.xPosition, this.yPosition, this.width, this.height);
        }
    }
}
