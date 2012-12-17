// Rename.cs
// 
//

namespace ImBatch.Actions
{
    using System.Xml;

    internal class Rename : Action
    {
        private readonly string pattern;

        public Rename(XmlElement element)
        {
            this.pattern = Parsing.ParseString(element, "pattern");
        }

        public override void Apply(ImageData img)
        {
            var f = this.pattern;
            foreach (var v in img.Params)
            {
                f = f.Replace("[" + v.Key + "]", v.Value);
            }
            img.Filename = f;
        }
    }
}
