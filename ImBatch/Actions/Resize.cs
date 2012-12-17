// Resize.cs
// 
//

namespace ImBatch.Actions
{
    using System.Xml;

    internal class Resize : Action
    {
        private readonly int height;
        private readonly int width;

        public Resize(XmlElement act)
        {
            this.width = Parsing.ParseInt(act, "width");
            this.height = Parsing.ParseInt(act, "height");
        }

        public override void Apply(ImageData img)
        {
            img.Image.Resize(this.width, this.height);
        }
    }
}
