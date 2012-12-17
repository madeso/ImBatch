// Contrast.cs
// 
//

namespace ImBatch.Actions
{
    using System.Xml;

    internal class Contrast : Action
    {
        private readonly double contrast;

        public Contrast(XmlElement element)
        {
            this.contrast = Parsing.ParseDouble(element, "contrast");
        }

        public override void Apply(ImageData img)
        {
            img.Image.SetContrast(this.contrast);
        }
    }
}
