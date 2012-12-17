// Shape.cs
// 
//

namespace ImBatch.Actions
{
    using System.Drawing;
    using System.Xml;

    internal class Shape : Action
    {
        private readonly Color color;
        private readonly int height;
        private readonly ShapeType shapeType;
        private readonly int width;
        private readonly int xPosition;
        private readonly int yPosition;

        public Shape(XmlElement element)
        {
            this.xPosition = Parsing.ParseInt(element, "x");
            this.yPosition = Parsing.ParseInt(element, "y");
            this.width = Parsing.ParseInt(element, "width");
            this.height = Parsing.ParseInt(element, "height");
            this.shapeType = Parsing.ParseEnum<ShapeType>(element, "shape");
            this.color = Parsing.ParseColor(element, "color");
        }

        public override void Apply(ImageData img)
        {
            img.Image.InsertShape(this.shapeType, this.xPosition, this.yPosition, this.width, this.height, this.color);
        }
    }
}
