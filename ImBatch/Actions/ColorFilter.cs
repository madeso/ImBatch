// ColorFilter.cs
// 
//

namespace ImBatch.Actions
{
    using System.Xml;

    internal class ColorFilter : Action
    {
        private readonly ColorFilterTypes filter;

        public ColorFilter(XmlElement act)
        {
            this.filter = Parsing.ParseEnum<ColorFilterTypes>(act, "filter");
        }

        public override void Apply(ImageData img)
        {
            img.Image.SetColorFilter(this.filter);
        }
    }
}
