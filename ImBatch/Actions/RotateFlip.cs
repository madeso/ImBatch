// RotateFlip.cs
// 
//

namespace ImBatch.Actions
{
    using System.Drawing;
    using System.Xml;

    internal class RotateFlip : Action
    {
        private readonly RotateFlipType type;

        public RotateFlip(XmlElement act)
        {
            this.type = Parsing.ParseEnum<RotateFlipType>(act, "type");
        }

        public override void Apply(ImageData img)
        {
            img.Image.RotateFlip(this.type);
        }
    }
}
