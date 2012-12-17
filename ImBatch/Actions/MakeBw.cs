// MakeBw.cs
// 
//

namespace ImBatch.Actions
{
    using System.Xml;

    internal class MakeBw : Action
    {
        public MakeBw(XmlElement act)
        {
        }

        public override void Apply(ImageData img)
        {
            img.Image.SetGrayscale();
        }
    }
}
