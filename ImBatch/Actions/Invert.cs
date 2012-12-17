// Invert.cs
// 
//

namespace ImBatch.Actions
{
    using System.Xml;

    internal class Invert : Action
    {
        public Invert(XmlElement act)
        {
        }

        public override void Apply(ImageData img)
        {
            img.Image.SetInvert();
        }
    }
}
