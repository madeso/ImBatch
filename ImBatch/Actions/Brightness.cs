// Brightness.cs
// 
//

namespace ImBatch.Actions
{
    using System.Xml;

    internal class Brightness : Action
    {
        private readonly int brightness;

        public Brightness(XmlElement act)
        {
            this.brightness = Parsing.ParseInt(act, "brightness");
        }

        public override void Apply(ImageData img)
        {
            img.Image.SetBrightness(this.brightness);
        }
    }
}
